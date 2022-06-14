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
    public partial class YoneticiEkrani : Form
    {
        public YoneticiEkrani()
        {
            InitializeComponent();
        }

        OleDbConnection vtBaglantisi = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
            Application.StartupPath + "\\Amore_Boutique.accdb");


        //YoneticiEkrani formu yüklendiği anda çalışması gereken bilgiler ve metotlar çalışır. 
        private void YoneticiEkrani_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;


            urunleriListele();
            yoneticileriListele();
            siparisleriListele();
            label13.Visible = false;
            label14.Visible = false;
            iadeEdilenleriListele();
            kullanicilariListele();


        }

        //button1'e tıklandığı zaman uygulamadan çıkış yapılır.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        //Yöneticiler listelenir.
        private void yoneticileriListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter yoneticileriListele = new OleDbDataAdapter("select ad as AD,soyad as SOYAD,kullanici_adi as KULLANICI_ADI from kullanicilar where yetki = '" + "Yönetici" + "' ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                yoneticileriListele.Fill(dsHafiza);
                dataGridView5.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Yönetici bulunamadı.");
                vtBaglantisi.Close();
            }

        }

        //Kullanıcılar listelenir.
        private void kullanicilariListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter kullanicilariListele = new OleDbDataAdapter("select id as ID, ad as AD,soyad as SOYAD,kullanici_adi as KULLANICI_ADI from kullanicilar where yetki = '" + "Kullanıcı" + "' ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                kullanicilariListele.Fill(dsHafiza);
                dataGridView4.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Kullanıcı bulunamadı.");
                vtBaglantisi.Close();
            }

        }


        //Ürünler listelenir.
        private void urunleriListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter urunleriListele = new OleDbDataAdapter("select * from urunler", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                urunleriListele.Fill(dsHafiza);
                dataGridView6.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Ürün bulunamadı.");
                vtBaglantisi.Close();
            }
        }

        //Yönetici ekleme işlemi gerçekleştirilir. Bilgileri girilen kişi kullanicilar tablosuna yönetici olarak eklenir.
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text == textBox5.Text)
                {
                    OleDbCommand yoneticiEkle = new OleDbCommand();
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    DataSet dsHafiza = new DataSet();

                    vtBaglantisi.Open();
                    yoneticiEkle.Connection = vtBaglantisi;
                    yoneticiEkle.CommandText = "Insert Into kullanicilar(ad,soyad,yetki,kullanici_adi,sifre) " +
                        "Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + "Yönetici" + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                    yoneticiEkle.ExecuteNonQuery();
                    yoneticiEkle.Dispose();
                    vtBaglantisi.Close();
                    MessageBox.Show("Kayıt Tamamlandı!");
                    dsHafiza.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    yoneticileriListele();
                }

                else
                {
                    MessageBox.Show("Hatalı Şifre Girdiniz.", "Yanlış Şifre");
                }
            }

            else
            {
                MessageBox.Show("Bilgilerinizi eksiksiz giriniz.", "Kayıt Başarısız");
                vtBaglantisi.Close();
            }
        }

        //Yönetici arama işlemi
        private void yoneticiAra()
        {
            vtBaglantisi.Open();
            OleDbDataAdapter yoneticiAra = new OleDbDataAdapter("select * from kullanicilar where ad like '%" + textBox1.Text + "%' and yetki = '"+"Yönetici"+"'", vtBaglantisi);
            DataSet dsHafiza = new DataSet();
            yoneticiAra.Fill(dsHafiza);
            dataGridView5.DataSource = dsHafiza.Tables[0];
            vtBaglantisi.Close();
        }

        //Ürün arama işlemi
        private void urunAra()
        {
            vtBaglantisi.Open();
            OleDbDataAdapter urunAra = new OleDbDataAdapter("select * from urunler where urun_adi like '%" + textBox10.Text + "%'", vtBaglantisi);
            DataSet dsHafiza = new DataSet();
            urunAra.Fill(dsHafiza);
            dataGridView6.DataSource = dsHafiza.Tables[0];
            vtBaglantisi.Close();
        }

        //Ürün ekleme işlemi gerçekleşir. urunler tablosuna ekleme yapılır.
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox10.Text != "" && textBox9.Text != "" && textBox8.Text != "" && textBox7.Text != "" && comboBox1.SelectedItem.ToString() != "" && comboBox2.SelectedItem.ToString() != "")
                {

                    OleDbCommand urunEkle = new OleDbCommand();
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    DataSet dsHafiza = new DataSet();

                    vtBaglantisi.Open();
                    urunEkle.Connection = vtBaglantisi;
                    urunEkle.CommandText = "Insert Into urunler(urun_adi,urun_fiyati,stok_adeti,kategori,cinsiyet,beden) " +
                        "Values ('" + textBox10.Text + "','" + textBox9.Text + "','" + textBox8.Text + "','" + textBox7.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "')";
                    urunEkle.ExecuteNonQuery();
                    urunEkle.Dispose();
                    vtBaglantisi.Close();
                    MessageBox.Show("Ürün eklendi!");
                    dsHafiza.Clear();
                    textBox10.Clear();
                    textBox9.Clear();
                    textBox8.Clear();
                    textBox7.Clear();

                    urunleriListele();
                }

                else
                {
                    MessageBox.Show("Bilgileri eksiksiz giriniz.", "Kayıt Başarısız");
                    vtBaglantisi.Close();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Hatalı giriş yaptınız", "Kayıt Başarısız");
            }

        }

        //Ürünler urunler tablosunda güncellenir.
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


                OleDbCommand urunGuncelle = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();
                urunGuncelle.Connection = vtBaglantisi;
                urunGuncelle.CommandText = "update urunler set urun_adi ='" + textBox10.Text + "' , urun_fiyati = " + Convert.ToInt32(textBox9.Text) + ", stok_adeti = " + Convert.ToInt32(textBox8.Text) + ", kategori = '" + textBox7.Text + "' ,cinsiyet = '" + comboBox1.SelectedItem.ToString() + "',beden = '" + comboBox2.SelectedItem.ToString() + "' where urun_adi = @urun_adi";
                urunGuncelle.Parameters.AddWithValue("@urun_adi", textBox10.Text);
                urunGuncelle.ExecuteNonQuery();
                vtBaglantisi.Close();
                MessageBox.Show("Ürün güncellendi!");
                dsHafiza.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox7.Clear();

                urunleriListele();



            }

            catch (Exception)
            {
                MessageBox.Show("Güncelleme Başarısız");
                vtBaglantisi.Close();
            }

        }

        //Giriş ekranına yönlendirme yapılır.
        private void button7_Click(object sender, EventArgs e)
        {
            vtBaglantisi.Open();
            this.Hide();
            GirisEkrani giris = new GirisEkrani();
            giris.Show();
            vtBaglantisi.Close();
        }

        //Yönetici kaydı silme işlemi yapılır. Kullanıcı adı girilen yönetici veritabanından silinir.
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand kayitSil = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();
                kayitSil.Connection = vtBaglantisi;
                kayitSil.CommandText = "delete from kullanicilar where kullanici_adi = '" + textBox3.Text + "'";
                kayitSil.ExecuteNonQuery();
                vtBaglantisi.Close();
                MessageBox.Show("Kayıt silindi!");
                dsHafiza.Clear();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                yoneticileriListele();
            }
            catch
            {
                MessageBox.Show("Kayıt silinemedi!");
            }


        }

        //Ürün silme işlemi yapılır. Adı girilen ürün urunler tablosundan silinir.
        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                OleDbCommand urunSil = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();

                urunSil.Connection = vtBaglantisi;
                urunSil.CommandText = "delete from urunler where urun_adi = '" + textBox10.Text + "'";
                urunSil.ExecuteNonQuery();
                vtBaglantisi.Close();
                MessageBox.Show("Ürün silindi!");
                dsHafiza.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox7.Clear();

                urunleriListele();
            }

            catch
            {
                MessageBox.Show("Ürün silinemedi!");
                vtBaglantisi.Close();
            }

        }
        /*
                private void button2_Click(object sender, EventArgs e)
                {
                    try
                    {
                        OleDbCommand kayitGuncelle = new OleDbCommand();
                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        DataSet dsHafiza = new DataSet();

                        vtBaglantisi.Open();
                        kayitGuncelle.Connection = vtBaglantisi;
                        kayitGuncelle.CommandText = "update kullanicilar set ad = '" + textBox1.Text + "',soyad = '" + textBox2.Text + "',kullanici_adi = '" + textBox3.Text + "',sifre = '" + textBox4.Text + "' " +
                            "where (kullanici_adi = '" + textBox3.Text + "') or (ad = '" + textBox1.Text + "' and soyad = '" + textBox2.Text + "' )";
                        kayitGuncelle.ExecuteNonQuery();
                        vtBaglantisi.Close();
                        MessageBox.Show("Kayıt Güncellendi!");
                        dsHafiza.Clear();
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Kayıt Güncellenemedi!");
                    }
                }

                 */

        //Bu metot stok miktarlarının istenilen aralıklara göre takip edebilmesini sağlar.
        private void azalanUrunleriGoster()
        {

            try
            {
                vtBaglantisi.Open();
                var altSinir = Convert.ToDouble(textBox6.Text);
                var ustSinir = Convert.ToDouble(textBox11.Text);
                OleDbDataAdapter urunleriListele = new OleDbDataAdapter("select * from urunler where stok_adeti BETWEEN " + altSinir + " and " + ustSinir + " ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                urunleriListele.Fill(dsHafiza);
                dataGridView2.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();
            }

            catch (Exception)
            {
                vtBaglantisi.Close();
            }



        }

        //Yönetici kayıt güncelleme işlemi yapılır.
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand kayitGuncelle = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();
                kayitGuncelle.Connection = vtBaglantisi;
                kayitGuncelle.CommandText = "update kullanicilar set ad = '" + textBox1.Text + "',soyad = '" + textBox2.Text + "',kullanici_adi = '" + textBox3.Text + "',sifre = '" + textBox4.Text + "' " +
                    "where (kullanici_adi = '" + textBox3.Text + "') or (ad = '" + textBox1.Text + "' and soyad = '" + textBox2.Text + "' )";
                kayitGuncelle.ExecuteNonQuery();
                vtBaglantisi.Close();
                MessageBox.Show("Kayıt Güncellendi!");
                dsHafiza.Clear();
                yoneticileriListele();
            }

            catch (Exception)
            {
                MessageBox.Show("Kayıt Güncellenemedi!");
                vtBaglantisi.Close();
            }
        }

        //Siparişler siparis_edilen_urunler tablosundan dataGridView1 üzerinde listelenir.
        private void siparisleriListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter siparisleriListele = new OleDbDataAdapter("select ad , soyad  , kullanici_adi  ," +
                    "urun_adi  , urun_fiyati  , beden  from siparis_edilen_urunler", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                siparisleriListele.Fill(dsHafiza);
                dataGridView1.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        //button10'a tıklandığında azalanUrunleriGoster metodu çağrılır.
        private void button10_Click(object sender, EventArgs e)
        {
            azalanUrunleriGoster();
        }

        //button9'a tıklandığında istenilen tarihe göre sipariş edilen ürünler ve toplam fiyatları görüntülenir.
        private void button9_Click(object sender, EventArgs e)
        {

            vtBaglantisi.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from siparis_edilen_urunler where tarih between tarih1 and tarih2", vtBaglantisi);
            adapter.SelectCommand.Parameters.AddWithValue("tarih1", dateTimePicker1.Value.ToShortDateString());
            adapter.SelectCommand.Parameters.AddWithValue("tarih2", dateTimePicker2.Value.ToShortDateString());
            DataSet dsHafiza = new DataSet();
            adapter.Fill(dsHafiza);
            dataGridView1.DataSource = dsHafiza.Tables[0];
            vtBaglantisi.Close();


            vtBaglantisi.Open();
            OleDbCommand toplamFiyat = new OleDbCommand("select sum(urun_fiyati) from siparis_edilen_urunler " +
                "where tarih between tarih1 and tarih2", vtBaglantisi);

            toplamFiyat.Parameters.AddWithValue("tarih1", dateTimePicker1.Value.ToShortDateString());
            toplamFiyat.Parameters.AddWithValue("tarih2", dateTimePicker2.Value.ToShortDateString());
            label14.Text = toplamFiyat.ExecuteScalar() + "TL";

            label13.Visible = true;
            label14.Visible = true;
            vtBaglantisi.Close();
        }

        //İade edilen ürünler listelenir. 
        private void iadeEdilenleriListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter iadeEdilenleriListele = new OleDbDataAdapter("select * from iade_edilen_urunler", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                iadeEdilenleriListele.Fill(dsHafiza);
                dataGridView3.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }

            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            yoneticiAra();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            urunAra();
        }

    }
}


