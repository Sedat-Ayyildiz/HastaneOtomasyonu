using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Otomasyonu
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        //DGVPROPERTİES->MAXIMIZEBOX:FALSE/AUTOSIZEMODE:GOWANDSHRİNK yapınca form sağa sola veya yukarı aşağı buyumez kıtleriz !
        //DGVPROPERTİES->ENABLEHEADERVISIBLE:FALSE/ROWHEADERVISIBLE:FALSE/
        //PROPERTİES->ACCEPTBUTTON seçtikten sonra formda ENTER tuşuna baınca ekranda ki atadığımız buton çalışır !

        private void BtnHastaGirisi_Click(object sender, EventArgs e)
        {
            FrmHastaGiris fr = new FrmHastaGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnDoktorGirisi_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris fr = new FrmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnSekreterGirisi_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
