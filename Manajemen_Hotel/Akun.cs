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
    public partial class Akun : Form
    {
        koneksi konn = new koneksi();
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        void munculLevel()
        {
            comboBox1.Items.Add("ADMIN");
            comboBox1.Items.Add("FO");
        }

        void kondisiAwal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            munculLevel();
            munculDataAkun();
        }

        void munculDataAkun()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from akun", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "akun");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "akun";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Refresh();
        }

        void noOtomatis()
        {
            long hitung;
            string urutan;
            SqlDataReader rd;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select kode_akun from akun where kode_akun in(select max(kode_akun) from akun) order by kode_akun desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kode_akun"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "AKN" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else
            {
                urutan = "AKN001";
            }
            rd.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }

        public Akun()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Akun_Load(object sender, EventArgs e)
        {
            kondisiAwal();
            noOtomatis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                cmd = new SqlCommand("insert into akun values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Input");
                kondisiAwal();
                noOtomatis();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                cmd = new SqlCommand("update akun set nama = '" + textBox2.Text + "', pass = '" + textBox3.Text + "', jabatan ='" + comboBox1.Text + "'where kode_akun ='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Edit");
                kondisiAwal();
                noOtomatis();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();

            DialogResult konfir = MessageBox.Show("Apakah anda yaking ingin menghapus data ini?!");

            if (konfir == DialogResult.OK)
            {
                cmd = new SqlCommand("delete from akun where kode_akun ='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Terhapus");
                kondisiAwal();
                noOtomatis();
            }
            else
            {
                MessageBox.Show("Data tidak jadi di hapus");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["kode_akun"].Value.ToString();
                textBox2.Text = row.Cells["nama"].Value.ToString();
                textBox3.Text = row.Cells["pass"].Value.ToString();
                comboBox1.Text = row.Cells["jabatan"].Value.ToString();

            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
