int vout = A1;

void setup() {

  Serial.begin(115200);
  pinMode(vout, INPUT);
  // put your setup code here, to run once:

}

void loop() {

  int data = analogRead(vout);
  if(data > 511){
    Serial.write ("1");
    Serial.flush();
    delay(20);
  }
  else{
    Serial.write("2");
    Serial.flush();
    delay(20);
  }
  // put your main code here, to run repeatedly:

}
