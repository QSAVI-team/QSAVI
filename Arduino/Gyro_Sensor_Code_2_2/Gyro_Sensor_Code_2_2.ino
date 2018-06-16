
// 6/21/2012 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/i2cdevlib
//
// Changelog:
//      2013-05-08 - added seamless Fastwire support
//                 - added note about gyro calibration
//      2012-06-21 - added note about Arduino 1.0.1 + Leonardo compatibility error
//      2012-06-20 - improved FIFO overflow handling and simplified read process
//      2012-06-19 - completely rearranged DMP initialization code and simplification
//      2012-06-13 - pull gyro and accel data from FIFO packet instead of reading directly
//      2012-06-09 - fix broken FIFO read sequence and change interrupt detection to RISING
//      2012-06-05 - add gravity-compensated initial reference frame acceleration output
//                 - add 3D math helper file to DMP6 example sketch
//                 - add Euler output and Yaw/Pitch/Roll output formats
//      2012-06-04 - remove accel offset clearing for better results (thanks Sungon Lee)
//      2012-06-01 - fixed gyro sensitivity to be 2000 deg/sec instead of 250
//      2012-05-30 - basic DMP initialization working

/* ============================================
  I2Cdev device library code is placed under the MIT license
  Copyright (c) 2012 Jeff Rowberg

  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sel
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in
  all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
  THE SOFTWARE.
  ===============================================
*/

// I2Cdev and MPU6050 must be installed as libraries, or else the .cpp/.h files
// for both classes must be in the include path of your project
#include "I2Cdev.h"

#include "MPU6050_6Axis_MotionApps20.h"
#include "MPU6050.h" // not necessary if using MotionApps include file

// Arduino Wire library is required if I2Cdev I2CDEV_ARDUINO_WIRE implementation
// is used in I2Cdev.h
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
#include "Wire.h"
#endif

// DEBUG serial printing macros
#ifdef DEBUG
  //#define DPRINT(args...)  Serial.print(args)             //OR use the following syntax:
  #define DPRINTSTIMER(t)    for (static unsigned long SpamTimer; (unsigned long)(millis() - SpamTimer) >= (t); SpamTimer = millis())
  #define  DPRINTSFN(StrSize,Name,...) {char S[StrSize]};//Serial.print("\t");Serial.print(Name);Serial.print(" "); Serial.print(dtostrf((float)__VA_ARGS__ ,S));}//StringSize,Name,Variable,Spaces,Percision
  #define DPRINTLN(...)      //Serial.println(__VA_ARGS__)
#else
  #define DPRINTSTIMER(t)    if(false)
  #define DPRINTSFN(...)     //blank line
  #define DPRINTLN(...)      //blank line
#endif


// class default I2C address is 0x68
// specific I2C addresses may be passed as a parameter here
// AD0 low = 0x68 (default for SparkFun breakout and InvenSense evaluation board)
// AD0 high = 0x69
MPU6050 mpus[2]={MPU6050(0x68),MPU6050(0x69)};  // original code said MPU6050 mpu
 //MPU6050 mpu2(0x69); // <-- use for AD0 high

/* =========================================================================
   NOTE: In addition to connection 3.3v, GND, SDA, and SCL, this sketch
   depends on the MPU-6050's INT pin being connected to the Arduino's
   external interrupt #0 pin. On the Arduino Uno and Mega 2560, this is
   digital I/O pin 2.
   ========================================================================= */

/* =========================================================================
   NOTE: Arduino v1.0.1 with the Leonardo board generates a compile error
   when using Serial.write(buf, len). The Teapot output uses this method.
   The solution requires a modification to the Arduino USBAPI.h file, which
   is fortunately simple, but annoying. This will be fixed in the next IDE
   release. For more info, see these links:

   http://arduino.cc/forum/index.php/topic,109987.0.html
   http://code.google.com/p/arduino/issues/detail?id=958
   ========================================================================= */

// uncomment "OUTPUT_QSAVI_GLOVE" for the output to Unity
// Data Packet format is: cali,wPalm,xPalm,yPalm,zPalm,indBase,indknuckle, thumbCurl,thumbLateral,midBase,midknuckle
#define OUTPUT_QSAVI_GLOVE

// uncomment "OUTPUT_READABLE_QUATERNION" if you want to see the actual
// quaternion components in a [w, x, y, z] format (not best for parsing
// on a remote host such as Processing or something though)
//#define OUTPUT_READABLE_QUATERNION

// uncomment "OUTPUT_READABLE_EULER" if you want to see Euler angles
// (in degrees) calculated from the quaternions coming from the FIFO.
// Note that Euler angles suffer from gimbal lock (for more info, see
// http://en.wikipedia.org/wiki/Gimbal_lock)
//#define OUTPUT_READABLE_EULER

// uncomment "OUTPUT_READABLE_YAWPITCHROLL" if you want to see the yaw/
// pitch/roll angles (in degrees) calculated from the quaternions coming
// from the FIFO. Note this also requires gravity vector calculations.
// Also note that yaw/pitch/roll angles suffer from gimbal lock (for
// more info, see: http://en.wikipedia.org/wiki/Gimbal_lock)
//#define OUTPUT_READABLE_YAWPITCHROLL

// uncomment "OUTPUT_READABLE_REALACCEL" if you want to see acceleration
// components with gravity removed. This acceleration reference frame is
// not compensated for orientation, so +X is always +X according to the
// sensor, just without the effects of gravity. If you want acceleration
// compensated for orientation, us OUTPUT_READABLE_WORLDACCEL instead.
//#define OUTPUT_READABLE_REALACCEL

// uncomment "OUTPUT_READABLE_WORLDACCEL" if you want to see acceleration
// components with gravity removed and adjusted for the world frame of
// reference (yaw is relative to initial orientation, since no magnetometer
// is present in this case). Could be quite handy in some cases.
//#define OUTPUT_READABLE_WORLDACCEL

// uncomment "OUTPUT_TEAPOT" if you want output that matches the
// format used for the InvenSense teapot demo
//#define OUTPUT_TEAPOT

// This defines the calibrating button sensor. This sets the yaw/pitch/roll to 0/0/0
// on the arduino if the button is pressed.
#define BUTTON_PIN 3
#define POTBASE 2

#define LED_PIN 13 // (Arduino is 13, Teensy is 11, Teensy++ is 6)
bool blinkState = false;

// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
//uint8_t mpuIntStatus[2];   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)

// packet structure for InvenSense teapot demo
uint8_t teapotPacket[14] = { '$', 0x02, 0, 0, 0, 0, 0, 0, 0, 0, 0x00, 0x00, '\r', '\n' };
  int pos = 0; 



// ================================================================
// ===                       DATA SENDING                       ===
// ================================================================

// Will be un-used
#define SEND_DELAY 100 // ms
#define WAIT_FOR_REQUEST_BEFORE_SENDING_DATA

// ================================================================
// ===                          TIMING                          ===
// ================================================================

#define HEARBEAT_LED_BLINK_TIME 500 // ms
unsigned long lastHearbeadLedBlinkTime = 0;
unsigned long currentMillis = 0;    // stores the value of millis() in each iteration of loop()
unsigned long prevMillis = 0;    // stores the value of millis() in each iteration of loop()





// ================================================================
// ===                      INITIAL SETUP                       ===
// ================================================================

void i2cSetup() {
  // join I2C bus (I2Cdev library doesn't do this automatically)
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
  Wire.begin();
  TWBR = 24; // 400kHz I2C clock (200kHz if CPU is 8MHz)
#elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
  Fastwire::setup(400, true);
#endif

  // initialize serial communication
  // (115200 chosen because it is required for Teapot Demo output, but it's
  // really up to you depending on your project)
 // Serial.begin(115200);  //...............................................................
 // while (!Serial); // wait for Leonardo enumeration, others continue immediately  //.......
}



// ================================================================
// ===               INTERRUPT DETECTION ROUTINE                ===
// ================================================================
// indicates whether MPU interrupt pin has gone high
volatile bool mpuInterrupt[2] = {false, false};     

//Intentional duplication. Interrupt routine cannot pass args. 
void dmpDataReady0() {
    mpuInterrupt[0] = true;
}
void dmpDataReady1() {
    mpuInterrupt[1] = true;
}

int FifoAlive[2] = {0, 0}; // tests if the interrupt is triggering
uint16_t packetSize[2];    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount[2];     // count of all bytes currently in FIFO
uint8_t fifoBuffer[2][64]; // FIFO storage buffer


// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
Quaternion q2;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector



  // NOTE: 8MHz or slower host processors, like the Teensy @ 3.3v or Ardunio
  // Pro Mini running at 3.3v, cannot handle this baud rate reliably due to
  // the baud timing being too misaligned with processor ticks. You must use
  // 38400 or slower in these cases, or use some kind of external separate
  // crystal solution for the UART timer.



  //=========================

  //========================
void MPU6050Connect() {

for (int i = 0; i < 2; i++) {

      //Serial.print(F("Initializing MPU ")); Serial.println(i);
    
    static int MPUInitCntr = 0;  //Counter for looping and eventual initialization failure.

  mpus[i].initialize();

  devStatus = mpus[i].dmpInitialize();

    if (devStatus != 0) {
      
      // ERROR Checking!
      // 1 = initial memory load failed
      // 2 = DMP configuration updates failed
      // (if it's going to break, usually the code will be 1)
      
      char *StatStr[5] { "No Error", "initial memory load failed", "DMP configuration updates failed", "3", "4"};

      MPUInitCntr++;

      //Serial.print(F("MPU")); Serial.print(i); Serial.print(F(" connection Try #")); Serial.println(MPUInitCntr);
      //Serial.print("3");//Serial.print(F("DMP Initialization failed (code ")); Serial.print(StatStr[devStatus]); Serial.println(F(")"));
      
      if (MPUInitCntr >= 10) return; //only try 10 times
      delay(1000);
      MPU6050Connect(); // Lets try again
      return;
    }


  // supply your own gyro offsets here, scaled for min sensitivity
  mpus[i].setXGyroOffset(220);
  mpus[i].setYGyroOffset(76);
  mpus[i].setZGyroOffset(-85);
  mpus[i].setZAccelOffset(1788); // 1688 factory default for my test chip



    //Serial.print(F("Enabling DMP on MPU")); //Serial.println(i);
    mpus[i].setDMPEnabled(true);

    // enable Arduino interrupt detection
    int intPin = i + 2;
   // Serial.print(F("Enabling interrupt detection on MPU")); Serial.print(i); Serial.print(F("(Arduino external interrupt pin ")); Serial.print(intPin); Serial.println(F(" on the Uno)..."));
    //Serial.print(F("mpu")); Serial.print(i); Serial.print(F(".getInterruptDrive=  ")); Serial.println(mpus[i].getInterruptDrive());  //Current interrupt drive mode (0=push-pull, 1=open-drain)
    
    //Intentional duplication. Interrupt routine cannot pass args. 
    if (i == 0) {
      attachInterrupt(i, dmpDataReady0, RISING);
    }
    else {
      attachInterrupt(i, dmpDataReady1, RISING);
    }
    
    packetSize[i] = mpus[i].dmpGetFIFOPacketSize();  // get expected DMP packet size for later comparison
    delay(1000); // Let it Stabalize
    mpus[i].resetFIFO(); // Clear fifo buffer    
    mpuInterrupt[i] = false; // wait for next interrupt


  /* configure LED for output
  pinMode(LED_PIN, OUTPUT);
  // Configure button for input
  pinMode(BUTTON_PIN, INPUT);
  pinMode(POTBASE,INPUT);
  */
}

}


// ================================================================
// ===                    MAIN PROGRAM LOOP                     ===
// ================================================================

void GetDMP() { 

  for (int i = 0; i < 2; i++) { 
    mpuInterrupt[i] = false;
    FifoAlive[i] = 1;
    fifoCount[i] = mpus[i].getFIFOCount();
    
    if ((!fifoCount[i]) || (fifoCount[i] % packetSize[i])) { // we have failed Reset and wait till next time!
      digitalWrite(LED_PIN, LOW); // lets turn off the blinking LED so we can see we are failing.
      //Serial.print("1");
        mpus[i].resetFIFO();// clear the buffer and start over
    } 
    else {
      while (fifoCount[i]  >= packetSize[i]) { // Get the packets until we have the latest!
          mpus[i].getFIFOBytes(fifoBuffer[i], packetSize[i]); // lets do the magic and get the data
          fifoCount[i] -= packetSize[i];
      }
    }
  } 


  MPUMath(); // Successful!  Do the math and show angles from both MPUs
  digitalWrite(LED_PIN, !digitalRead(LED_PIN)); // Blink the LED on each cycle
  
}


//====================================================//
//----------------- QUATERNION OUTPUTS---------------
//===================================================//
void MPUMath() {


#ifdef OUTPUT_QSAVI_GLOVE

#ifdef WAIT_FOR_REQUEST_BEFORE_SENDING_DATA

    // Wait for request (any 1 byte)
  while (Serial.available() > 0) {      // doesnt like this while or the if  below when viewing through the serial monitor
      if (Serial.read() > 0) {
        
        Serial.print(0); Serial.print(","); // used for calibration
 
     for (int i = 0; i < 2; i++) {
        mpus[i].dmpGetQuaternion(&q, fifoBuffer[i]);
       // Serial.println("Waiting for request");

        Serial.print(q.w); Serial.print(",");
        Serial.print(q.x); Serial.print(",");
        Serial.print(q.y); Serial.print(",");
        Serial.print(q.z); Serial.print(",");
       }
        Serial.print(analogRead(8));Serial.print(","); Serial.print(analogRead(9));Serial.print(","); // thumb
         Serial.print(analogRead(10));Serial.print(","); Serial.print(analogRead(11));Serial.print(","); // index
         Serial.print(analogRead(12));Serial.print(","); Serial.print(analogRead(13));Serial.print(","); // middle
          Serial.print(analogRead(14));Serial.print(","); Serial.println(analogRead(15)); // pinky
          // string length should be 17 now

     
#endif
     
#ifdef WAIT_FOR_REQUEST_BEFORE_SENDING_DATA
     
   } 
  }
#endif

#endif

#ifdef OUTPUT_READABLE_QUATERNION

#ifdef WAIT_FOR_REQUEST_BEFORE_SENDING_DATA
    // Wait for request (any 1 byte)
    while (Serial.available() > 0) {
      if (Serial.read() > 0) {
      #endif
      
    // Setup the buttonState to read the status of the button
    buttonState = digitalRead(BUTTON_PIN);
    // If using button, set if condition buttonstate to LOW
    // If using touch sensor, use HIGH
    if (buttonState == HIGH) // Condition if button is not pressed, LOW for button, HIGH for touch sensor
    {Serial.print("1,");
    }
    else if (buttonState == LOW) // Condition if button is pressed, HIGH for button, LOW for touch sensor
    {Serial.print("2,");
    }
        // display quaternion values in easy matrix form: w, x, y, z
        mpu.dmpGetQuaternion(&q, fifoBuffer);
        //Serial.print("quat\t");
        Serial.print(q.w);
        Serial.print(",");
        Serial.print(q.x);
        Serial.print(",");
        Serial.print(q.y);
        Serial.print(",");
        Serial.print(q.z);
        Serial.print(",");
        Serial.print(0);
        Serial.print(",");
        Serial.print(0);
        Serial.print(",");
        Serial.print(0);
        Serial.print(",");
        Serial.print(0);
        

      #ifdef WAIT_FOR_REQUEST_BEFORE_SENDING_DATA
      }
    }
#endif

#endif

#ifdef OUTPUT_READABLE_EULER
    // display Euler angles in degrees

      mpu.dmpGetQuaternion(&q, fifoBuffer);
      mpu.dmpGetEuler(euler, &q);
      //            Serial.print("euler\t");
      Serial.print(euler[0] * 180 / M_PI);
      Serial.print(",");
      Serial.print(euler[1] * 180 / M_PI);
      Serial.print(",");
      Serial.println(euler[2] * 180 / M_PI);

#endif



#ifdef OUTPUT_READABLE_YAWPITCHROLL
    // Setup the buttonState to read the status of the button
    buttonState = digitalRead(BUTTON_PIN);
    // If using button, set if condition buttonstate to LOW
    // If using touch sensor, use HIGH
    if (buttonState == HIGH) // Condition if button is not pressed, LOW for button, HIGH for touch sensor
    {
      // display Euler angles in degrees
      mpu.dmpGetQuaternion(&q, fifoBuffer);
      mpu.dmpGetGravity(&gravity, &q);
      mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
      Serial.print("1,");
    }
    else if (buttonState == LOW) // Condition if button is pressed, HIGH for button, LOW for touch sensor
    {
      // display Euler angles in degrees
      mpu.dmpGetQuaternion(&q, fifoBuffer);
      mpu.dmpGetGravity(&gravity, &q);
      mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
      Serial.print("2,");
    }
    Serial.print(ypr[0] * 180 / M_PI);
    Serial.print(",");
    Serial.print(ypr[1] * 180 / M_PI);
    Serial.print(",");
    Serial.println(ypr[2] * 180 / M_PI);
    delay(SEND_DELAY);


#endif

#ifdef OUTPUT_READABLE_REALACCEL
    // display real acceleration, adjusted to remove gravity
    mpu.dmpGetQuaternion(&q, fifoBuffer);
    mpu.dmpGetAccel(&aa, fifoBuffer);
    mpu.dmpGetGravity(&gravity, &q);
    mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
    Serial.print("0,");
    Serial.print(",");
    Serial.print(aaReal.x);
    Serial.print(",");
    Serial.print(aaReal.y);
    Serial.print(",");
    Serial.println(aaReal.z);
    //            delay(20);
#endif

#ifdef OUTPUT_READABLE_WORLDACCEL
    // display initial world-frame acceleration, adjusted to remove gravity
    // and rotated based on known orientation from quaternion
    mpu.dmpGetQuaternion(&q, fifoBuffer);
    mpu.dmpGetAccel(&aa, fifoBuffer);
    mpu.dmpGetGravity(&gravity, &q);
    mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
    mpu.dmpGetLinearAccelInWorld(&aaWorld, &aaReal, &q);
    Serial.print("aworld\t");
    Serial.print(aaWorld.x);
    Serial.print("\t");
    Serial.print(aaWorld.y);
    Serial.print("\t");
    Serial.println(aaWorld.z);
    //            delay(20);
#endif

#ifdef OUTPUT_TEAPOT
    // display quaternion values in InvenSense Teapot demo format:
    teapotPacket[2] = fifoBuffer[0];
    teapotPacket[3] = fifoBuffer[1];
    teapotPacket[4] = fifoBuffer[4];
    teapotPacket[5] = fifoBuffer[5];
    teapotPacket[6] = fifoBuffer[8];
    teapotPacket[7] = fifoBuffer[9];
    teapotPacket[8] = fifoBuffer[12];
    teapotPacket[9] = fifoBuffer[13];
    Serial.write(teapotPacket, 14);
    teapotPacket[11]++; // packetCount, loops at 0xFF on purpose
#endif

    unsigned long currentMillis =  millis();  //.....................................................

    // Blink "Hearbeat" LED to indicate activity
    if (currentMillis - lastHearbeadLedBlinkTime >= HEARBEAT_LED_BLINK_TIME) {
      lastHearbeadLedBlinkTime = currentMillis;
      blinkState = !blinkState;
      digitalWrite(LED_PIN, blinkState);
      
    }

}

 


void setup() {
  Serial.begin(115200);
  while (!Serial);  // Wait for the connection to be established?

  // Run MPU initializations
  //Serial.println(F("i2cSetup"));
  i2cSetup();
  //Serial.println(F("MPU6050 Connection Routine"));
  MPU6050Connect();
  //Serial.println(F("Setup complete"));

  pinMode(LED_PIN, OUTPUT);

  loop();
}


/**
 * =============================================================
 * ===                  Arduino Loop                         ===
 * =============================================================
 *
 * @author Mike Muscato
 * @date   2017-01-30
 *
 * @return {void}
 */
void loop() {
 unsigned long currentMillis = millis();   // Capture the latest value of millis()

  if (mpuInterrupt[0] && mpuInterrupt[1]) { // Wait for MPU interrupt or extra packet(s) available on both MPUs
    GetDMP();
  }

  // Uncomment for setting and adjusting the hardware start position. 
  // (e.g. Setting rotation and leveling the pan/tilt servo arms at initial 90-degree position)
  // IMPORTANT: Comment out the moveServos() method call in MPUMath() when using this. 
  // TODO: Remove in final program once hardware and software calibration is completed.
  // 
  // yawServo.write(90);
  // pitchServo.write(90);

}






