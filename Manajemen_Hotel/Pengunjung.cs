using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Manajemen_Hotel
{
    public partial class Pengunjung : Form
    {
        koneksi konn = new koneksi();
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        void kondisiAwal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            munculDataPengunjung();
        }

        void munculDataPengunjung()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from pengunjung", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pengunjung");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "pengunjung";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Refresh();
        }

        void cariPengunjung()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from pengunjung where email like '%" + textBox4.Text + "%' or  nama like'%" + textBox4.Text + "%'", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pengunjung");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "pengunjung";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
        }

        public Pengunjung()
        {
            InitializeComponent();
        }

        private void Pengunjung_Load(object sender, EventArgs e)
        {
            kondisiAwal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || richTextBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                // Periksa apakah email sudah ada dalam database
                cmd = new SqlCommand("SELECT COUNT(*) FROM pengunjung WHERE email = @email", conn);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Email sudah ada dalam database");
                }
                else
                {
                    // Email belum ada, lakukan insert ke tabel pengunjung
                    cmd = new SqlCommand("INSERT INTO pengunjung VALUES (@nama, @alamat, @email, @keterangan)", conn);
                    cmd.Parameters.AddWithValue("@nama", textBox1.Text);
                    cmd.Parameters.AddWithValue("@alamat", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil di Input");
                }

                conn.Close();
                kondisiAwal();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();

            DialogResult konfir = MessageBox.Show("Apakah anda yaking ingin menghapus data ini?!");

            if (konfir == DialogResult.OK)
            {
                cmd = new SqlCommand("delete from pengunjung where email ='" + textBox3.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Terhapus");
                kondisiAwal();
            }
            else
            {
                MessageBox.Show("Data tidak jadi di hapus");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || richTextBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                cmd = new SqlCommand("update pengunjung set nama = '" + textBox1.Text + "', no_telp = '" + textBox2.Text + "', alamat ='" + richTextBox1.Text + "'where email ='" + textBox3.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Edit");
                kondisiAwal();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["nama"].Value.ToString();
                textBox2.Text = row.Cells["no_telp"].Value.ToString();
                textBox3.Text = row.Cells["email"].Value.ToString();
                richTextBox1.Text = row.Cells["alamat"].Value.ToString();

            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cariPengunjung();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
