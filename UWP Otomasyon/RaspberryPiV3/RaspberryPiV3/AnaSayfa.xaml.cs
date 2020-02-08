using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.I2c;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=234238 adresinde açıklanmaktadır

namespace RaspberryPiV3
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine gezinebilecek boş bir sayfa.
    /// </summary>
    public sealed partial class AnaSayfa : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer1 = new DispatcherTimer();
     
        I2cDevice arduio;
        public AnaSayfa()
        {
            this.InitializeComponent();
            Initialiasecom();
            imggaz.Visibility = Visibility.Collapsed;
            imggaz1.Visibility = Visibility.Collapsed;
            timer1.Tick += timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 2000);
            timer1.Start();
        }


        //Arabanın sensör ve durum bilgilerini sürekli ekranda gösteren fonksiyon
        public async void timer1_Tick(object sender, object e)
        {
            try
            {
                bool arabakont = false;
                arabakont = uygulama.veritbani.Araba_Durumu();
                txtarabadurum.Text = (arabakont == true) ? txtarabadurum.Text = "Araba Durumu : Pozitif" : txtarabadurum.Text = "Araba Durumu : Negatif";
                ArrayList arababilgisi = new ArrayList();
                arababilgisi = uygulama.veritbani.Araba_Bilgisi();
                txtarabad.Text ="Araba Adı : "+arababilgisi[0] + "";
                txtarabasicaklik.Text ="Araba Sıcaklık Değeri : "+ arababilgisi[1] + "";
                txtarabanem.Text = "Araba Nem Değeri : "+arababilgisi[2] + "";
                GC.Collect();
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog c = new Windows.UI.Popups.MessageDialog("" + ex, "Hata ile Karşılaşıldı");
                c.ShowAsync();
            }
        }
        #region
        //Sayfa geçişleri fonksiyonları
        private void btnkullanicilar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Kullanicilar));
            arduio.Dispose();
        }

        private void btnurunler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Urunler));
            arduio.Dispose();

        }

        private void btniletisim_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Iletisim));
            arduio.Dispose();

        }
        private void btnanasayfa_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnaSayfa));
            arduio.Dispose();

        }

        private void btndepo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DepoDurumu));
            arduio.Dispose();
        }
        private void btnbolgeler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Bolgeler));
            arduio.Dispose();
        }
        private void btnarabalar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Depolar));
            arduio.Dispose();

        }
        #endregion

        byte[] a = new byte[100];
        //I2C aracılığıyla haberleşilecek cihaza erişimi sağlayacak fonksiyon
        public async void Initialiasecom()
        {
            try
            {
                var settings = new I2cConnectionSettings(0x40); // Arduino Uno Slave Adresi
                settings.BusSpeed = I2cBusSpeed.FastMode;
                string aqs = I2cDevice.GetDeviceSelector("I2C1");
                var dis = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(aqs);
                arduio = await I2cDevice.FromIdAsync(dis[0].Id, settings);
                timer.Tick += Timer_Tick;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                timer.Start();//I2C'ten gelen verileri okuyacak 
                //ve bunlardan yola çıkaracak uyarı gösterecek olan timer'ımızı başlattık
            }
            catch (Exception ex)
            { }
            }
        //Bu kısımda 
        //1)Arduinodan sürekli sensör verileri çekiliyor
        //2)Veritabanına sürekli sensör verileri kaydediliyor
        //3)Sensörlerden gelen verilere göre ekranda bildirimler gösteriyor
        public async void Timer_Tick(object sender, object e)
        {
            try
            {
                arduio.Read(a);//Burda arduinodan gelen veriyi okumakta ve byte dizisi şeklindeki a değişkenine atamaktadır

            }
            catch (Exception ex)
            {             
            }
            string str1 = System.Text.ASCIIEncoding.ASCII.GetString(a);
            string[] veri = str1.Split('/');
            string[] veriler = new string[5];
            //  txtveriler1.Text = "";
            int i = 0;
            foreach (string value in veri)
            {
                string degisken = "";
                degisken = value;
                if (value.Contains('?'))
                {
                    degisken = value.Substring(0, value.IndexOf('?'));
                }
                veriler[i] = degisken;
                i++;
                txttumveriler.Text += degisken + "+";
            }
            txttumveriler.Text = str1;
            if (i >= 3)
            {
                if (veriler[0] == "1")
                {
                    txthareket.Text = "Hareket Algılandı";
                }
                if (veriler[0] == "0")
                {
                    txthareket.Text = "Hareket Yok";
                }
                int sicaklik = Convert.ToInt32(veriler[2].Substring(0, veriler[2].IndexOf('.')));
                int nem = Convert.ToInt32(veriler[3].Substring(0, veriler[3].IndexOf('.')));
                prognem.Value = sicaklik;
                progsicaklik.Value = nem;
                txtsicaklik.Text = "Sıcaklık : " + sicaklik;
                txtnem.Text = "Nem : " + nem;
                if (Convert.ToInt32(veriler[4]) < 400)
                {
                    imggaz.Visibility = Visibility.Visible;
                    imggaz1.Visibility = Visibility.Collapsed;
                }
                if (Convert.ToInt32(veriler[4]) >= 400)
                {
                    imggaz.Visibility = Visibility.Collapsed;
                    imggaz1.Visibility = Visibility.Visible;
                }
                uygulama.gadgetlar.Depo_Verileri(sicaklik,nem,Convert.ToInt32(veriler[4]),Convert.ToInt32(veriler[0]));
                DateTime dateTime = DateTime.Now;
                ArrayList urunler = uygulama.veritbani.hatali_urunler(sicaklik, nem, dateTime);
                if (urunler.Count < 1)
                {
                }
                else
                {
                    foreach (Object urun in urunler)
                    {
                        listhataliurunler.Items.Add(urun);
                    }
                }
            }
            i = 0;
        }
        //hatalı ürünler listboxında seçilen hatalı ürünün bilgilerini
        //kullanıcıya göstermeye yarayan fonksiyon
        private void listhataliurunler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                    String metin = "";
                    uygulama.veritbani.baglanti_durumu();
                    uygulama.veritbani.baglanti.Open();
                    uygulama.veritbani.komut = new MySql.Data.MySqlClient.MySqlCommand();
                    uygulama.veritbani.komut.Connection = uygulama.veritbani.baglanti;
                    uygulama.veritbani.komut.CommandText = "Select Urun_Son_Kullanma_Tarihi,Urun_Ideal_Sicaklik,Urun_Ideal_Nem,Urun_Stok_Adedi,Urun_Bolge_Id,Urun_Depo_Id,Urun_Adi,Urun_Id , Urun_Max_Nem , Urun_Max_Sicaklik From Urunler WHERE Urun_Id=" + Convert.ToInt32(listhataliurunler.SelectedItem.ToString().Substring(0, listhataliurunler.SelectedItem.ToString().IndexOf(" "))) + "";
                    uygulama.veritbani.oku = uygulama.veritbani.komut.ExecuteReader();
                    while (uygulama.veritbani.oku.Read())
                    {
                        metin = "Ürün Id = " + uygulama.veritbani.oku["Urun_Id"].ToString() + "\n"
                            + "Ürün Adı = " + uygulama.veritbani.oku["Urun_Adi"].ToString() + "\n"
                            + "Ürün Depo Id = " + uygulama.veritbani.oku["Urun_Depo_Id"].ToString() + "\n"
                            + "Ürün Bölge Id = " + uygulama.veritbani.oku["Urun_Bolge_Id"].ToString() + "\n"
                            + "Ürün İdeal Min. - Max. Nem = " + uygulama.veritbani.oku["Urun_Ideal_Nem"].ToString() + "-" + uygulama.veritbani.oku["Urun_Max_Nem"].ToString() + "\n"
                            + "Ürün İdeal Min. - Max. Sıcaklık =" + uygulama.veritbani.oku["Urun_Ideal_Sicaklik"].ToString() + "-" + uygulama.veritbani.oku["Urun_Max_Sicaklik"].ToString() + "\n"
                            + "Ürün Son Kullanma Tarihi = " + uygulama.veritbani.oku["Urun_Son_Kullanma_Tarihi"].ToString();
                    }
                    uygulama.veritbani.oku.Close();
                    uygulama.veritbani.baglanti.Close();
                Windows.UI.Popups.MessageDialog a;
                if (listhataliurunler.SelectedItem.ToString().Contains("Arabada"))
                {
                         a = new Windows.UI.Popups.MessageDialog("" + metin, "Arabada Bulunan Bu Ürünün Acilen İncelenmesi Gerekmektedir");                         
                }
                else
                {
                         a = new Windows.UI.Popups.MessageDialog("" + metin, "Depoda Bulunan Bu Ürünün Acilen İncelenmesi Gerekmektedir");
                }
                a.ShowAsync();
                }                            
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog b = new Windows.UI.Popups.MessageDialog("" + ex, "");
            }
            }        
    }
}

