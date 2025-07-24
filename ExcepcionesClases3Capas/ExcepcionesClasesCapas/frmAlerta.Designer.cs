namespace Vista
{
    partial class frmAlerta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlerta));
            lblAlerta = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblAlerta
            // 
            lblAlerta.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAlerta.ForeColor = Color.FromArgb(224, 224, 224);
            lblAlerta.Location = new Point(12, 5);
            lblAlerta.Name = "lblAlerta";
            lblAlerta.Size = new Size(294, 167);
            lblAlerta.TabIndex = 0;
            lblAlerta.Text = "label1";
            lblAlerta.TextAlign = ContentAlignment.MiddleCenter;
            lblAlerta.Click += lblAlerta_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(135, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 43);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(224, 224, 224);
            label1.Location = new Point(12, 152);
            label1.Name = "label1";
            label1.Size = new Size(294, 20);
            label1.TabIndex = 2;
            label1.Text = "Click para cerrar";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmAlerta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkRed;
            ClientSize = new Size(318, 181);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(lblAlerta);
            Name = "frmAlerta";
            Text = "frmAlerta";
            Load += frmAlerta_Load;
            KeyPress += frmAlerta_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblAlerta;
        private PictureBox pictureBox1;
        private Label label1;
    }
}