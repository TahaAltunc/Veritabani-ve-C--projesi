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

namespace Muhammed_Taha_Altunç
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OOI2R10;Initial Catalog=kutuphane;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Bilgileri Doldurun.");
                return;
            }

            if (GirisKontrol(kullaniciAdi, sifre))
            {
                MessageBox.Show("Giriş Yapıldı.");
                Form2 mainForm = new Form2();
                this.Hide();
                mainForm.Show();
            }
            else
            {   
                MessageBox.Show("Tekrar Deneyin.");
            }
        }
        private bool GirisKontrol(string kullaniciAdi, string sifre)
        {

            baglanti.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM yoneticiler WHERE yoneticiad = @yadi AND yoneticisifre = @sifre", baglanti);
            command.Parameters.AddWithValue("@yadi", kullaniciAdi);
            command.Parameters.AddWithValue("@sifre", sifre);

            using (SqlDataReader reader = command.ExecuteReader())

            {
                return reader.HasRows;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }
    }
}
