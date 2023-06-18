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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //AD-SOYAD ÇEKME
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //RANDEVU GEÇMİŞİ
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid as 'Randevu No',RandevuTarih as 'Tarih',RandevuSaat as 'Saat'," +
            "RandevuBrans as 'Brans',RandevuDoktor as 'Doktor',RandevuDurum as 'Durum',HastaTC as 'Hasta TC',HastaSikayet as 'Şikayet' " +
            "from Tbl_Randevular where HastaTC='"+tc+"'",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //BRANŞLARI ÇEKME
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {   //Branş Combobax'ında Bölüm seçince Doktor combobax'ına o Bölmle ilgili Doktorlar atanır.
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid as 'Randevu No',RandevuTarih as 'Tarih',RandevuSaat as 'Saat'," +
            "RandevuBrans as 'Brans',RandevuDoktor as 'Doktor',RandevuDurum as 'Durum',HastaTC as 'Hasta TC',HastaSikayet as 'Şikayet' " +
            "from Tbl_Randevular where RandevuBrans='" + CmbBrans.Text + "'" + " and RandevuDoktor='" + CmbDoktor.Text + "' and RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDüzenle fr = new FrmHastaBilgiDüzenle();
            fr.TCno = LblTC.Text;//HastaDetay da ki TC'yi BilgiDüzenleye taşıdık.
            fr.Show();            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komutRandevuAl = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@h1,HastaSikayet=@h2 where Randevuid=@h3",bgl.baglanti());
            komutRandevuAl.Parameters.AddWithValue("@h1", LblTC.Text);
            komutRandevuAl.Parameters.AddWithValue("@h2", RchSikayet.Text);
            komutRandevuAl.Parameters.AddWithValue("@h3", Txtid.Text);
            komutRandevuAl.ExecuteNonQuery();
            bgl.baglanti().Close();            
            MessageBox.Show("Randevu Başarılı Bir Şekilde Alındı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Txtid.Clear();
            CmbBrans.Text = null;
            CmbDoktor.Text = null;
            RchSikayet.Clear();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            FrmGirisler frg = new FrmGirisler();
            frg.Show();
            this.Close();
        }
    }    
}
