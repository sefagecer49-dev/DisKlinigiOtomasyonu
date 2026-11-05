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
    public partial class Tedavi : Form
    {
        public Tedavi()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO TedaviTbl values('" + TAdTb.Text + "'," + TUcretTb.Text + ",'" + TAciklamaTb.Text + "')";
            Tedaviler Td = new Tedaviler();
            try
            {
                Td.TedaviEkle(query);
                MessageBox.Show("Tedavi Başarıyla Eklendi");
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
            Tedaviler Td = new Tedaviler();
            string query = "select * from TedaviTbl";
            DataSet ds = Td.ShowTedavi(query);
            TedaviDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            TAdTb.Text = "";
            TUcretTb.Text = "";
            TAciklamaTb.Text = "";
            key = 0;
        }

        private void Tedavi_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        int key = 0;

        private void TedaviDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TAdTb.Text = TedaviDGV.SelectedRows[0].Cells[1].Value.ToString();
            TUcretTb.Text = TedaviDGV.SelectedRows[0].Cells[2].Value.ToString();
            TAciklamaTb.Text = TedaviDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (TAdTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TedaviDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Tedaviler Td = new Tedaviler();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tedaviyi Seçin");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM TedaviTbl WHERE TId=" + key + "";
                    Td.TedaviSil(query);
                    MessageBox.Show("Tedavi Başarıyla Silindi");
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
            //tedavi güncelleme
            Tedaviler Td = new Tedaviler();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Tedaviyi Seçin");
            }
            else
            {
                try
                {
                    string query = "UPDATE TedaviTbl SET TAd='" + TAdTb.Text + "', TUcret=" + TUcretTb.Text + ", TAciklama='" + TAciklamaTb.Text + "' WHERE TId=" + key + "";
                    Td.TedaviGuncelle(query);
                    MessageBox.Show("Tedavi Başarıyla Güncellendi");
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

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
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
            Tedaviler Td = new Tedaviler();
            string query = "select * from TedaviTbl where TAd like '%" + AraTb.Text + "%'";
            DataSet ds = Td.ShowTedavi(query);
            TedaviDGV.DataSource = ds.Tables[0];
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
