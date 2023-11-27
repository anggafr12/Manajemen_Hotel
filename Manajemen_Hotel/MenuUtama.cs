using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Manajemen_Hotel
{
    public partial class MenuUtama : Form
    {
        public static MenuUtama menu;
        MenuStrip mnstrip;

        Login frmlogin;

        void frmLogin_close(object sender, FormClosedEventArgs e)
        {
            frmlogin = null;
        }

        Reservasi frmReservasi;

        void frmReservasi_close(object sender, FormClosedEventArgs e)
        {
            frmReservasi = null;
        }

        Kamar frmKamar;

        void frmKamar_close(object sender, FormClosedEventArgs e)
        {
            frmKamar = null;
        }

        Pengunjung frmPengunjung;

        void frmPengunjung_close(object sender, FormClosedEventArgs e)
        {
            frmPengunjung = null;
        }

        Akun frmAkun;

        void frmAkun_close(object sender, FormClosedEventArgs e)
        {
            frmAkun = null;
        }

        Laporan frmLap;

        void frmLap_close(object sender, FormClosedEventArgs e)
        {
            frmLap = null;
        }

        void MenuTerkunci()
        {
            menuAkun.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            pengaturanAkunToolStripMenuItem.Enabled = false;
            logOutToolStripMenuItem.Enabled = false;
            pictureBox1.Visible = false;
            label9.Visible = false;
            menu = this;
        }

        public MenuUtama()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            MenuTerkunci();
        }

        private void MenuUtama_Load(object sender, EventArgs e)
        {
            MenuTerkunci();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmlogin == null)
            {
                frmlogin = new Login();
                frmlogin.FormClosed += new FormClosedEventHandler(frmLogin_close);
                frmlogin.ShowDialog();
            }
            else
            {
                frmlogin.Activate();
            }
        }

        private void pengaturanAkunToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frmAkun == null)
            {
                frmAkun = new Akun();
                frmAkun.FormClosed += new FormClosedEventHandler(frmAkun_close);
                frmAkun.ShowDialog();
            }
            else
            {
                frmAkun.Activate();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuTerkunci();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (frmPengunjung == null)
            {
                frmPengunjung = new Pengunjung();
                frmPengunjung.FormClosed += new FormClosedEventHandler(frmPengunjung_close);
                frmPengunjung.ShowDialog();
            }
            else
            {
                frmPengunjung.Activate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (frmKamar == null)
            {
                frmKamar = new Kamar();
                frmKamar.FormClosed += new FormClosedEventHandler(frmKamar_close);
                frmKamar.ShowDialog();
            }
            else
            {
                frmKamar.Activate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (frmReservasi == null)
            {
                frmReservasi = new Reservasi();
                frmReservasi.FormClosed += new FormClosedEventHandler(frmReservasi_close);
                frmReservasi.ShowDialog();
            }
            else
            {
                frmReservasi.Activate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (frmLap == null)
            {
                frmLap = new Laporan();
                frmLap.FormClosed += new FormClosedEventHandler(frmLap_close);
                frmLap.ShowDialog();
            }
            else
            {
                frmLap.Activate();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (frmlogin == null)
            {
                frmlogin = new Login();
                frmlogin.FormClosed += new FormClosedEventHandler(frmLogin_close);
                frmlogin.ShowDialog();
            }
            else
            {
                frmlogin.Activate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MenuTerkunci();
        }
    }
}
