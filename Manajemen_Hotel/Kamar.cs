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
    public partial class Kamar : Form
    {

        koneksi konn = new koneksi();
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        void kondisiAwal()
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            numericUpDown1.Text = "0";
            munculLevel();
            munculDatakamar();
            noOtomatis();
        }

        void munculLevel()
        {
            comboBox1.Items.Add("STANDARD ROOM");
            comboBox1.Items.Add("TWIN ROOM");
            comboBox1.Items.Add("SINGLE ROOM");
            comboBox1.Items.Add("FAMILY ROOM");
            comboBox1.Items.Add("SUITE ROOM");
            comboBox1.Items.Add("VVIP ROOM");
        }

        void munculDatakamar()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from kamar", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kamar");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "kamar";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
        }

        void noOtomatis()
        {
            long hitung;
            string urutan;
            SqlDataReader rd;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select nomor_kamar from kamar where nomor_kamar in(select max(nomor_kamar) from kamar) order by nomor_kamar desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["nomor_kamar"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "KMR" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else
            {
                urutan = "KMR001";
            }
            rd.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }

        public Kamar()
        {
            InitializeComponent();
        }

        private void Kamar_Load(object sender, EventArgs e)
        {
            kondisiAwal();
            noOtomatis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || comboBox1.Text.Trim() == "" || textBox4.Text.Trim() == "" || numericUpDown1.Text.Trim() == "0")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                cmd = new SqlCommand("insert into kamar values ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + numericUpDown1.Text + "','" + textBox4.Text + "')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Input");
                kondisiAwal();
                noOtomatis();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kondisiAwal();
            noOtomatis();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["nomor_kamar"].Value.ToString();
                textBox2.Text = row.Cells["nama"].Value.ToString();
                comboBox1.Text = row.Cells["jenis"].Value.ToString();
                numericUpDown1.Text = row.Cells["kapasitas"].Value.ToString();
                textBox4.Text = row.Cells["harga"].Value.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || comboBox1.Text.Trim() == "" || textBox4.Text.Trim() == "" || numericUpDown1.Text.Trim() == "0")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                SqlConnection conn = konn.GetConn();

                cmd = new SqlCommand("update kamar set nama = '" + textBox2.Text + "', jenis = '" + comboBox1.Text + "', kapasitas ='" + numericUpDown1.Text + "', harga = '" + textBox4.Text + "'where nomor_kamar ='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Edit");
                kondisiAwal();
                noOtomatis();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();

            DialogResult konfir = MessageBox.Show("Apakah anda yaking ingin menghapus data ini?!");

            if (konfir == DialogResult.OK)
            {
                cmd = new SqlCommand("delete from kamar where nomor_kamar ='" + textBox1.Text + "'", conn);
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
