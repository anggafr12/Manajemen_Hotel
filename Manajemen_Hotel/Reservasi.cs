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
    public partial class Reservasi : Form
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
            numericUpDown1.Text = "0";
            munculDataReservasi();
            munculKamar();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        void munculKamar()
        {
            SqlDataReader reader = null;
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select nomor_kamar from kamar", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string nomorKamar = reader["nomor_kamar"].ToString();
                comboBox1.Items.Add(nomorKamar);
            }
            reader.Close();

            conn.Close();
        }

        void munculDataReservasi()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from reservasi", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "reservasi");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "reservasi";
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
            cmd = new SqlCommand("select kode_reservasi from reservasi where kode_reservasi in(select max(kode_reservasi) from reservasi) order by kode_reservasi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kode_reservasi"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "RSV" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else
            {
                urutan = "RSV001";
            }
            rd.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }

        public Reservasi()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reservasi_Load(object sender, EventArgs e)
        {
            kondisiAwal();
            noOtomatis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "" || numericUpDown1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan Semua Form Terisi");
            }
            else
            {
                cmd = new SqlCommand("insert into reservasi values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + numericUpDown1.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + labelTotal.Text + "')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil di Input");
                kondisiAwal();
                noOtomatis();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["kode_reservasi"].Value.ToString();
                textBox2.Text = row.Cells["email"].Value.ToString();
                textBox3.Text = row.Cells["nama"].Value.ToString();
                comboBox1.Text = row.Cells["kamar"].Value.ToString();
                numericUpDown1.Text = row.Cells["jumlah"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["check_in"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["check_out"].Value);
                labelTotal.Text = row.Cells["total"].Value.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim(); // Mendapatkan email dari TextBox2

            if (!string.IsNullOrEmpty(email)) // Memastikan email tidak kosong
            {
                SqlConnection conn = konn.GetConn();
                SqlDataReader rd;

                conn.Open();

                cmd = new SqlCommand("SELECT nama FROM pengunjung WHERE email = @email;", conn);
                cmd.Parameters.AddWithValue("@email", email);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    string nama = rd["nama"].ToString();
                    textBox3.Text = nama; // Mengisi TextBox3 dengan nama pengunjung

                    rd.Close();
                }
                else
                {
                    textBox3.Text = string.Empty; // Mengosongkan TextBox3 jika email tidak ditemukan
                }

                conn.Close();
            }
            else
            {
                textBox3.Text = string.Empty; // Mengosongkan TextBox3 jika email kosong
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            SqlDataReader rd;

            conn.Open();

            cmd = new SqlCommand("SELECT harga, kapasitas FROM kamar WHERE nomor_kamar = @nomor_kamar;", conn);
            cmd.Parameters.AddWithValue("@nomor_kamar", comboBox1.Text);
            rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                decimal hargaKamar = Convert.ToDecimal(rd["harga"]);
                int kapasitasKamar = Convert.ToInt32(rd["kapasitas"]);

                rd.Close();

                int jumlahOrang = Convert.ToInt32(numericUpDown1.Value);

                if (jumlahOrang > kapasitasKamar)
                {
                    MessageBox.Show("Jumlah orang melebihi kapasitas kamar!");
                    return;
                }

                TimeSpan selisihHari = dateTimePicker2.Value.Subtract(dateTimePicker1.Value);
                decimal totalHarga = hargaKamar * Convert.ToDecimal(selisihHari.Days);

                labelTotal.Text = totalHarga.ToString();
            }
            else
            {
                MessageBox.Show("Nomor kamar tidak valid!");
            }

            conn.Close();
        }


    }
}
