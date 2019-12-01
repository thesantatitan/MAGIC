/*
    Arduino and ADXL345 Accelerometer Tutorial
     by Dejan, https://howtomechatronics.com
*/
#include <Wire.h>  // Wire library - used for I2C communication
int ADXL345 = 0x53; // The ADXL345 sensor I2C address
float X_out, Y_out, Z_out;  // Outputs
bool t1=true;
void setup() {
  Serial.begin(9600); // Initiate serial communication for printing the results on the Serial monitor
  Wire.begin(D1,D2); // Initiate the Wire library
  // Set ADXL345 in measuring mode
  Wire.beginTransmission(ADXL345); // Start communicating with the device 
  Wire.write(0x2D); // Access/ talk to POWER_CTL Register - 0x2D
  // Enable measurement
  Wire.write(8); // (8dec -> 0000 1000 binary) Bit D3 High for measuring enable 
  Wire.endTransmission();
  delay(10);
  pinMode(D7,INPUT_PULLUP);
}



void loop() {
  // === Read acceleromter data === //
  Wire.beginTransmission(ADXL345);
  Wire.write(0x32); // Start with register 0x32 (ACCEL_XOUT_H)
  Wire.endTransmission(false);
  Wire.requestFrom(ADXL345, 6, true); // Read 6 registers total, each axis value is stored in 2 registers
  X_out = ( Wire.read()| Wire.read() << 8); // X-axis value
  //X_out = X_out/32; //For a range of +-2g, we need to divide the raw values by 256, according to the datasheet
  Y_out = ( Wire.read()| Wire.read() << 8); // Y-axis value
  //Y_out = Y_out/32;
  Z_out = ( Wire.read()| Wire.read() << 8); // Z-axis value
  //Z_out = Z_out/32;
  //Serial.write('<');
  //Serial.write("Xa= ");
  //Serial.println(X_out);
  //Serial.print(' ');
  //Serial.write("   Ya= ");
  Str      ing z=String(X_out);
  //char arr[z.length()+1];
  //z.toCharArray(arr,z.length()+1);
  String z1=String(Z_out);
  z=z+' '+z1+'\n';
  char arr[z.length()+1];
  z.toCharArray(arr,z.length()+1);
  if(digitalRead(D7)==LOW){
    Serial.write(arr);
    t1=false;
  }
  if(!t1 && digitalRead(D7)==HIGH){
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    Serial.write('e');
    t1=true;
  }
  //Serial.write(arr);
  //Serial.write(' ');
  //Serial.write(arr1);
  //Serial.write('\n');
  //Serial.write("   Za= ");
  //Serial.write(Z_out);
  //Serial.write('\n');
  delay(30);
}
