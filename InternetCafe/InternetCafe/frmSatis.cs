using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafe
{
    public partial class frmSatis : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-OR3T0EKC\\SQLEXPRESS;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        public frmSatis()
        {
            InitializeComponent();
        }
        Button btn;
        private void SecileneGore(object sender, MouseEventArgs e)
        {
            btn= sender as Button;
            if (btn.BackColor==Color.Red)
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = false;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible=false;
            }
            else
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
            }
        }
        RadioButton radio;
        private void RadioButtonSeciliyeGore(object sender, EventArgs e)
        {
            radio= sender as RadioButton;
        }

        private void frmSatis_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'internetCafeDataSet7.TBLSaatUcreti' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLSaatUcretiTableAdapter.Fill(this.internetCafeDataSet7.TBLSaatUcreti);
            // TODO: Bu kod satırı 'internetCafeDataSet7.TBLMasalar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLMasalarTableAdapter.Fill(this.internetCafeDataSet7.TBLMasalar);
            radioButtonSuresiz.Checked = true;
            Yenile();
            comboBosMasalar.Text = "";
        }

        private void Yenile()
        {
            Veritabanı.SepetListele(dataGridView1);
            Veritabanı.listviewdeKayitlariGoster(listView1);
            Veritabanı.ComboyaBosMasaGetir(comboBosMasalar);
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    if (item.Name != btnMasaAc.Name)
                    {
                       Veritabanı.baglanti.Open();
                        SqlCommand komut = new SqlCommand("select*from TBLMasalar",Veritabanı.baglanti);
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {

                            if (dr["durumu"].ToString() == "BOŞ" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.LightGreen;
                            }
                            if (dr["durumu"].ToString() == "DOLU" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.Red;
                            }
                        }
                       Veritabanı.baglanti.Close();
                    } 
                }
            }
        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            if (Kullanici.KullaniciID==1)
            {
                string sqlsorgu = "insert into TBLSepet(MasaID,Masa,AcilisTuru,Baslangic,Tarih) values('" + comboBosMasalar.Text.Substring(5) + "','" + comboBosMasalar.Text + "','" + radio.Text + "','" + DateTime.Now.ToString("yyyy.MM.dd") + "','" + DateTime.Now.ToString("yyyy.MM.dd") + "')";
                SqlCommand komut = new SqlCommand();
                komut.Parameters.AddWithValue("@Baslangic",DateTime.Parse(DateTime.Now.ToString()));
                komut.Parameters.AddWithValue("@Tarih", DateTime.Parse(DateTime.Now.ToString()));
                Veritabanı.ESG(komut, sqlsorgu);
                Yenile();
                radioButtonSuresiz.Checked = true;
                MessageBox.Show(comboBosMasalar.Text.Substring(5) + "nolu masa açıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                MessageBox.Show("Böyle Bir Yetkiniz Bulunmuyor.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
