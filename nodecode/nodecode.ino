#include<Wire.h>
#include<ESP8266WiFi.h>
#include<SoftwareSerial.h>

SoftwareSerial bt(D3,D4);

const uint16_t port=3500;
const char* host="192.168.43.195";
float X_out,Y_out,Z_out;
WiFiClient wand;
bool spell_end=false;
String spell_hold_lamp="nox";
void setup() { 
  // put your setup code here, to run once:
  Serial.begin(115200);
  bt.begin(9600);
  pinMode(D7,INPUT_PULLUP); 
  WiFi.begin("Coolboi","qwertyuiop");
  while(WiFi.status()!=WL_CONNECTED){
    delay(500);
    Serial.println("...");
  }
  Serial.print("WiFi connected with IP:");
  Serial.println(WiFi.localIP());
  while(!wand.connect(host,port)){
    Serial.println("Connecting to host");
    delay(1000);
  }
  Serial.println("connection succesful");
  Wire.begin(D1,D2);
  Wire.beginTransmission(0x53);
  Wire.write(0x2D);
  Wire.write(8);
  Wire.endTransmission();
  delay(10);
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(D6,LOW);
  digitalWrite(D5,HIGH);
  Wire.beginTransmission(0x53);
  Wire.write(0x32);
  Wire.endTransmission(false);
  Wire.requestFrom(0x53,6,true);
  X_out=(Wire.read() | Wire.read()<<8);
  Y_out=(Wire.read() | Wire.read()<<8);
  Z_out=(Wire.read() | Wire.read()<<8);
  //Serial.println(String(X_out)+','+String(Y_out)+','+String(Z_out));
  if(digitalRead(D7)==LOW){
  wand.print(String(X_out)+','+String(Y_out)+','+String(Z_out));
  Serial.println("yay");
  spell_end=true;}
  if(digitalRead(D7)==HIGH && spell_end){
    wand.print("abcde");
    wand.print("abcde");
    spell_end=false;
    delay(200);
    while(wand.available()){
      String spell=wand.readStringUntil('\n');
      Serial.println(spell); 
      /*if(spell=="circle" && spell_hold_lamp!=spell ){
            digitalWrite(D6,HIGH);
            digitalWrite(D5,HIGH);
            delay(200);
            digitalWrite(D6,LOW);
            digitalWrite(D5,LOW);
            spell_hold_lamp=spell;
      }
      if(spell=="nox" && spell_hold_lamp!=spell ){
            digitalWrite(D6,HIGH);
            digitalWrite(D5,HIGH);
            delay(200);
            digitalWrite(D6,LOW);
            digitalWrite(D5,LOW);
            spell_hold_lamp=spell;
      }*/
      if(spell=="circle"){
        for(int i=0;i<100;i++){
          bt.print("1");
          Serial.print("1");
          delay(50);  
        }  
      }
      if(spell=="exp"){
        for(int i=0;i<100;i++){
          bt.print("2");
          Serial.print("2");
          delay(50);  
        }  
      }
      if(spell=="gig"){
        for(int i=0;i<100;i++){
          bt.print("3");
          Serial.print("3");
          delay(50);  
        }  
      }
    }
  }
  bt.write("0");
  delay(30);
}
