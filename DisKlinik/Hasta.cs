using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Hasta : Form
    {
        public Hasta()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO HastaTbl values('"+HAdSoyadTb.Text+"','"+HTelefonTb.Text+"','"+AdresTb.Text+"','"+HDogumTarih.Text+"','"+HCinsiyetCb.SelectedItem.ToString()+"','"+AlerjiTb.Text+"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Hasta Başarıyla Eklendi");
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
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            HAdSoyadTb.Text = "";
            HTelefonTb.Text = "";
            AdresTb.Text = "";
            HDogumTarih.Text = "";
            HCinsiyetCb.SelectedIndex = -1;
            AlerjiTb.Text = "";
      

        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        int key = 0;

        private void HastaDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            AdresTb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarih.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.SelectedItem = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            AlerjiTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();
            if(HAdSoyadTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if(key== 0)
            {
                MessageBox.Show("Silinecek Hastayı Seçin");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM HastaTbl WHERE HId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Başarıyla Silindi");
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
            //hasta güncelleme
            Hastalar Hs = new Hastalar();
            if(key== 0) {
                MessageBox.Show("Güncellenecek Hastayı Seçin");
            }
            else
            {
                try
                {
                    string query = "UPDATE HastaTbl SET HAd='" + HAdSoyadTb.Text + "', HTelefon='" + HTelefonTb.Text + "', HAdres='" + AdresTb.Text + "', HDTarih='" + HDogumTarih.Text + "', HCinsiyet='" + HCinsiyetCb.SelectedItem.ToString() + "', HAlerji='" + AlerjiTb.Text + "' WHERE HId=" + key + "";
                    Hs.HastaGuncelle(query);
                    MessageBox.Show("Hasta Başarıyla Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
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
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl where HAd like '%" + AraTb.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGV.DataSource = ds.Tables[0];
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
