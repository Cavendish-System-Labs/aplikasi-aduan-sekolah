namespace DOKUMEN_NEGARA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAduan form = new FormAduan();
            form.Show();
        }
    }
}
