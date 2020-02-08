using LightBuzz.SMTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
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
    public sealed partial class Kullanicikayit : Page
    {
        public Kullanicikayit()
        {
            this.InitializeComponent();
        }
        string kid = "";
        string kad = "";
        string kemail ="";
        string ksifre = "";
        private void btnkayit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtkullaniciad.Text != "" && txtkullanicimail.Text != "" && txtsifre.Password != "")
                {
                    #region
                    kad = txtkullaniciad.Text;
                    kemail = txtkullanicimail.Text;
                    ksifre = txtsifre.Password;
                    uygulama.veritbani.kullanıcı_ekle(kad, kemail, ksifre);
                    kid = uygulama.veritbani.Kullanici_Id(kad, kemail);
                    mailgonderAsync();
                    kid = "";
                    kad = "";
                    kemail = "";
                    GC.Collect();
                    this.Frame.Navigate(typeof(MainPage));
                    #endregion

                }
                else
                {
                    MessageDialog a = new MessageDialog("Hatayla Karşılaşıldı!","Lütfen Gerekli Alanları Doldurunuz!");
                }
            }
            catch(Exception ex)
            {
                MessageDialog a = new MessageDialog("Aşağıdaki Hatayla Karşılaşıldı",""+ex);
                a.ShowAsync();
            }
          

        }
        public async Task mailgonderAsync()
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 465, true, "rpidepootomasyonudestek@gmail.com", "rpidepo04062018"))
                {
                    EmailMessage emailMessage = new EmailMessage();

                    emailMessage.To.Add(new EmailRecipient(""+txtkullanicimail.Text));

                    emailMessage.Subject = "Yeni Üyelik Kaydı";
                    emailMessage.Body = "Kullanıcı Id : "+kid +"" + "\n" +
                        "Şifreniz : "+ksifre;

                    await client.SendMailAsync(emailMessage);
                    
                    MessageDialog c = new MessageDialog("", "Kullanıcı Bilgileriniz Mailinize Gönderildi");
                    c.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                MessageDialog a = new MessageDialog("" + ex, "Mail Gönderiminde Hata Okuştu");
                a.ShowAsync();
            }

        }
    }
}
