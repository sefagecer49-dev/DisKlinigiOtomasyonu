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

namespace DisKlinik
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }

        ConnectionString MyCon = new ConnectionString();
        private void fillHasta()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            RadCb.ValueMember = "HAd";
            RadCb.DataSource = dt;
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select TAd from TedaviTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(rdr);
            RtedaviCb.ValueMember = "TAd";
            RtedaviCb.DataSource = dt;
            baglanti.Close();
        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            fillHasta();
            fillTedavi();
            uyeler();
            Reset();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO RandevuTbl values('" + RadCb.SelectedValue.ToString() + "','" + RtedaviCb.SelectedValue.ToString() + "','" + RTarih.Text + "','" + RSaatCb.SelectedItem.ToString() + "')";
                Randevular Rd = new Randevular();
                Rd.RandevuEkle(query);
                MessageBox.Show("Randevu Başarıyla Eklendi");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void uyeler()
        {
            Randevular Rd = new Randevular();
            string query = "select * from RandevuTbl";
            DataSet ds = Rd.ShowRandevu(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            RadCb.SelectedIndex = -1;
            RtedaviCb.SelectedIndex = -1;
            RTarih.Text = "";
            RSaatCb.SelectedIndex = -1;
            key = 0;
        }

        int key = 0;

        private void RandevuDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RadCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            RtedaviCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            RTarih.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            RSaatCb.SelectedItem = RandevuDGV.SelectedRows[0].Cells[4].Value.ToString();
            key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Randevular Rd = new Randevular();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçin");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM RandevuTbl WHERE RId=" + key + "";
                    Rd.RandevuSil(query);
                    MessageBox.Show("Randevu Başarıyla Silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            //randevu güncelleme
            Randevular Rd = new Randevular();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Randevuyu Seçin");
            }
            else
            {
                try
                {
                    string query = "UPDATE RandevuTbl SET Hasta='" + RadCb.SelectedValue.ToString() + "', Tedavi='" + RtedaviCb.SelectedValue.ToString() + "', RTarih='" + RTarih.Text + "', RSaat='" + RSaatCb.SelectedItem.ToString() + "' WHERE RId=" + key + "";
                    Rd.RandevuGuncelle(query);
                    MessageBox.Show("Randevu Başarıyla Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Hasta hst = new Hasta();
            hst.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Tedavi tdv = new Tedavi();
            tdv.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Receteler rct = new Receteler();
            rct.Show();
            this.Hide();
        }

        private void geriLbl_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        void filter()
        {
            Randevular Rd = new Randevular();
            string query = "select * from RandevuTbl where Hasta like '%" + AraTb.Text + "%'";
            DataSet ds = Rd.ShowRandevu(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
