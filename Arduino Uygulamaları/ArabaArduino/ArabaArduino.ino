#include <LiquidCrystal.h>
#include <LiquidCrystal_I2C_AvrI2C.h>
#define ag_ismi "PreoP1"
#define ag_sifresi "04081998"
LiquidCrystal_I2C_AvrI2C lcd(0x27,16,2);
#include <dht11.h> // dht11 kütüphanesini ekliyoruz.
#define DHT11PIN 8 // DHT11PIN olarak Dijital 2"yi belirliyoruz.
dht11 DHT11;
String baglantiKomutu;
void setup()
{
  Serial.begin(115200); //Seriport'u açıyoruz. Güncellediğimiz 
  lcd.begin();
  lcd.backlight();
  Serial.println("AT"); //ESP modülümüz ile bağlantı kurulup kurulmadığını kontrol ediyoruz.
  pinMode(13,OUTPUT);
  int alinan =0;
   alinan = Serial.read(); // Serial Porttan değer okuma
    Serial.print("Alinan Deger: ");
    Serial.print(alinan); // integer olarak alınan değeri yazdırma
    Serial.print(" - Char Olarak Alinan Deger: ");
    Serial.println((char)alinan); // char'a dönüştürerek alınan değeri yazdırma.
  delay(3000); //ESP ile iletişim için 3 saniye bekliyoruz.
 alinan =0;
   alinan = Serial.read(); // Serial Porttan değer okuma
    Serial.print("Alinan Deger: ");
    Serial.print(alinan); // integer olarak alınan değeri yazdırma
    Serial.print(" - Char Olarak Alinan Deger: ");
    Serial.println((char)alinan); // char'a dönüştürerek alınan değeri yazdırma.
  if(Serial.find("OK")){         //esp modülü ile bağlantıyı kurabilmişsek modül "AT" komutuna "OK" komutu ile geri dönüş yapıyor.
     Serial.println("AT+CWMODE=1"); //esp modülümüzün WiFi modunu STA şekline getiriyoruz. Bu mod ile modülümüz başka ağlara bağlanabilecek.
     delay(2000);
    baglantiKomutu=String("AT+CWJAP=\"")+ag_ismi+"\",\""+ag_sifresi+"\"";
    Serial.println(baglantiKomutu);

     delay(2000);
 }
 
  Serial.print("AT+CIPMUX=1\r\n");
   delay(200);
   Serial.print("AT+CIPSERVER=1,80\r\n");
   alinan =0;
   alinan = Serial.read(); // Serial Porttan değer okuma
    Serial.print("Alinan Deger: ");
    Serial.print(alinan); // integer olarak alınan değeri yazdırma
    Serial.print(" - Char Olarak Alinan Deger: ");
    Serial.println((char)alinan); // 
   delay(1000);
}
void loop(){
   int sicaklikDegeri= sicaklik();
   int nemDegeri=nem();
  if(Serial.available()>0){
    if(Serial.find("+IPD,")){
      String metin="<head><title>Page Title</title><script type=\"text/javascript\"> function codeAddress() { window.location.replace(\"http://ertechinsoftware.com/wp-content/ArabaAnlikVeri.php?arabaad=Ford&temp1="+String(sicaklikDegeri)+"&hum1="+String(nemDegeri)+"\");}</script> </head> <body onload=\"codeAddress()\"></body>";
      String cipsend = "AT+CIPSEND=";
      cipsend +="0";
      cipsend +=",";
      cipsend += metin.length();
      cipsend += "\r\n";
      delay(2000);
      Serial.print(cipsend);
      delay(2000);
      Serial.println(metin);
      led_yakma();
      Serial.println("AT+CIPCLOSE=0");
    }
  }
}

 int sicaklik()
 {
  Serial.println();
  int chk = DHT11.read(DHT11PIN);
  Serial.print("Sicaklik (Celcius): ");
  Serial.println((float)DHT11.temperature, 2);
  int a=((int)DHT11.temperature);
  delay(2000);

return a; 
 }
 int nem()
 {
  Serial.println();
  int chk = DHT11.read(DHT11PIN);
  Serial.print("Nem (%): ");
  Serial.println((float)DHT11.humidity, 2);

  int a=((int)DHT11.humidity);
  delay(2000);

return a; 
 }

