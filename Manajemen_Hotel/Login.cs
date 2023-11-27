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
    public partial class Login : Form
    {

        private SqlCommand cmd;
        private SqlCommand nm;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        koneksi konn = new koneksi();

        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            SqlConnection conn = konn.GetConn();
            {
                conn.Open();
                cmd = new SqlCommand("select * from akun where kode_akun='" + textBox1.Text + "' and pass = '" + textBox2.Text + "'", conn);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string jab = reader.GetString(reader.GetOrdinal("jabatan"));
                    if (jab == "ADMIN")
                    {

                        MenuUtama.menu.menuAkun.Enabled = true;
                        MenuUtama.menu.button1.Enabled = true;
                        MenuUtama.menu.button2.Enabled = true;
                        MenuUtama.menu.button3.Enabled = true;
                        MenuUtama.menu.button4.Enabled = true;
                        MenuUtama.menu.button5.Enabled = true;
                        MenuUtama.menu.button6.Enabled = true;
                        MenuUtama.menu.pictureBox1.Visible = true;
                        MenuUtama.menu.pictureBox3.Visible = false;
                        MenuUtama.menu.label9.Visible = true;
                        MenuUtama.menu.pengaturanAkunToolStripMenuItem.Enabled = true;
                        MenuUtama.menu.logOutToolStripMenuItem.Enabled = true;
                        conn.Close();
                        conn.Open();
                        nm = new SqlCommand("select nama from akun where kode_akun='" + textBox1.Text + "'", conn);
                        nm.ExecuteNonQuery();
                        reader2 = nm.ExecuteReader();
                        reader2.Read();
                        string nama = reader2.GetString(0);
                        MenuUtama.menu.label9.Text = "Admin  " + nama;
                    }
                    else if (jab == "FO")
                    {
                        MenuUtama.menu.menuAkun.Enabled = true;
                        MenuUtama.menu.button1.Enabled = true;
                        MenuUtama.menu.button2.Enabled = true;
                        MenuUtama.menu.button3.Enabled = false;
                        MenuUtama.menu.button4.Enabled = true;
                        MenuUtama.menu.button5.Enabled = false;
                        MenuUtama.menu.button6.Enabled = true;
                        MenuUtama.menu.pictureBox1.Visible = true;
                        MenuUtama.menu.pictureBox3.Visible = false;
                        MenuUtama.menu.label9.Visible = true;
                        MenuUtama.menu.pengaturanAkunToolStripMenuItem.Enabled = false;
                        MenuUtama.menu.logOutToolStripMenuItem.Enabled = true;
                        conn.Close();
                        conn.Open();
                        nm = new SqlCommand("select nama from akun where kode_akun='" + textBox1.Text + "'", conn);
                        nm.ExecuteNonQuery();
                        reader2 = nm.ExecuteReader();
                        reader2.Read();
                        string nama = reader2.GetString(0);
                        MenuUtama.menu.label9.Text = "FO  " + nama;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username atau Password Salah!!!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
