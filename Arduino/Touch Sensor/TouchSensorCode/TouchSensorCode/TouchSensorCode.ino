#define TOUCH_PIN 2
#define LED_PIN 13

void setup() {
  Serial.begin(9600);
  pinMode(TOUCH_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
}

void loop() {


if(digitalRead(TOUCH_PIN) == HIGH)
{
  digitalWrite(LED_PIN, LOW);  
}
if(digitalRead(TOUCH_PIN) == LOW)
{
  digitalWrite(LED_PIN, HIGH);
}
  
}
