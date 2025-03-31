﻿using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafe
{
    internal class Veritabanı
    {
      public static  SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-OR3T0EKC\\SQLEXPRESS;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        public static DataTable SepetListele(DataGridView gridview)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select*from TBLSepet", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            gridview.DataSource = tbl;
            return tbl;
        }
        public static DataTable ComboyaBosMasaGetir(ComboBox combo)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select*from TBLMasalar where Durumu='BOŞ'", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            combo.DataSource = tbl;
            combo.DisplayMember = "Masalar";
            combo.ValueMember = "MasaID";
            baglanti.Close();
            return tbl;
        }
        public static void ESG(SqlCommand command,string sorgu)
        {
            baglanti.Open();
            command.Connection = baglanti;
            command.CommandText = sorgu;
             command.ExecuteNonQuery();
            baglanti.Close();
        }
        public static SqlDataReader listviewdeKayitlariGoster(ListView list)
        {
            baglanti.Open();
            SqlCommand cmd= new SqlCommand("Select*from tblhareketler",baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle= new ListViewItem();
                ekle.Text = dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                ekle.SubItems.Add(dr[2].ToString());
                ekle.SubItems.Add(dr[3].ToString());
                ekle.SubItems.Add(dr[4].ToString());
                ekle.SubItems.Add(dr[5].ToString());
                ekle.SubItems.Add(dr[6].ToString());
                list.Items.Add(ekle);
            }
            baglanti.Close();
            return dr;
        }
    }
}
