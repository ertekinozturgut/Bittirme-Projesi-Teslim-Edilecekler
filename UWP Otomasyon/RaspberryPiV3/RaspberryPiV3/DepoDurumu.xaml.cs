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

 
    public sealed partial class DepoDurumu : Page
    {
        byte[] a = new byte[2];
        I2cDevice arduio;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        //gecici
        int ornekSicaklik = 25;
        int ornekNem = 35;
        int ornekGaz = 1;
        int ornekHareket = 1;
        
        bool arabakont = false;
        bool urunkont = false;
        bool depokont = false;
        bool donanimkont = false;
        int depokontkatsayisi = 1;
        int donanimkontkatsayisi = 1;
        public DepoDurumu()
        {
            this.InitializeComponent();
            Initialiasecom();
            timer1.Tick += Timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer1.Start();
            timer2.Tick += timer1_Tick;
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 2000);
            timer2.Start();
        }
        private void btnanasayfa_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnaSayfa));
            arduio.Dispose();

        }

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

        public async void Initialiasecom()
        {
            var settings = new I2cConnectionSettings(0x40); // Arduino Uno Slave Adresi

            settings.BusSpeed = I2cBusSpeed.FastMode;

            string aqs = I2cDevice.GetDeviceSelector("I2C1");
            var dis = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(aqs);
            arduio = await I2cDevice.FromIdAsync(dis[0].Id, settings);
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
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
       public async void Timer1_Tick(object sender, object e)
        {
            arabakont = uygulama.veritbani.Araba_Durumu();
            uygulama.veritbani.urun_kontrol(ornekSicaklik, ornekNem, DateTime.Now, datagridurunler);
            urunkont = (datagridurunler.DataContext==null) ? true : false;
            depokont = (depokontkatsayisi == 0 || urunkont == false) ? false : true;
            donanimkont = (donanimkontkatsayisi == 1)?true:false;
            if (donanimkont==true)
            {
                imgdonanimpoz.Visibility = Visibility.Visible;
                imgdonanimneg.Visibility = Visibility.Collapsed;
            }
            if(donanimkont==false)
            {
                imgdonanimpoz.Visibility = Visibility.Collapsed;
                imgdonanimneg.Visibility = Visibility.Visible;
            }
            if(arabakont==true)
            {
                imgaracpoz.Visibility = Visibility.Visible;
                imgaracneg.Visibility = Visibility.Collapsed;
            }
            if(arabakont==false)
            {
                imgaracpoz.Visibility = Visibility.Collapsed;
                imgaracneg.Visibility = Visibility.Visible;
            }
            if(urunkont==true)
            {
                imggidapoz.Visibility = Visibility.Visible;
                imggidaneg.Visibility = Visibility.Collapsed;
            }
            if(urunkont==false)
            {
                imggidaneg.Visibility = Visibility.Visible;
                imggidapoz.Visibility = Visibility.Collapsed;
            }
            if(depokont==true)
            {
                imgdeponeg.Visibility = Visibility.Collapsed;
                imgdepopoz.Visibility = Visibility.Visible;
                depokontkatsayisi = 1;
            }
            if(depokont==false)
            {
                imgdepopoz.Visibility = Visibility.Collapsed;
                imgdeponeg.Visibility = Visibility.Visible;
                depokontkatsayisi = 1;
            }
        }
       public async void Timer_Tick(object sender, object e)
       {
            donanimkont = true;
            try
            {
                arduio.Read(a);
            }
            catch (Exception p)
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
            }

           
            if (i >= 3)
            {
                int sicaklik = Convert.ToInt32(veriler[2].Substring(0, veriler[2].IndexOf('.')));
                int nem = Convert.ToInt32(veriler[3].Substring(0, veriler[3].IndexOf('.')));
             
                if (Convert.ToInt32(veriler[4]) < 400)
                {
                    depokontkatsayisi*= 1;
                }
                if (Convert.ToInt32(veriler[4]) >= 400)
                {
                    depokontkatsayisi *= 0;
                }
                donanimkontkatsayisi = 1;
                uygulama.gadgetlar.Depo_Verileri(sicaklik, nem, Convert.ToInt32(veriler[4]), Convert.ToInt32(veriler[0]));
            }
            else
            {
                donanimkontkatsayisi = 0;
            }
            i = 0;
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
    }
}
