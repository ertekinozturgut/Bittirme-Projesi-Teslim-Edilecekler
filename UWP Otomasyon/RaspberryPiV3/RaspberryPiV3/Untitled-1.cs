  DispatcherTimer timer = new DispatcherTimer();

        byte[] a = new byte[100];//arduino verilerinin aktarılacağı byte dizisi
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
        //Veritabanına sürekli arduinodan çekmekte olduğu sensör verilerini ekleyen fonksiyonumuz
        public async void Timer_Tick(object sender, object e)
        {
            try
            {
                arduio.Read(a);//Burda arduinodan gelen veriyi okumakta ve byte dizisi şeklindeki a değişkenine atamaktadır

            }
            catch (Exception ex)
            {
            }
            //arduinodan byte olarak aldığım verileri stringe çevirdik
            string str1 = System.Text.ASCIIEncoding.ASCII.GetString(a);
            //str1 verisini '/' lar ile parçlara ayırıp her bir parçayı bir diziye ekledik
            string[] veri = str1.Split('/');
            string[] veriler = new string[5];
            int i = 0;
            //veri değişkeninden gelebilecek fazla bitleri '?' ile gösteren arduinonun bu işlemine karşın
            //eski veri dizisini kullanarak ? olmayan yeni bir veriler dizisi oluşturtuk
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
            //sistemde min 3 sensör olacağı için eğer en az 3 sensör verisi alınmıyorsa
            //bu şart sağlanana kadar sistemde her hangi bir işlem yapılmıyor 
            if (i >= 3)
            {

                int sicaklik = Convert.ToInt32(veriler[2].Substring(0, veriler[2].IndexOf('.')));
                int nem = Convert.ToInt32(veriler[3].Substring(0, veriler[3].IndexOf('.')));
                uygulama.gadgetlar.Depo_Verileri(sicaklik, nem, Convert.ToInt32(veriler[4]), Convert.ToInt32(veriler[0]));
                DateTime dateTime = DateTime.Now;
            }
            i = 0;
        }
           //Araba durumunu sürekli ekranda gösterten timerımız
        public async void timer1_Tick(object sender, object e)
        {    
            try
            {
                bool arabakont = false;
                arabakont = uygulama.veritbani.Araba_Durumu();
                txtarabadurum.Text = (arabakont == true) ? txtarabadurum.Text = "Araba Durumu : Pozitif" : txtarabadurum.Text = "Araba Durumu : Negatif";
                ArrayList arababilgisi = new ArrayList();
                arababilgisi = uygulama.veritbani.Araba_Bilgisi();
                txtarabad.Text = "Araba Adı : " + arababilgisi[0] + "";
                txtarabasicaklik.Text = "Araba Sıcaklık Değeri : " + arababilgisi[1] + "";
                txtarabanem.Text = "Araba Nem Değeri : " + arababilgisi[2] + "";
                GC.Collect();//ramden kazanç sağlamamızı sağlayan Garbage collect fonksiyonu
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog c = new Windows.UI.Popups.MessageDialog("" + ex, "Hata ile Karşılaşıldı");
                c.ShowAsync();
            }
        }
    
    //Sayfa geçişlerini sağlayan ve her sayfa geçişinde Arduino bağlantısını kapatıp yeni sayfada tekrar başlatılmasına hazır eden fonksiyonlar
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
        private void btnanasayfa_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AnaSayfa)); arduio.Dispose();

        }
        private void btndepo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DepoDurumu)); arduio.Dispose();

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
