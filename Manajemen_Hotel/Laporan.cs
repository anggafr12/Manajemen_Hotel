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
    public partial class Laporan : Form
    {
        koneksi konn = new koneksi();
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        void kondisiAwal()
        {
            munculDataPengunjung();
            munculDataReservasi();
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

        void munculDataReservasi()
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select * from reservasi", conn);
            ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "reservasi");
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = "reservasi";
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Refresh();
        }

        public Laporan()
        {
            InitializeComponent();
        }

        private void Laporan_Load(object sender, EventArgs e)
        {
            kondisiAwal();
        }
    }
}
