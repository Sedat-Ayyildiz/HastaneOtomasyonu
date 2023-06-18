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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string TCno;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTc.Text = TCno;

            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@d1",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();   
            }
            bgl.baglanti().Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d4 where DoktorTC=@d5",bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1" ,TxtAd.Text);
            komut1.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut1.Parameters.AddWithValue("@d4", TxtSifre.Text);
            komut1.Parameters.AddWithValue("@d5", MskTc.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Başarılı Bir Şekilde Güncellendi.\nGüncel Bilgilerinizi Sisteme Kayıt Edebilmemiz " +
                            "için Lütfen Çıkış Yapıp Tekrar GİRİNİZ !", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frg = new FrmDoktorGiris();
            frg.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frg = new FrmGirisler();
            frg.Show();
            this.Close();
        }
    }
}
