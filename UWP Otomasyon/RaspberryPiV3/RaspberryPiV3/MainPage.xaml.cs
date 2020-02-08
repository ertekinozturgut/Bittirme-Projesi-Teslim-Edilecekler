using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x41f adresinde açıklanmaktadır

namespace RaspberryPiV3
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine taşınabilecek boş bir sayfa.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        byte[] c = new byte[2];

        public MainPage()
        {
            this.InitializeComponent();
          //  Initialiasecom();
           // uygulama.gadgetlar.Arduino_Haberlesme(ref c);
            //MessageDialog a = new MessageDialog("veriler",""+c[0]);
            //a.ShowAsync();
        }
    
        private void btngiris_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region
                string ksifre = txtksifre.Password;
                int kid = Convert.ToInt32(txtkad.Text);
                bool giriskont;
                giriskont = uygulama.veritbani.kullanici_giris_kontrol(kid, ksifre);
                if (giriskont)
                {
                    GC.Collect();
                    this.Frame.Navigate(typeof(AnaSayfa));

                }
                else
                {
                    MessageDialog hata = new MessageDialog("Hatalı Giriş Yaptınız", "Lütfen Tekrar Deneyiniz");
                    hata.ShowAsync();
                }
            }

            #endregion
            catch (Exception ex)
            {
                MessageDialog a = new MessageDialog("", "" + ex);
                a.ShowAsync();
            }
            GC.Collect();
        }



        private void btnkayit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Kullanicikayit));   
        }
    }
}
