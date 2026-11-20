namespace DOKUMEN_NEGARA
{
    partial class FormAduan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox4 = new TextBox();
            button1 = new Button();
            label5 = new Label();
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            button2 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(31, 99);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(367, 33);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(36, 82);
            label1.Name = "label1";
            label1.Size = new Size(40, 19);
            label1.TabIndex = 1;
            label1.Text = "NISN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(35, 145);
            label2.Name = "label2";
            label2.Size = new Size(60, 19);
            label2.TabIndex = 1;
            label2.Text = "Kategori";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(31, 223);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(367, 33);
            textBox3.TabIndex = 0;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(35, 206);
            label3.Name = "label3";
            label3.Size = new Size(47, 19);
            label3.TabIndex = 1;
            label3.Text = "Lokasi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(35, 271);
            label4.Name = "label4";
            label4.Size = new Size(132, 19);
            label4.TabIndex = 1;
            label4.Text = "Keterangan Keluhan";
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox4.Location = new Point(31, 288);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(367, 140);
            textBox4.TabIndex = 0;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(64, 132, 197);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(31, 453);
            button1.Name = "button1";
            button1.Size = new Size(367, 48);
            button1.TabIndex = 2;
            button1.Text = "Kirim Aspirasi";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(69, 16);
            label5.Name = "label5";
            label5.Size = new Size(294, 34);
            label5.TabIndex = 3;
            label5.Text = "Form Aplikasi Siswa";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 132, 197);
            panel1.Controls.Add(label5);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(432, 66);
            panel1.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI Emoji", 14.25F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(31, 162);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(367, 34);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.FlatAppearance.BorderColor = Color.FromArgb(64, 132, 197);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.FromArgb(64, 132, 197);
            button2.Location = new Point(31, 507);
            button2.Name = "button2";
            button2.Size = new Size(367, 48);
            button2.TabIndex = 6;
            button2.Text = "Cek Histori";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // FormAduan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 582);
            ControlBox = false;
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox4);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.None;
            HelpButton = true;
            Name = "FormAduan";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormAduan";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
        private TextBox textBox4;
        private Button button1;
        private Label label5;
        private Panel panel1;
        private ComboBox comboBox1;
        private Button button2;
    }
}