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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Muhammed_Taha_Altunç
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OOI2R10;Initial Catalog=kutuphane;Integrated Security=True;");



        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                string kitapad = comboBox1.Text;
                string kitapstok = numericUpDown1.Text;


                baglanti.Open();

                SqlCommand kitapekle = new SqlCommand("insert into kitaplar(kitapad,kitapstok) values(@kad,@kstok)", baglanti);
                kitapekle.Parameters.AddWithValue("@kad", kitapad);
                kitapekle.Parameters.AddWithValue("@kstok", kitapstok);
                kitapekle.ExecuteNonQuery();
                MessageBox.Show("Kitap başarıyla eklendi.");
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir kitap adı yazın.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string stok = numericUpDown1.Text;



            baglanti.Open();
            string sqlQuery = "UPDATE kitaplar SET kitapstok = @stok WHERE kitapad = @kitapad";

            using (SqlCommand cmd = new SqlCommand(sqlQuery, baglanti))
            {
                cmd.Parameters.AddWithValue("@stok", stok);
                cmd.Parameters.AddWithValue("@kitapad", comboBox1.Text);
                cmd.ExecuteNonQuery();
            }
            baglanti.Close();

            MessageBox.Show("Stok güncellendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kitapsil = new SqlCommand("delete from kitaplar where kitapad=@kitapad", baglanti);
            kitapsil.Parameters.AddWithValue("@kitapad", comboBox1.Text);
            kitapsil.ExecuteNonQuery();
            MessageBox.Show("Kitap Listeden Kaldırıldı.");
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from kitaplar where kitapstok=0" ,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from kitaplar", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
