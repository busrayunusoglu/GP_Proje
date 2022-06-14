using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _1180505023_Büşra_Yunusoğlu_GP_Projesi
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        //Access veritabanımızı bağladık.
        OleDbConnection vtBaglantisi = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=" +
            Application.StartupPath + "\\Amore_Boutique.accdb");

        //Formlar arası veri aktarımında kullanacağımız değişkenler:
        public static string id, ad, soyad, yetki, kullanici_adi, sifre;

        private void button2_Click(object sender, EventArgs e)
        {
            vtBaglantisi.Open();
            this.Hide();
            KayitEkrani kayitOl = new KayitEkrani();
            kayitOl.Show();
            vtBaglantisi.Close();
        }



        bool girisYapabilmeDurumu = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /*button1'e tıkladığımız zaman hangi yetkiye göre giriş yapılmak istenirse oraya yönlendirilir. Kimin adına göre
        giriş yapılırsa onun ekranı açılır.*/
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                vtBaglantisi.Open();
                OleDbCommand selectSorgusu = new OleDbCommand("SELECT * FROM kullanicilar", vtBaglantisi);
                OleDbDataReader kayitOku = selectSorgusu.ExecuteReader();

                while (kayitOku.Read() == true)
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitOku["kullanici_adi"].ToString() == textBox1.Text &&
                            kayitOku["sifre"].ToString() == textBox2.Text &&
                            kayitOku["yetki"].ToString() == "Yönetici")
                        {
                            girisYapabilmeDurumu = true;
                            id = kayitOku.GetValue(0).ToString();
                            ad = kayitOku.GetValue(1).ToString();
                            soyad = kayitOku.GetValue(2).ToString();
                            yetki = kayitOku.GetValue(3).ToString();
                            kullanici_adi = kayitOku.GetValue(4).ToString();
                            sifre = kayitOku.GetValue(5).ToString();


                            this.Hide();

                            YoneticiEkrani yonetici = new YoneticiEkrani();
                            yonetici.Show();
                            vtBaglantisi.Close();
                            break;

                        }


                    }


                    if (radioButton2.Checked == true)
                    {
                        if (kayitOku["kullanici_adi"].ToString() == textBox1.Text &&
                            kayitOku["sifre"].ToString() == textBox2.Text &&
                            kayitOku["yetki"].ToString() == "Kullanıcı")
                        {
                            girisYapabilmeDurumu = true;
                            id = kayitOku.GetValue(0).ToString();
                            ad = kayitOku.GetValue(1).ToString();
                            soyad = kayitOku.GetValue(2).ToString();
                            yetki = kayitOku.GetValue(3).ToString();
                            kullanici_adi = kayitOku.GetValue(4).ToString();
                            sifre = kayitOku.GetValue(5).ToString();

                            this.Hide();

                            KullaniciEkrani kullanici = new KullaniciEkrani();
                            kullanici.Show();
                            vtBaglantisi.Close();
                            break;

                        }
                    }
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Giriş Yapılamadı. Bilgilerinizi Kontrol Ediniz.", "Hatalı Giriş");
                vtBaglantisi.Close();
            }
        }
    }
}