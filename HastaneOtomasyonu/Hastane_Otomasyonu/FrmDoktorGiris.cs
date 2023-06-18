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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@d1 and DoktorSifre=@d2",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", MskTc.Text);
            komut.Parameters.AddWithValue("@d2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frd = new FrmDoktorDetay();
                frd.TC = MskTc.Text;//Doktor Girişte ki TC'yi DOKTORDETAY kısmında ki LblTC label'ına gönderdik.
                frd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı KULLANICI ADI veya ŞİFRE.");
            }
            bgl.baglanti().Close();
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmGirisler frg = new FrmGirisler();
            frg.Show();
            this.Close();
        }
    }
}
