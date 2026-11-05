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
    public partial class Receteler : Form
    {
        public Receteler()
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
            HastaASCb.ValueMember = "HAd";
            HastaASCb.DataSource = dt;
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from RandevuTbl where Hasta='" + HastaASCb.SelectedValue.ToString() + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            TedaviTb.Text = "";
            foreach (DataRow dr in dt.Rows)
            {
                TedaviTb.Text = dr["Tedavi"].ToString();
            }
            baglanti.Close();
        }

        private void fillPrice()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TedaviTbl where TAd='" + TedaviTb.Text + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            TutarTb.Text = "";
            foreach (DataRow dr in dt.Rows)
            {
                TutarTb.Text = dr["TUcret"].ToString();
            }
            baglanti.Close();
        }

        private void Receteler_Load(object sender, EventArgs e)
        {
            fillHasta();
            uyeler();
            Reset();
        }

        private void HastaASCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillTedavi();
        }

        private void TedaviTb_TextChanged(object sender, EventArgs e)
        {
            fillPrice();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void geriLbl_Click(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO ReceteTbl values('" + HastaASCb.SelectedValue.ToString() + "','" + TedaviTb.Text + "'," + TutarTb.Text + ",'" + IlacTb.Text + "'," + MiktarTb.Text + ")";
                Recete Rc = new Recete();
                Rc.ReceteEkle(query);
                MessageBox.Show("Reçete Başarıyla Eklendi");
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
            Recete Rc = new Recete();
            string query = "select * from ReceteTbl";
            DataSet ds = Rc.ShowRecete(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            HastaASCb.SelectedIndex = -1;
            TedaviTb.Text = "";
            TutarTb.Text = "";
            IlacTb.Text = "";
            MiktarTb.Text = "";
            key = 0;
        }

        int key = 0;

        private void ReceteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HastaASCb.SelectedValue = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            TedaviTb.Text = ReceteDGV.SelectedRows[0].Cells[2].Value.ToString();
            TutarTb.Text = ReceteDGV.SelectedRows[0].Cells[3].Value.ToString();
            IlacTb.Text = ReceteDGV.SelectedRows[0].Cells[4].Value.ToString();
            MiktarTb.Text = ReceteDGV.SelectedRows[0].Cells[5].Value.ToString();
            key = Convert.ToInt32(ReceteDGV.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Recete Rc = new Recete();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçin");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM ReceteTbl WHERE RecId=" + key + "";
                    Rc.ReceteSil(query);
                    MessageBox.Show("Reçete Başarıyla Silindi");
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
            Hasta hst = new Hasta();
            hst.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Tedavi tdv = new Tedavi();
            tdv.Show();
            this.Hide();
        }

        void filter()
        {
            Recete Rc = new Recete();
            string query = "select * from ReceteTbl where HasAd like '%" + AraTb.Text + "%'";
            DataSet ds = Rc.ShowRecete(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        Bitmap bitmap;
        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            int height = ReceteDGV.Height;
            ReceteDGV.Height = ReceteDGV.RowCount * ReceteDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(ReceteDGV.Width, ReceteDGV.Height);
            ReceteDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, ReceteDGV.Width, ReceteDGV.Height));
            ReceteDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
