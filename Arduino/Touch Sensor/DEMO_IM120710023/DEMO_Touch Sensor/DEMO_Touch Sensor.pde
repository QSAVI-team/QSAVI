/******************************************
	FILE:		DEMO_Touch Sensor.pde
	PURPOSE:	Touch sensor sketch for Arduino
	Created by Stan Lee from Iteadstuduino
	E-mail:		Lizq@iteadstudio.com
	DATE:		2013/4/20
*******************************************/

int Button=2;		//connect button to D2
int LED=13;
void setup()
{
  pinMode(LED, OUTPUT);
  pinMode(Button, INPUT);
  
}
 
void loop()
{
   
if(digitalRead(Button)==HIGH)	//when the digital output value of button is high, turn on the LED.
  {
    digitalWrite(LED, LOW);
   
  }
   
  if(digitalRead(Button)==LOW)	//when the digital output value of button is low, turn off the LED.
  {
    digitalWrite(LED, HIGH);
  }
 
}