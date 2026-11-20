using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DOKUMEN_NEGARA
{
    public partial class FormAduan : Form
    {
        public FormAduan()
        {
            InitializeComponent();
            load_kategori();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tombol Kirim Aspirasi diklik.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void load_kategori() 
        {
            comboBox1.DataSource = Database.GetKategori();
            comboBox1.DisplayMember = "KetKategori";
            comboBox1.ValueMember = "IdKategori";
        }
    }
}
