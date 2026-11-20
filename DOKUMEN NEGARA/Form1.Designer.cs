namespace DOKUMEN_NEGARA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            pnlSiswa = new Panel();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(904, 55);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("SimSun-ExtG", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(788, 8);
            button1.Name = "button1";
            button1.Size = new Size(105, 40);
            button1.TabIndex = 0;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 55);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(904, 578);
            dataGridView1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.HotTrack;
            panel2.Controls.Add(pnlSiswa);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 548);
            panel2.Name = "panel2";
            panel2.Size = new Size(904, 85);
            panel2.TabIndex = 2;
            // 
            // pnlSiswa
            // 
            pnlSiswa.BackColor = SystemColors.GradientActiveCaption;
            pnlSiswa.Location = new Point(3, 3);
            pnlSiswa.Name = "pnlSiswa";
            pnlSiswa.Size = new Size(898, 79);
            pnlSiswa.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(3, 4);
            button2.Name = "button2";
            button2.Size = new Size(145, 51);
            button2.TabIndex = 0;
            button2.Text = "Buat Keluhan";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 633);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private DataGridView dataGridView1;
        private Button button2;
        private Panel panel2;
        private Panel pnlSiswa;
    }
}
