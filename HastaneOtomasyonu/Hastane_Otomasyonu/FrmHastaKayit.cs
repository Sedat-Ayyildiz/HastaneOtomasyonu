using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Otomasyonu
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();//Oluşurduğmuz SQLBAGLANTİSİ sınıfını her projede methodla cagırırız.
        private void BtnKayıtOl_Click(object sender, EventArgs e)
        {            
            SqlCommand komutKayıtOl = new SqlCommand("insert into Tbl_Hastalar(HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)",bgl.baglanti());
            komutKayıtOl.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutKayıtOl.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komutKayıtOl.Parameters.AddWithValue("@p3", MskTc.Text);
            komutKayıtOl.Parameters.AddWithValue("@p4", MskTelefon.Text);
            komutKayıtOl.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komutKayıtOl.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
            komutKayıtOl.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Başarılı Bir Şekilde Gerçekleşmiştir.\nŞifreniz : "+TxtSifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);            
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frg = new FrmHastaGiris();
            frg.Show();
            this.Close();
        }
    }
}
