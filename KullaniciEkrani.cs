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
    public partial class KullaniciEkrani : Form
    {
        public KullaniciEkrani()
        {
            InitializeComponent();
        }

        OleDbConnection vtBaglantisi = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
           Application.StartupPath + "\\Amore_Boutique.accdb");

        //KullanıcıEkrani formu yüklendiği anda çalışması gereken bilgiler ve metotlar çalışır. 
        private void KullaniciEkrani_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;

            tumUrunleriListele();

            textBox1.Text = GirisEkrani.ad;
            textBox2.Text = GirisEkrani.soyad;
            textBox3.Text = GirisEkrani.kullanici_adi;
            textBox4.Text = GirisEkrani.sifre;

            sepetiListele();
            siparislerimiListele();

            textBox6.Visible = false;
            label10.Visible = false;


        }

        //button1'e tıklandığı zaman uygulamadan çıkış yapılır.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        //comboBox1'de hangi satır seçildiyse comboBox2'de satırlar ona göre gelir.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            string cinsiyet = comboBox1.SelectedItem.ToString();

            if (cinsiyet == "Kadın")
            {
                string[] kategori = { "Kazak", "Sweatshirt", "Elbise", "Mont", "Pantolon", "Etek", "Jeans" };
                comboBox2.Items.AddRange(kategori);
            }

            if (cinsiyet == "Erkek")
            {
                string[] kategori = { "Kazak", "Sweatshirt", "Mont", "Pantolon", "Gömlek", "Jeans", "Ceket" };
                comboBox2.Items.AddRange(kategori);
            }
        }

        //Veritabanındaki urunler tablosunu listeler.
        private void tumUrunleriListele()
        {
            vtBaglantisi.Open();
            OleDbDataAdapter listele = new OleDbDataAdapter("select * from urunler", vtBaglantisi);
            DataSet dsHafiza = new DataSet();
            listele.Fill(dsHafiza);
            dataGridView1.DataSource = dsHafiza.Tables[0];
            vtBaglantisi.Close();
        }

        //Veritabanındaki urunler tablosundan comboBox1 , comboBox2 ve comboBox3'e göre listeleme yapılır.
        private void urunleriListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from urunler " +
                    "where (cinsiyet = '" + comboBox1.SelectedItem.ToString() + "' " +
                    "and kategori = '" + comboBox2.SelectedItem.ToString() + "' " +
                    "and beden = '" + comboBox3.SelectedItem.ToString() + "') ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                listele.Fill(dsHafiza);
                dataGridView1.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }

            catch (Exception)
            {
                MessageBox.Show("Ürün bulunamadı.");
                vtBaglantisi.Close();
            }

        }

        //pictureBox1'de seçilen satıra göre ürün resmi getirilir.
        private void resimleriGetir()
        {
            vtBaglantisi.Open();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Kıyafet\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + ".jpg");
            vtBaglantisi.Close();
        }

        //pictureBox2'de seçilen satıra göre ürün resmi getirilir.
        private void resimleriGetir2()
        {
            vtBaglantisi.Open();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Kıyafet\\" + dataGridView4.CurrentRow.Cells[0].Value.ToString() + ".jpg");
            vtBaglantisi.Close();
        }

        //pictureBox3'de seçilen satıra göre ürün resmi getirilir.
        private void resimleriGetir3()
        {
            vtBaglantisi.Open();
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\Kıyafet\\" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + ".jpg");
            vtBaglantisi.Close();
        }

        //button3'e tıklandığı zaman urunleriListele metodu çalışır.
        private void button3_Click(object sender, EventArgs e)
        {
            urunleriListele();

        }

        //button4'e tıklandığı zaman giriş ekranına geçiş yapılır.
        private void button4_Click(object sender, EventArgs e)
        {
            vtBaglantisi.Open();
            this.Hide();
            GirisEkrani giris = new GirisEkrani();
            giris.Show();
            vtBaglantisi.Close();
        }

        //dataGridView1'de satırlara göre resimleriGetir metodu çalışır.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            resimleriGetir();
        }


        //Kaydını silmek isteyen kullanıcılar için button2'ye tıklandığı zaman bütün verileri veritabanından silinir.
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand kayitSil1 = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();
                kayitSil1.Connection = vtBaglantisi;
                kayitSil1.CommandText = "delete from kullanicilar where kullanici_adi = '" + textBox3.Text + "'";
                kayitSil1.ExecuteNonQuery();
                vtBaglantisi.Close();

                OleDbCommand kayitSil2 = new OleDbCommand();
                vtBaglantisi.Open();
                kayitSil2.Connection = vtBaglantisi;
                kayitSil2.CommandText = "delete from sepetteki_urunler where kullanici_adi = '" + textBox3.Text + "'";
                kayitSil2.ExecuteNonQuery();
                vtBaglantisi.Close();

                OleDbCommand kayitSil3 = new OleDbCommand();
                vtBaglantisi.Open();
                kayitSil3.Connection = vtBaglantisi;
                kayitSil3.CommandText = "delete from siparis_edilen_urunler where kullanici_adi = '" + textBox3.Text + "'";
                kayitSil3.ExecuteNonQuery();
                vtBaglantisi.Close();

                OleDbCommand kayitSil4 = new OleDbCommand();
                vtBaglantisi.Open();
                kayitSil4.Connection = vtBaglantisi;
                kayitSil4.CommandText = "delete from iade_edilen_urunler where kullanici_adi = '" + textBox3.Text + "'";
                kayitSil4.ExecuteNonQuery();
                vtBaglantisi.Close();

                MessageBox.Show("Kaydınız silindi!");
                dsHafiza.Clear();
                this.Close();
                Application.Exit();
            }

            catch (Exception)
            {
                MessageBox.Show("Kaydınız silinemedi!");
                vtBaglantisi.Close();
            }
        }

        //Kaydını güncellemek isteyen kullanıcılar değişiklikleri girip button5'e tıkladığında kayıtları güncellenir.
        private void button5_Click(object sender, EventArgs e)
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
                MessageBox.Show("Kaydınız Güncellendi!");
                dsHafiza.Clear();
            }

            catch (Exception)
            {
                MessageBox.Show("Kaydınız Güncellenemedi!");
                vtBaglantisi.Close();
            }

        }

        //Sepete ürün eklemek isteyen kullanıcılar ürünü seçip button10'a tıklar ve ürün sepete eklenir.
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString())>0)
                {
                    OleDbCommand sepeteEkle = new OleDbCommand();
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    DataSet dsHafiza = new DataSet();

                    vtBaglantisi.Open();
                    sepeteEkle.Connection = vtBaglantisi;
                    sepeteEkle.CommandText = "Insert Into sepetteki_urunler(ad,soyad,kullanici_adi,urun_adi,urun_fiyati,beden) " +
                        "Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "')";
                    sepeteEkle.ExecuteNonQuery();
                    sepeteEkle.Dispose();
                    vtBaglantisi.Close();
                    MessageBox.Show("Ürün sepete eklendi!");
                    dsHafiza.Clear();

                    sepetiListele();
                }

                else
                {
                    MessageBox.Show("Ürün Tükendi", "İşlem Başarısız");
                }
           

            }

            catch (Exception)
            {
                MessageBox.Show("Ürün sepete eklenemedi!");
                vtBaglantisi.Close();
            }

        }

        //Veritabanındaki sepetteki_urunler tablosunun verileri dataGridView4'te listelenir.
        private void sepetiListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter sepetiListele = new OleDbDataAdapter("select urun_adi as ÜRÜN , urun_fiyati as FİYAT , beden as BEDEN from sepetteki_urunler where ad = '" + textBox1.Text + "' and " +
                    "soyad = '" + textBox2.Text + "' and kullanici_adi = '" + textBox3.Text + "' ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                sepetiListele.Fill(dsHafiza);
                dataGridView4.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        //Ürünü sepetten çıkarmak için oluşturuldu.
        private void sepettenCikar()
        {
            try
            {

                OleDbCommand urunSil = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();

                urunSil.Connection = vtBaglantisi;
                urunSil.CommandText = "delete from sepetteki_urunler where urun_adi = '" + dataGridView4.CurrentRow.Cells[0].Value.ToString() + "' and beden = '" + dataGridView4.CurrentRow.Cells[2].Value.ToString() + "'";
                urunSil.ExecuteNonQuery();

                MessageBox.Show("Ürün sepetten kaldırıldı!");
                dsHafiza.Clear();
                dataGridView4.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

                sepetiListele();


            }
            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        //Sepetteki ürünlerin toplam fiyatını hesaplar.
        private void ToplamAl()
        {
            decimal toplam = 0;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                toplam += Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                label5.Text = toplam.ToString("#,###.00 TL");
            }
        }

        //button9'a tıklandığı zaman sepettenCikar metodu çalışır ve sonrasında sepetin güncellenmesi için sepetiListele metodu yeniden çağrılır.
        private void button9_Click(object sender, EventArgs e)
        {
            sepettenCikar();
            sepetiListele();
        }

        //dataGridView4'de satırlara göre resimleriGetir2 metodu çalışır.
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            resimleriGetir2();
        }

        //ToplamAl metodu button12'ye tıklanınca çalıştırılır.
        private void button12_Click(object sender, EventArgs e)
        {
            ToplamAl();
        }

        //Sepetteki ürünlerin onayını yapan kullanıcıların ürün ve kişi bilgilerinin siparis_edilen_urunler tablosuna eklenmesini sağlar.
        //Ayrıca bu ürünlerin stok adetinin bir azalmasını sağlar.
        private void siparisEdilenlereEkle()
        {
            try
            {

                OleDbCommand siparisEdilenler = new OleDbCommand();

                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                DateTime tarih;
                tarih = DateTime.Now.Date;


                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    vtBaglantisi.Open();
                    siparisEdilenler.Connection = vtBaglantisi;
                    var urunAdi = dataGridView4.Rows[i].Cells[0].Value.ToString();
                    var urunFiyati = dataGridView4.Rows[i].Cells[1].Value.ToString();
                    var urunBedeni = dataGridView4.Rows[i].Cells[2].Value.ToString();

                    siparisEdilenler.CommandText = "Insert Into siparis_edilen_urunler(ad,soyad,kullanici_adi,urun_adi,urun_fiyati,beden,tarih) " +
                    "Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + urunAdi + "'," + urunFiyati + ",'" + urunBedeni + "','" + tarih.ToString().TrimEnd('0', ':') + "')";
                    siparisEdilenler.ExecuteNonQuery();
                    vtBaglantisi.Close();


                    OleDbCommand stokAzalt = new OleDbCommand();
                    vtBaglantisi.Open();
                    stokAzalt.Connection = vtBaglantisi;


                    stokAzalt.CommandText = "update urunler set stok_adeti = stok_adeti-1 where urun_adi = @urun_adi  and beden = @beden";
                    stokAzalt.Parameters.AddWithValue("@urun_adi", urunAdi);
                    stokAzalt.Parameters.AddWithValue("@beden", urunBedeni);
                    stokAzalt.ExecuteNonQuery();
                    vtBaglantisi.Close();

                }

                dsHafiza.Clear();
                satilanUrunleriSepettenCikar();
                siparislerimiListele();
                tumUrunleriListele();
            }



            catch (Exception)
            {

                vtBaglantisi.Close();
            }



        }

        //Sipariş edilen ürünleri kullanıcın görmesi sağlanır. siparis_edilen_urunler tablosunun dataGridView3'te görüntülenmesi sağlanır.
        private void siparislerimiListele()
        {
            try
            {

                vtBaglantisi.Open();
                OleDbDataAdapter siparisListele = new OleDbDataAdapter("select urun_adi as ÜRÜN , urun_fiyati as FİYAT , beden as BEDEN from siparis_edilen_urunler where ad = '" + textBox1.Text + "' and " +
                    "soyad = '" + textBox2.Text + "' and kullanici_adi = '" + textBox3.Text + "' ", vtBaglantisi);
                DataSet dsHafiza = new DataSet();
                siparisListele.Fill(dsHafiza);
                dataGridView3.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();

            }
            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        //Ödeme türü belirlenir. Kartla ödeme yapılacaksa label10 ve textBox6 görünür olur ve kart bilgisi girilir.
        private void odemeTuru()
        {
            if (radioButton1.Checked == true)
            {
                label10.Visible = false;
                textBox6.Visible = false;
            }
            else
            {
                label10.Visible = true;
                textBox6.Visible = true;
            }
        }

        //button8'e tıklandığı zaman SiparisOnay formu açılır ve siparisEdilenlereEkle metodu çalışır.
        private void button8_Click(object sender, EventArgs e)
        {
            vtBaglantisi.Open();
            SiparisOnay siparis = new SiparisOnay();
            siparis.Show();

            vtBaglantisi.Close();
            siparisEdilenlereEkle();
            //dataGridView4.DataSource = null;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            odemeTuru();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            odemeTuru();
        }

        //dataGridView3'te satırlara göre resimleriGetir3 metodu çalışır.
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            resimleriGetir3();
        }

        //Satılan ürünler sepetten kaldırılır.
        private void satilanUrunleriSepettenCikar()
        {
            try
            {
                OleDbCommand sepettenKaldir = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();

                sepettenKaldir.Connection = vtBaglantisi;
                sepettenKaldir.CommandText = "delete from sepetteki_urunler where kullanici_adi = '" + textBox3.Text + "'";
                sepettenKaldir.ExecuteNonQuery();
                vtBaglantisi.Close();
                dsHafiza.Clear();
                sepetiListele();

            }

            catch (Exception)
            {
                vtBaglantisi.Close();
            }
        }

        //Ürün iade edilmek istenirse bu ürün siparis_edilen_urunler tablosundan çıkartılır.
        private void urunuIadeEt()
        {
            try
            {

                OleDbCommand urunIadeEt = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                vtBaglantisi.Open();

                urunIadeEt.Connection = vtBaglantisi;
                urunIadeEt.CommandText = "delete from siparis_edilen_urunler where urun_adi = '" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "' and beden = '" + dataGridView3.CurrentRow.Cells[2].Value.ToString() + "'";
                urunIadeEt.ExecuteNonQuery();


                dsHafiza.Clear();
                dataGridView3.DataSource = dsHafiza.Tables[0];
                vtBaglantisi.Close();




            }
            catch (Exception)
            {

                vtBaglantisi.Close();
            }
        }

        //Ürün iade edilmek istenirse iade_edilen_urunler tablosuna eklenir ve ürün iade edildiğinden stok adeti bir artırılır.
        private void iadeEdilenlereEkle()
        {
            try
            {

                OleDbCommand iadeEdilenler = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                DataSet dsHafiza = new DataSet();

                DateTime tarih;
                tarih = DateTime.Now.Date;

                vtBaglantisi.Open();
                iadeEdilenler.Connection = vtBaglantisi;

                iadeEdilenler.CommandText = "Insert Into iade_edilen_urunler(ad,soyad,kullanici_adi,urun_adi,urun_fiyati,beden,tarih) " +
                "Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "','" + Convert.ToInt32(dataGridView3.CurrentRow.Cells[1].Value.ToString()) + "','" + dataGridView3.CurrentRow.Cells[2].Value.ToString() + "','" + tarih.ToString().TrimEnd('0', ':') + "')";
                iadeEdilenler.ExecuteNonQuery();
                vtBaglantisi.Close();

                OleDbCommand stokArtir = new OleDbCommand();
                vtBaglantisi.Open();
                stokArtir.Connection = vtBaglantisi;


                stokArtir.CommandText = "update urunler set stok_adeti = stok_adeti+1 where urun_adi = @urun_adi and beden = @beden and kullanici_adi = @kullanici_adi";
                stokArtir.Parameters.AddWithValue("@urun_adi", dataGridView3.CurrentRow.Cells[0].Value.ToString());
                stokArtir.Parameters.AddWithValue("@beden", dataGridView3.CurrentRow.Cells[2].Value.ToString());
                stokArtir.Parameters.AddWithValue("@kullanici_adi", textBox3.Text);
                stokArtir.ExecuteNonQuery();
                vtBaglantisi.Close();

                dsHafiza.Clear();
                MessageBox.Show("Ürün iade işlemleri başladı!");


            }
            catch (Exception)
            {
                MessageBox.Show("Ürün iade edilemedi!");
                vtBaglantisi.Close();
            }
        }

        //button6'ya tıklandığı zaman iadeEdilenlereEkle , urunuIadeEt ve siparislerimiListele metodu çağrılır.
        private void button6_Click(object sender, EventArgs e)
        {
            iadeEdilenlereEkle();
            urunuIadeEt();
            siparislerimiListele();
            tumUrunleriListele();

        }
    }
}
