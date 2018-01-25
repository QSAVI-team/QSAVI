/*********************** Arduino Bend Sensor Code **********************************/
/* Written by Dj
 * 11.10.2017 
 * 
 * The Purpose of this code is to determine the angle of a bend sensor for the use in modeling the 3D position
 * of a hand in unity. This code will print the angle, theta, in degress of the bend angle of the sensor.
 * This code uses equations from sensorprod.com which were given by Sensor Products Inc Brochures
 */

#define PIN_IN A0 // Input pin for flex sensor

void setup() {
  // put your setup code here, to run once:
  
  Serial.begin(9600);
  pinMode(PIN_IN, INPUT);
  
}

void loop() {

  float vin; // voltage across bend sensor in arduino values (0-1023)
  float v; // Voltage across bend sensor (0-5V)
  float vs; // Supplied voltage to the system
  float R; //  Resistance across bend sensor
  float Rmax; // Max resistance of bend sensor
  float Rmin; // Min resistance of bend sensor
  float r1; // Series resistor in voltage divider circuit
  float theta_max; // Maximum bending angle in degrees
  float theta_min; // Minimum bending angle in degrees
  float theta; // Angle of the bend sensor

  // Constant Variable Declarations
  Rmax = 1100000; // Not expressed with decimals due to character type constraints
  Rmin = 4800; // Not expressed with decimals due to character type constraints
  theta_max = 180;
  theta_min = 0;
  r1 = 5000;
  vs = 5;

  // Time Dependent Variables
  vin = analogRead(PIN_IN);
  v = 5.0*vin/1023.0;
  R = (v*r1)/(vs - v);
  theta = theta_min + ((theta_max - theta_min)/(Rmax - Rmin))*(R - Rmin);
  
//  Serial.print("The voltage across the sensor is ");
//  Serial.println(v);
//  Serial.print("The resistance of the sensor is ");
//  Serial.println(R);
//  Serial.print("The angle of the bend sensor is ");
  Serial.println(theta);
  delay(1000);

  
}
