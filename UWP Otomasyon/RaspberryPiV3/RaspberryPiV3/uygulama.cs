using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.Devices.I2c;
using Windows.UI.Popups;
using System.Collections;

namespace RaspberryPiV3
{
    class uygulama
    {
        //Veritabanı bağlantı metni metnini tanımladık
        public static string baglantitxt = "datasource= 94.73.170.150;username=user_7621258; password=******;database=db_ertechinsoftware_com;";

        public static class veritbani
        {
            //Veritabanında komut çalıştırıp sonuçları okuyabileceğimiz nesneler üretildi.
            public static MySqlConnection baglanti = new MySqlConnection(uygulama.baglantitxt);
            public static MySqlCommand komut;
            public static MySqlDataReader oku;

            //Bağlantı açık problemlerini önlemek için 
            //Bağlantı açıksa kapatan bir fonksiyon yazıldı
            public static void baglanti_durumu()
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
            /// Listeleme Fonksiyonları

            //Depolar Combobox'ında gösterilecek depoları kaynağı
            public static void Depo_listele(List<String> a)
            {
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.CommandText = "SELECT Depo_Id FROM Depolar";
                komut.Connection = baglanti;
                oku = komut.ExecuteReader();
                while (oku.Read())

                {
                    a.Add(oku["Depo_Id"].ToString());
                }
                oku.Dispose();
                baglanti.Close();
            }
            //Bölgeler Combobox'ında gösterilecek bölgelerin kaynağı
            public static List<String> Bolge_listele(List<String> a)
            {
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.CommandText = "SELECT Bolge_Id FROM Bolgeler";
                komut.Connection = baglanti;
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    a.Add(oku["Bolge_Id"].ToString());
                    MessageDialog b = new MessageDialog("", "" + a[0]);
                }
                oku.Dispose();
                baglanti.Close();
                return a;
            }
            //Uygulamadaki Bölgeler tablosunda bölgelerin listelenmesi
            public static void Bolge_Listele(RadDataGrid a)
            {
                List<Bolgeler> bolgeler = new List<Bolgeler>();//Bölgeler classımız tipinde bir liste oluşturduk
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM Bolgeler";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    bolgeler.Add(new Bolgeler
                    {
                        BolgeId = Convert.ToInt32(oku["Bolge_Id"].ToString()),
                        BolgeAd = oku["Bolge_Adi"].ToString(),
                        BolgeDepo = Convert.ToInt32(oku["Bolge_Depo_Id"].ToString()),
                        BolgeUrunStok = Convert.ToInt32(oku["Bolge_Urun_Sayisi"].ToString())
                    });//Bu listeye veritabanından dönen verileri aktardık
                }
                a.DataContext = bolgeler;//Bölgeler tablosunun kaynağını bu liste olarak seçtik
                oku.Close();
                baglanti.Close();
            }

            //Uygulamadaki Kullanıcılar tablosunda kullanıcıların listelenmesi

            public static void Kullanicilari_listele(RadDataGrid a)
            {
                List<KullaniciData> kullanicilar = new List<KullaniciData>();//Kullanıcılar classımız tipinde bir liste oluşturduk
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Select Kullanici_Id,Kullanici_Adı,Kullanici_Emaili From Kullanicilar Order By Kullanıcı_Adı";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    kullanicilar.Add(new KullaniciData { KullaniciId = oku["Kullanici_Id"].ToString(), KullaniciAdi = oku["Kullanici_Adi"].ToString(), KullaniciEmail = oku["Kullanici_Emaili"].ToString() });
                }//Bu listeye veritabanından dönen verileri aktardık
                a.DataContext = kullanicilar;//Kullanıcılar tablosunun kaynağını bu liste olarak seçtik
                oku.Close();
                baglanti.Close();
            }
            //Uygulamadaki Ürünler tablosunda ürünlerin listelenmesi

            public static void urunleri_listele(RadDataGrid a)
            {
                List<UrunData> urunler = new List<UrunData>();//Ürünler classımız tipinde bir liste oluşturduk
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Select Urun_Son_Kullanma_Tarihi,Urun_Ideal_Sicaklik,Urun_Ideal_Nem,Urun_Stok_Adedi," +
                    "Urun_Bolge_Id,Urun_Depo_Id,Urun_Adi,Urun_Id , Urun_Max_Nem , Urun_Max_Sicaklik From Urunler Order By Urun_Id";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    urunler.Add(new UrunData { UrunId = oku["Urun_Id"].ToString(), UrunAdi = oku["Urun_Adi"].ToString(), UrunDepo = oku["Urun_Depo_Id"].ToString(), UrunBolge = oku["Urun_Bolge_Id"].ToString(), UrunNem = Convert.ToInt32(oku["Urun_Ideal_Nem"].ToString()), UrunSicaklik = Convert.ToInt32(oku["Urun_Ideal_Sicaklik"].ToString()), UrunSktarihi = Convert.ToDateTime(oku["Urun_Son_Kullanma_Tarihi"]), UrunStok = Convert.ToInt32(oku["Urun_Stok_Adedi"].ToString()), UrunMaxNem = Convert.ToInt32(oku["Urun_Max_Nem"].ToString()), UrunMaxSicaklik = Convert.ToInt32(oku["Urun_Max_Sicaklik"].ToString()) });
                }//Bu listeye veritabanından dönen verileri aktardık
                a.DataContext = urunler;//Ürünler tablosunun kaynağını bu liste olarak seçtik
                oku.Close();
                baglanti.Close();
            }
            //Kullanıcıya Id'sini geri döndüren fonksiyon
            //İlk girişte maille kullanıcıya iletmekte kullanılıyor
            public static string Kullanici_Id(string kad, string kemail)
            {
                string id = "";
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Kullanici_Id FROM Kullanicilar WHERE Kullanici_Adi= '" + kad + "' and Kullanici_Emaili='" + kemail + "' ORDER BY Kullanici_Id DESC LIMIT 1 ";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    id = oku["Kullanici_Id"].ToString();
                }
                baglanti.Close();
                GC.Collect();
                return id;
            }
            //Kullanıcı Id'sinden kullanıcı verileri getiren fonksiyon
            //Kullanıcı düzenlemek için gerekli olan default verileri getirmekte kullanılıyor
            public static string[] kullanici_bul(int id)
            {
                string[] b = new string[3];
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Kullanici_Adi , Kullanici_Emaili , Kullanici_Sifresi FROM Kullanicilar WHERE Kullanici_Id=" + id + "";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    b[0] = oku["Kullanici_Adi"].ToString();
                    b[1] = oku["Kullanici_Emaili"].ToString();
                    b[2] = oku["Kullanici_Sifresi"].ToString();
                }
                oku.Close();
                baglanti.Close();
                return b;
            }

            /*Sisteme girişte kullanılmaya çalışılan Kullanıcı Id ve Şifrenin
             eşleşip eşlemşmediği denetleniyor*/
            public static bool kullanici_giris_kontrol(int id, string sifre)
            {
                bool kont = false;
                string kullaniciid = "";
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Kullanici_Id FROM Kullanicilar WHERE Kullanici_Id=" + id + " and Kullanici_Sifresi='" + sifre + "'";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    kullaniciid = oku["Kullanici_Id"].ToString() + "";
                }
                baglanti.Close();
                if (kullaniciid == id.ToString())
                {
                    kont = true;
                }
                if (kullaniciid != id.ToString())
                {
                    kont = false;
                }
                return kont;
            }
            //Ortam şartlarına uyumsuz ürünleri listeleyen fonksiyon
            public static void urun_kontrol(int sicaklik, int nem, DateTime sktarihi, RadDataGrid a)
            {
                string MySQLFormatDate = sktarihi.ToString("yyyy-MM-dd HH:mm:ss");//Tarih verisini MYSQL'e uyarladık
                List<UrunData> urunler = new List<UrunData>();//UrunData classımız tipinde bir liste oluşturduk
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM Urunler WHERE Urun_Max_Nem<=" + nem + " OR Urun_Ideal_Nem>=" + nem + " OR Urun_Max_Sicaklik<=" + sicaklik + " OR Urun_Ideal_Sicaklik >=" + sicaklik + " OR DATE(Urun_Son_Kullanma_Tarihi)<='" + MySQLFormatDate + "'";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    urunler.Add(new UrunData
                    {
                        UrunId = oku["Urun_Id"].ToString(),
                        UrunAdi = oku["Urun_Adi"].ToString(),
                        UrunDepo = oku["Urun_Depo_Id"].ToString(),
                        UrunBolge = oku["Urun_Bolge_Id"].ToString(),
                        UrunNem = Convert.ToInt32(oku["Urun_Ideal_Nem"].ToString()),
                        UrunSicaklik = Convert.ToInt32(oku["Urun_Ideal_Sicaklik"].ToString()),
                        UrunSktarihi = Convert.ToDateTime(oku["Urun_Son_Kullanma_Tarihi"]),
                        UrunStok = Convert.ToInt32(oku["Urun_Stok_Adedi"].ToString()),
                        UrunMaxSicaklik = Convert.ToInt32(oku["Urun_Max_Sicaklik"].ToString()),
                        UrunMaxNem = Convert.ToInt32(oku["Urun_Max_Nem"].ToString())
                    });
                }
                if (urunler.Count == 0)
                {
                }
                else
                {
                    a.DataContext = urunler;//Hatalı Ürünler tablomuzun veri kaynağını listemiz yaptık
                }
                oku.Close();
                baglanti.Close();
            }
            //Ana sayfadaki hatalı ürünler listboxın içine atacağımız
            //hatalı ürünleri hazırlayan fonksiyon
            public static ArrayList hatali_urunler(int sicaklik, int nem, DateTime sktarihi)
            {
                ArrayList urunler = new ArrayList();
                string MySQLFormatDate = sktarihi.ToString("yyyy-MM-dd");
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM Urunler WHERE Urun_Max_Nem<=" + nem + " OR Urun_Ideal_Nem>=" + nem + " OR Urun_Max_Sicaklik<=" + sicaklik + " OR Urun_Ideal_Sicaklik >=" + sicaklik + " OR DATE(Urun_Son_Kullanma_Tarihi)<='" + MySQLFormatDate + "'";

                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    urunler.Add(oku["Urun_Id"].ToString() + "  " + oku["Urun_Adi"].ToString());
                }
                oku.Close();
                baglanti.Close();
                return urunler;
            }

            public static void Araba_ekle(string ArabaAd, int stok, int urunid)
            {
                baglanti_durumu();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "INSERT INTO `Arabalar`(`Araba_Ad`) VALUES ('" + ArabaAd + "')";
                komut.ExecuteNonQuery();
                komut.CommandText = "INSERT INTO `Tasimalar`(`Araba_Ad`, `Urun_Id`, `Urun_Stok_Adedi`) VALUES ('" + ArabaAd + "','" + urunid + "','" + stok + "')";
                komut.ExecuteNonQuery();
                komut.CommandText = "UPDATE `Urunler` SET Urun_Stok_Adedi=Urun_Stok_Adedi-" + stok + " WHERE Urun_Id='" + urunid + "'";
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            //Uygulamadaki Arabalar tablosunda arabaların listelenmesi

            public static void Araba_Listele(RadDataGrid a)
            {
                List<Arabalar> arabalar = new List<Arabalar>();//Arabalar classımız tipinde bir liste oluşturduk
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Select * From Tasimalar Order By Araba_Ad";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    arabalar.Add(new Arabalar { ArabaAdi = oku["Araba_Ad"].ToString(), UrunId = Convert.ToInt32(oku["Urun_Id"].ToString()), UrunStok = Convert.ToInt32(oku["Urun_Stok_Adedi"].ToString()) });
                }//Bu listeye veritabanından dönen verileri aktardık
                a.DataContext = arabalar;//Arabalar tablosunun kaynağını bu liste olarak seçtik
                oku.Close();
                baglanti.Close();
            }

            //Uygulamada sayfanın en üstünde görünen araba bilgisini sağlayan fonksiyon
            public static ArrayList Araba_Bilgisi()
            {
                ArrayList array = new ArrayList();
                baglanti_durumu();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Araba_Ad,Sicaklik, Nem FROM ArabaVerileri ORDER BY Veri_Id DESC LIMIT 1 ";

                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    array.Add(oku["Araba_Ad"].ToString());
                    array.Add(oku["Sicaklik"].ToString());
                    array.Add(oku["Nem"].ToString());
                }
                baglanti.Close();
                oku.Close();

                return array;
            }
            //Araba içindeki ürün uygun şartlarda mı saklanmakta bunu tespit eden fonksiyon
            public static bool Araba_Durumu()
            {
                bool kont = false;
                int minsicaklik = 0;
                int maxsicaklik = 0;
                int minnem = 0;
                int maxnem = 0;
                int urunid = 0;
                baglanti_durumu();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Select Urun_Id,Urun_Ideal_Nem, Urun_Ideal_Sicaklik, Urun_Max_Sicaklik, Urun_Max_Nem FROM Urunler Where Urun_Id =" +
                    " (SELECT Urun_Id FROM Tasimalar WHERE Araba_Ad = (SELECT Araba_Ad FROM ArabaVerileri ORDER BY Veri_Id DESC LIMIT 1 ) )";
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    minnem = Convert.ToInt32(oku["Urun_Ideal_Nem"].ToString());
                    maxnem = Convert.ToInt32(oku["Urun_Max_Nem"].ToString());
                    minsicaklik = Convert.ToInt32(oku["Urun_Ideal_Sicaklik"].ToString());
                    maxsicaklik = Convert.ToInt32(oku["Urun_Max_Sicaklik"].ToString());
                    urunid = Convert.ToInt32(oku["Urun_Id"].ToString());
                }//Arabadaki ürünün saklanma koşullarıyla ilgili verileri çekiyoruz
                komut.CommandText = "SELECT Sicaklik, Nem FROM ArabaVerileri ORDER BY Veri_Id DESC LIMIT 1 ";
                while (oku.Read())
                {
                    if (Convert.ToInt32(oku["Sicaklik"].ToString()) >= minsicaklik &&
                        Convert.ToInt32(oku["Nem"].ToString()) >= minnem
                        && Convert.ToInt32(oku["Sicaklik"].ToString()) <= maxsicaklik &&
                        Convert.ToInt32(oku["Nem"].ToString()) <= maxnem)
                    {
                        kont = true;
                    }
                    else
                    {
                        kont = false;
                    }
                }//Verilerle arabanın verileri arasında kıyaslama yapıp
                //ürünün bir problemi olup olmadığını tespit ettik
                baglanti.Close();
                oku.Close();

                return kont;
            }
            //Veritabanına ürün eklememize yardım eden fonksiyonumuz
            public static void urun_ekle(string UrunAdi, DateTime UrunSktarihi, int UrunDepo, int UrunBolge, int UrunStok, int UrunSicaklik, int UrunNem, int UrunMaxNem, int UrunMaxSicaklik)
            {
                string MySQLFormatDate = UrunSktarihi.ToString("yyyy-MM-dd HH:mm:ss");
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Insert into Urunler(Urun_Adi,Urun_Depo_Id,Urun_Bolge_Id,Urun_Stok_Adedi,Urun_Ideal_Nem,Urun_Ideal_Sicaklik,Urun_Son_Kullanma_Tarihi,Urun_Max_Nem,Urun_Max_Sicaklik) values('" + UrunAdi + "','" + UrunDepo + "','" + UrunBolge + "','" + UrunStok + "','" + UrunNem + "','" + UrunSicaklik + "','" + MySQLFormatDate + "','" + UrunMaxNem + "','" + UrunMaxSicaklik + "')";
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            //Veritabanına kullanıcı eklememize yaraya fonksiyonumuz
            public static void kullanıcı_ekle(string KullaniciAdi, string KullaniciEmaili, string KullaniciSifre)
            {
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Insert into Kullanicilar (Kullanici_Adi,Kullanici_Emaili,Kullanici_Sifresi) values ('" + KullaniciAdi + "','" + KullaniciEmaili + "','" + KullaniciSifre + "')";
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            //Veritabanına bölge eklememize yaraya fonksiyonumuz

            public static void bolge_ekle(string BolgeAdi, int BolgeDepo, int BolgeUrunSayisi)
            {
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Insert Into Bolgeler(Bolge_Adi,Bolge_Depo_Id,Bolge_Urun_Sayisi) values('" + BolgeAdi + "','" + BolgeDepo + "', 0 )";
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            //Veritabanında kullanıcı bilgilerini düzenlememize yaraya fonksiyonumuz
            public static void kullanıcı_duzenle(int id, string KullaniciAdi, string KullaniciEmail, string KullaniciSifre)
            {
                baglanti_durumu();
                baglanti.Open();
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "UPDATE Kullanicilar Set Kullanici_Adi='" + KullaniciAdi + "', Kullanici_Emaili='" + KullaniciEmail + "', Kullanici_Sifresi='" + KullaniciSifre + "' Where Kullanici_Id=" + id + "";
                komut.ExecuteNonQuery();
                baglanti.Close();
            }


        }

        public static class gadgetlar
        {
            private static I2cDevice arduio; // Used to Connect to Arduino
            private static DispatcherTimer timer = new DispatcherTimer();
            public static byte[] a = new byte[2];
            public static async void Initialiasecom()
            {
                var settings = new I2cConnectionSettings(0x40); // Slave Address of Arduino Uno 

                settings.BusSpeed = I2cBusSpeed.FastMode; // this bus has 400Khz speed

                string aqs = I2cDevice.GetDeviceSelector("I2C1"); // This will return Advanced Query String which is used to select i2c device
                var dis = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(aqs);
                arduio = await I2cDevice.FromIdAsync(dis[0].Id, settings);
                a = new byte[2];
                timer.Tick += Timer_Tick; // We will create an event handler 
                timer.Interval = new TimeSpan(0, 0, 0, 0, 500); // Timer_Tick is executed every 500 milli second
                timer.Start();

            }
            public static async void Timer_Tick(object sender, object e)
            {
                try
                {
                    arduio.Read(a); // this funtion will read data from Arduino 
                }
                catch (Exception p)
                {
                    Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(p.Message);
                    await msg.ShowAsync(); // this will show error message(if Any)
                }
            }

            public static void Depo_Verileri(int sicaklik, int nem, int gaz, int hareket)
            {
                uygulama.veritbani.baglanti_durumu();
                uygulama.veritbani.baglanti.Open();
                uygulama.veritbani.komut = new MySqlCommand();
                uygulama.veritbani.komut.Connection = uygulama.veritbani.baglanti;
                uygulama.veritbani.komut.CommandText = "INSERT INTO `SensorVerileri`(PIR, Sicaklik, Nem, Gaz) VALUES ('" + hareket + "','" + sicaklik + "','" + nem + "','" + gaz + "')";
                uygulama.veritbani.komut.ExecuteNonQuery();
                uygulama.veritbani.baglanti.Close();
                GC.Collect();
            }
        }
        public class UrunData
        {
            public string UrunId { get; set; }
            public string UrunAdi { get; set; }
            public DateTime UrunSktarihi { get; set; }
            public string UrunDepo { get; set; }
            public string UrunBolge { get; set; }
            public int UrunSicaklik { get; set; }
            public int UrunNem { get; set; }
            public int UrunStok { get; set; }
            public int UrunMaxNem { get; set; }
            public int UrunMaxSicaklik { get; set; }
        }
        public class KullaniciData
        {
            public string KullaniciId { get; set; }
            public string KullaniciAdi { get; set; }
            public string KullaniciEmail { get; set; }
        }
        public class Arabalar
        {
            public string ArabaAdi { get; set; }
            public int UrunId { get; set; }
            public int UrunStok { get; set; }

        }
        public class Bolgeler
        {
            public int BolgeId { get; set; }
            public string BolgeAd { get; set; }
            public int BolgeDepo { get; set; }
            public int BolgeUrunStok { get; set; }

        }
    }
}


