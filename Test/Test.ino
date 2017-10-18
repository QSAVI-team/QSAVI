int vout = A5;

void setup() {

  Serial.begin(115200);
  pinMode(A1, INPUT);
  // put your setup code here, to run once:

}

void loop() {

  int data = analogRead(vout);
  if(data > 511){
    Serial.write ("A");
    Serial.flush():
    delay(20);
  }
  else{
    Serial.write("C");
    Serial.flush():
    delay(20);
  }
  // put your main code here, to run repeatedly:

}
