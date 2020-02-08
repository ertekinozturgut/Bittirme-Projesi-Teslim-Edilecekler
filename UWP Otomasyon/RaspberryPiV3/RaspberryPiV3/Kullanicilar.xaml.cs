using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.I2c;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=234238 adresinde açıklanmaktadır

namespace RaspberryPiV3
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine gezinebilecek boş bir sayfa.
    /// </summary>
    public sealed partial class Kullanicilar : Page
    {
        DispatcherTimer timer1 = new DispatcherTimer();

        public Kullanicilar()
        {   
            this.InitializeComponent();
            txtkullaniciid.Visibility = Visibility.Collapsed;
            lblkullaniciara.Visibility = Visibility.Collapsed;
            btnara.Visibility = Visibility.Collapsed;
            Initialiasecom();
            uygulama.veritbani.Kullanicilari_listele(datagridkullanicilar);
            timer1.Tick += timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 2000);
            timer1.Start();
        }
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
        DispatcherTimer timer = new DispatcherTimer();

        byte[] a = new byte[100];
        I2cDevice arduio;//I2C aracılığıyla haberleşilecek cihaz
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
            }
            if (i >= 3)
            {

                int sicaklik = Convert.ToInt32(veriler[2].Substring(0, veriler[2].IndexOf('.')));
                int nem = Convert.ToInt32(veriler[3].Substring(0, veriler[3].IndexOf('.')));
                uygulama.gadgetlar.Depo_Verileri(sicaklik, nem, Convert.ToInt32(veriler[4]), Convert.ToInt32(veriler[0]));
                DateTime dateTime = DateTime.Now;
            }
            i = 0;
        }


        private void btnanasayfa_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnaSayfa)); arduio.Dispose();

        }

        private void btnkullanicilar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Kullanicilar)); arduio.Dispose();

        }

        private void btnurunler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Urunler)); arduio.Dispose();

        }



        private void btniletisim_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Iletisim)); arduio.Dispose();

        }
        private void btnkayit_Click(object sender, RoutedEventArgs e)
        {
            string KullaniciSifre = "";
            string KullaniciEmail = "";
            string KullaniciAdi = "";
            int KullaniciId = 0;
            //   datagridkullanicilar.DataContext = kullanicilar;    
            //Kullanıcı Ekle Komutları
            if (btnswitch.IsOn && txtkullaniciid.Visibility==Visibility.Visible)
            {
                KullaniciId = Convert.ToInt32(txtkullaniciid.Text);
                KullaniciAdi = txtkadi.Text;
                KullaniciEmail = txtkemail.Text;
                KullaniciSifre = txtksifre.Text;
                uygulama.veritbani.kullanıcı_duzenle(KullaniciId,KullaniciAdi,KullaniciEmail,KullaniciSifre);
                GC.Collect();
            }
            else
            {
                #region
              

                try
                {
                    KullaniciAdi = txtkadi.Text;
                    KullaniciEmail = txtkemail.Text;
                    KullaniciSifre = txtksifre.Text;
                    uygulama.veritbani.kullanıcı_ekle(KullaniciAdi, KullaniciEmail, KullaniciSifre);
                    uygulama.veritbani.Kullanicilari_listele(datagridkullanicilar);
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    MessageDialog HataMesaji = new MessageDialog("Aşağıdaki Hatayla Karşılaşıldı\n" + ex + "\nLütfen Tekrar Deneyiniz");
                    HataMesaji.ShowAsync();
                }
                finally
                {
                    KullaniciAdi = "";
                    KullaniciEmail = "";
                    KullaniciSifre = "";
                    txtkadi.Text = "";
                    txtkemail.Text = "";
                    txtksifre.Text = "";
                    GC.Collect();
                }
                #endregion }
                if (txtkadi.Text != "" && txtkemail.Text != "" && txtksifre.Text != "")
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        MessageDialog a = new MessageDialog("İşlem gerçekleştirilirken bir hata meydana geldi", "" + ex);
                        a.ShowAsync();
                    }
                }
                string kad = txtkadi.Text;
                string kmail = txtkemail.Text;
                string ksifre = txtksifre.Text;
                uygulama.veritbani.kullanıcı_ekle(kad, kmail, ksifre);

                /*List<KullaniciData> kullanicilar = new List<KullaniciData>();
                  uygulama.veritbani.baglanti_durumu();
                  uygulama.veritbani.baglanti.Open();
                  uygulama.veritbani.komut = new MySqlCommand();
                  uygulama.veritbani.komut.Connection = uygulama.veritbani.baglanti;
                  uygulama.veritbani.komut.CommandText = "Select Kullanici_Id,Kullanici_Adı,Kullanici_Emaili From Kullanicilar Order By Kullanıcı_Adı";
                  uygulama.veritbani.oku = uygulama.veritbani.komut.ExecuteReader();
                  while (uygulama.veritbani.oku.Read())
                  {
                      // Windows.UI.Popups.MessageDialog message = new Windows.UI.Popups.MessageDialog(""+oku["Kullanici_Id"]+""+oku["Kullanici_Adi"]+""+ oku["Kullanici_Emaili"] + "");
                      // message.ShowAsync();
                      kullanicilar.Add(new KullaniciData { KullaniciId = uygulama.veritbani.oku["Kullanici_Id"].ToString(), KullaniciAdi = uygulama.veritbani.oku["Kullanici_Adi"].ToString(), KullaniciEmail = uygulama.veritbani.oku["Kullanici_Emaili"].ToString() });

                  }
                  datagridkullanicilar.DataContext = kullanicilar;
                  //  a.DataContext = kullanicilar;

                  uygulama.veritbani.oku.Close();
                  uygulama.veritbani.baglanti.Close();*/
            }
        }

        private void btnsifirla_Click(object sender, RoutedEventArgs e)
        {
            txtkadi.Text = "";
            txtkemail.Text = "";
            txtkullaniciid.Text = "";
            txtksifre.Text = "";
        }

        private void btnswitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (btnswitch.IsOn)
            {
                txtkullaniciid.Visibility = Visibility.Visible;
                lblkullaniciara.Visibility = Visibility.Visible;
                btnara.Visibility = Visibility.Visible;
            }
            if (!btnswitch.IsOn)
            {
                txtkullaniciid.Visibility = Visibility.Collapsed;
                lblkullaniciara.Visibility = Visibility.Collapsed;
                btnara.Visibility = Visibility.Collapsed;
            }
        }

        private void btnara_Click(object sender, RoutedEventArgs e)
        {
            //Kullanıcı Arama Kodlarının Bulunduğu kısım
            if (txtkullaniciid.Text != "")
            {
                #region
                string KullaniciAdi = txtkadi.Text;
                string KullaniciEmail = txtkemail.Text;
                string KullaniciSifre = txtksifre.Text;
                int KullaniciId = Convert.ToInt32(txtkullaniciid.Text);
                string[] kbilgiler = new string[3];
                kbilgiler = uygulama.veritbani.kullanici_bul(KullaniciId);
                txtkadi.Text = kbilgiler[0];
                txtkemail.Text = kbilgiler[1];
                txtksifre.Text = kbilgiler[2];
                #endregion
            }
            else
            {
                MessageDialog a = new MessageDialog("Lütfen geçerli bir Id giriniz", "Lütfen Tekrar Deneyin");
                a.ShowAsync();
            }

            // uygulama.veritbani.kullanıcı_duzenle(KullaniciId, KullaniciAdi, KullaniciEmail, KullaniciSifre);
            GC.Collect();
        }

        private void btndepo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DepoDurumu));
            arduio.Dispose();

        }

        private void btnbolgeler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Bolgeler)); arduio.Dispose();

        }

        private void btnarabalar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Depolar)); arduio.Dispose();

        }
    }
}

