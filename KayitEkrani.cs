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
    public partial class KayitEkrani : Form
    {
        public KayitEkrani()
        {
            InitializeComponent();
        }

        OleDbConnection vtBaglantisi = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
           Application.StartupPath + "\\Amore_Boutique.accdb");
        OleDbCommand kayitEkle = new OleDbCommand();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        DataSet ds = new DataSet();


        private void KayitEkrani_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        //button1'e tıklandığı zaman bilgileri girilen kişi veritabanına kullanıcı olarak kaydedilir.
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text == textBox5.Text)
                {
                    kayitEkle.Connection = vtBaglantisi;
                    kayitEkle.CommandText = "Insert Into kullanicilar(ad,soyad,yetki,kullanici_adi,sifre) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + "Kullanıcı" + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                    vtBaglantisi.Open();
                    kayitEkle.ExecuteNonQuery();
                    kayitEkle.Dispose();
                    vtBaglantisi.Close();
                    MessageBox.Show("Kayıt Tamamlandı!");
                    ds.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }

                else
                {
                    MessageBox.Show("Hatalı Şifre Girdiniz.", "Yanlış Şifre");
                }
            }

            else
            {
                MessageBox.Show("Bilgilerinizi eksiksiz giriniz.", "Kayıt Başarısız");
            }


        }

        //button2'ye tıklandığında giriş ekranına yönlendiriliriz.
        private void button2_Click(object sender, EventArgs e)
        {
            vtBaglantisi.Open();
            this.Hide();
            GirisEkrani giris = new GirisEkrani();
            giris.Show();
            vtBaglantisi.Close();
        }
    }
}
