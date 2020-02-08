#include <Wire.h>

#include <dht11.h>
#include <LiquidCrystal_I2C_AvrI2C.h>
//#include <LiquidCrystal.h>
#define DHT11PIN 2 // DHT11PIN olarak Dijital 2"yi belirliyoruz.
#define echoPin 6
#define trigPin 7
#define buzzerPin 8
#define sensor_pin A0
#define buzzer_pin 9
//RGB LED'imizin çıkış pinlerini tanımlıyoruz:
#define led_gas 4
//Sensörün çalışması için gerekli ön ısıtma süresini 5sn olarak belirliyoruz
#define preheat_time 5000
//Alarmın çalması için gerekli eşik değerini 300 olarak belirliyoruz.
#define threshold 300
#define SLAVE_ADDRESS 0x40// Define the i2c address
dht11 DHT11; // DHT11_sensor adında bir DHT11 nesnesi oluşturduk.
int komut=0;

LiquidCrystal_I2C_AvrI2C lcd(0x27,16,2);  // 16 karakter 2 satır için - 20x4 satır için (0x27,20,4) yazın
int piractiveled=10;
int pirpasifled=9;
int pirPin = 12;
int val;
int brightness = 0; // parlaklık için değişken atadık
int maximumRange = 50;
int minimumRange = 0;
String sensorsonuclari="";
void setup() {
  // Sıcaklık-Nem S.
  Serial.begin(9600); // Seri iletişimi başlatıyoruz.
  Serial.println("Ertekin Özturgut");
  Wire.begin(SLAVE_ADDRESS); 
  //Yakınlık S.
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(pirpasifled,OUTPUT);
  pinMode(piractiveled,OUTPUT);
  //
}
int denemesayisi=0;
void loop() {
    Wire.begin(SLAVE_ADDRESS); 

if(komut==0)
{
  //Serial.println("Komut Algılanmadı");
   Wire.onRequest(sendData); 
   delay(1000);
   sensorsonuclari="";
//veri[0]='a';
  komut=1;
}
if(komut==1)
{
  hareket();
  komut=2;
 // veri[1]='a';
   delay(500);

   denemesayisi++;
}
if(komut==2)
{
  komut=3;
  yakinlik();
 // veri[2]='a';
   delay(500);

     denemesayisi++;

}
if(komut==3)
{  
  komut=4;
  sicakliknem();
     denemesayisi++;
   delay(500);

}
if(komut==4)
{  
  komut=0;
     denemesayisi++; 
     delay(500);

//veri[3]='a';

  gaz();
}

}
void sicakliknem()
{
    // put your main code here, to run repeatedly:
 Serial.println();
  // Sensörün okunup okunmadığını konrol ediyoruz. 
  // chk 0 ise sorunsuz okunuyor demektir. Sorun yaşarsanız
  // chk değerini serial monitörde yazdırıp kontrol edebilirsiniz.
  int chk = DHT11.read(DHT11PIN);

  // Sensörden gelen verileri serial monitörde yazdırıyoruz.
  Serial.print("Nem (%): ");
  sensorsonuclari+="/"+String((float)DHT11.humidity, 2);
  Serial.println((float)DHT11.humidity, 2);

  Serial.print("Sicaklik (Celcius): ");
  Serial.println((float)DHT11.temperature, 2);
 sensorsonuclari+="/"+String((float)DHT11.temperature, 2);


  Serial.print("Sicaklik (Fahrenheit): ");
  Serial.println(DHT11.fahrenheit(), 2);

  Serial.print("Sicaklik (Kelvin): ");
  Serial.println(DHT11.kelvin(), 2);

  // Çiğ Oluşma Noktası, Dew Point
  Serial.print("Cig Olusma Noktasi: ");
  Serial.println(DHT11.dewPoint(), 2);

  // 2 saniye bekliyoruz. 2 saniyede bir veriler ekrana yazdırılacak.
  delay(500);
}
void lcdyazdir(String metin)
{
lcd.begin();
lcd.backlight();
lcd.print(metin);
}
void yakinlik()
{
  int olcum = mesafe(maximumRange, minimumRange);
  sensorsonuclari+="/"+String(olcum);
  Serial.println(""+sensorsonuclari+"/");
  String sonuc=olcum+"";
  //lcdyazdir(sonuc);
  melodi(olcum * 10);

}
int mesafe(int maxrange, int minrange)
{
  long duration, distance;
  digitalWrite(trigPin, LOW);
  
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH);
  distance = duration / 58.2;
  delay(50);
  if (distance >= maxrange || distance <= minrange)
    return 0;
  return distance;
}
int melodi(int dly)
{
digitalWrite(buzzerPin, HIGH);
delay(dly);
digitalWrite(buzzerPin, LOW);
delay(dly);

 // tone(buzzerPin, 440);
 // delay(dly);
  //noTone(buzzerPin);
//  delay(dly);
}
void gaz()
{
  //analogRead() fonksiyonu ile sensör değerini ölçüyoruz ve bir değişken içerisinde tutuyoruz:
int sensorValue = analogRead(sensor_pin);
//Sensör değeri belirlediğimiz eşik değerinden yüksek ise alarm() fonksiyonunu çağırıyoruz:
if (sensorValue >= threshold)
gazalarm(100);
//Alarmın çalmadığı durumda LED'in yeşil yanmasını istiyoruz:
else
//digitalWrite(led_g, LOW);
//Sensör değerini bilgisayarımızdan görebilmek için seri monitöre yazıyoruz
sensorsonuclari+="/"+String(sensorValue);
Serial.println(""+sensorsonuclari+"/");
delay(1);
}
void gazalarm(unsigned int duration)
{
//Buzzer'ın 440Hz'te (la notası) ses üretmesini istiyoruz:
//tone(buzzer_pin, 440);
//Normal durumda yeşil yanmakta olan LED'i söndürüp kırmızı renkte yakıyoruz:

digitalWrite(led_gas, LOW);

delay(duration);
//noTone(buzzer_pin);
//digitalWrite(led_r, HIGH);
digitalWrite(led_gas, HIGH);

delay(duration);
}
//hareket algılama tamam
void hareket()
{
  val = digitalRead(pirPin); //read state of the PIR
  String har="";
  if (val == LOW) {
    har="0";
    lcdyazdir(har);
     analogWrite(9,255); // analogWrite kodu ile 0-255 arasında bir parlaklık değeri atıyoruz
     analogWrite(10, 0);// led1 mizin parlaklığı artarken led2 mizinki azalıcak
     sensorsonuclari+=har;
     Serial.println(""+sensorsonuclari+"/"); //if the value read was high, there was motion

  }
  else {
    har="1";
    lcdyazdir(har);
      analogWrite(10, 255);// led1 mizin parlaklığı artarken led2 mizinki azalıcak
      analogWrite(9, 0);// led1 mizin parlaklığı artarken led2 mizinki azalıcak
      sensorsonuclari+=har;
      Serial.println(""+sensorsonuclari+"/"); //if the value read was high, there was motion
  }
  delay(500);
}
void sendData()
{
 Serial.println(""+sensorsonuclari+"/"); //if the value read was high, there was motion

char veri[100];
//sprintf(veri, "%05d", denemesayisi);

 strcpy(veri,sensorsonuclari.c_str());
 Wire.write(veri); 
 veri[0]=char(0);
}
/*
void lcd()
{
  lcd.begin();
  lcd.backlight();
 
// Ekrana yazdırılacak metin
lcd.print("Proje Hocam");
}*/

