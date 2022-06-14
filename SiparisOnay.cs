using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1180505023_Büşra_Yunusoğlu_GP_Projesi
{
    public partial class SiparisOnay : Form
    {
        public SiparisOnay()
        {
            InitializeComponent();
        }

        //Sipariş onaylandığında bilgilendirme olarak ekranda açılır.
        private void SiparisOnay_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;

            label4.Text = GirisEkrani.ad + " " + GirisEkrani.soyad;

        }

    }
}
