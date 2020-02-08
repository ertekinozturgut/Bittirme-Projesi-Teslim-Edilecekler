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
using Windows.UI.Xaml.Navigation;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=234238 adresinde açıklanmaktadır

namespace RaspberryPiV3
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine gezinebilecek boş bir sayfa.
    /// </summary>
    public sealed partial class Depolar : Page
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        public Depolar()
        {
            this.InitializeComponent();
            uygulama.veritbani.Araba_Listele(datagridarabalar);
            Initialiasecom();
            timer1.Tick += timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 2000);
            timer1.Start();
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
                uygulama.gadgetlar.Depo_Verileri(sicaklik,nem,Convert.ToInt32(veriler[4]),Convert.ToInt32(veriler[0]));
                DateTime dateTime = DateTime.Now;
            }
            i = 0;
        }
        private void btnkayit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string arabaad = txtarabaad.Text + "";
                string urunid = txturunid.Text + "";
                string urunstok = txturunstok.Text + "";
                uygulama.veritbani.Araba_ekle(arabaad, Convert.ToInt32(urunstok), Convert.ToInt32(urunid));
                arabaad = "";
                urunid = "";
                urunstok = "";
                txtarabaad.Text = "";
                txturunid.Text = "";
                txturunstok.Text = "";
                uygulama.veritbani.Araba_Listele(datagridarabalar);
                GC.Collect();
            }
            catch(Exception ex)
            {
                Windows.UI.Popups.MessageDialog a = new Windows.UI.Popups.MessageDialog(ex+"");
                a.ShowAsync();
            }          
        }

        private void btnsifirla_Click(object sender, RoutedEventArgs e)
        {
            txturunstok.Text = "";
            txtarabaad.Text = "";
            txturunid.Text = "";
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
        #region
        private void btnanasayfa_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnaSayfa));
        }      
        private void btnurunler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Urunler));
        }      
        private void btnkullanicilar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Kullanicilar));
        }
        private void btniletisim_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Iletisim));
        }
        private void btndepo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DepoDurumu));
        }
        private void btnbolgeler_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Bolgeler));
        }
        private void btnarabalar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Depolar));
        }
        #endregion
    }
}
