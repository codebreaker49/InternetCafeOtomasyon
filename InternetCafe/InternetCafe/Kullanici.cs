using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafe
{
    internal class Kullanici
    {
        public static int KullaniciID = 0;
        public static bool durum = false;
        public static SqlDataReader KullaniciGirisi(TextBox KullaniciAdi,TextBox Sifre)
        {
            Veritabanı.baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from TBLKullanici where KullaniciAdi='" + KullaniciAdi.Text + "' and Sifre='" + Sifre.Text+"'",Veritabanı.baglanti);
            SqlDataReader dr= cmd.ExecuteReader();
            if (dr.Read())
            {
                durum = true;
                KullaniciID = int.Parse(dr["KullaniciID"].ToString());
            }
            else
            {
                durum = false;
            }
            Veritabanı.baglanti.Close();
            return dr;
        }
    }
}
