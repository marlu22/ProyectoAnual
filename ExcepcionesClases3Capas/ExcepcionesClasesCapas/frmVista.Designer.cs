namespace ExcepcionesClasesCapas
{
    partial class frmVista
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
            label3 = new Label();
            lblTipoCuenta = new Label();
            lblSaldo = new Label();
            lblSaldoCta = new Label();
            lblDNI = new Label();
            lblNombre = new Label();
            txtNroCuenta = new TextBox();
            label2 = new Label();
            label1 = new Label();
            lblEdad = new Label();
            lblNombres = new Label();
            panel2 = new Panel();
            btnExtraer = new Button();
            txtExtraccion = new TextBox();
            LblExtraccion = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(lblTipoCuenta);
            panel1.Controls.Add(lblSaldo);
            panel1.Controls.Add(lblSaldoCta);
            panel1.Controls.Add(lblDNI);
            panel1.Controls.Add(lblNombre);
            panel1.Controls.Add(txtNroCuenta);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblEdad);
            panel1.Controls.Add(lblNombres);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(368, 230);
            panel1.TabIndex = 2;
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(12, 6);
            label3.Name = "label3";
            label3.Size = new Size(339, 35);
            label3.TabIndex = 15;
            label3.Text = "Ingrese el nro de cuenta y presione Enter";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTipoCuenta
            // 
            lblTipoCuenta.BorderStyle = BorderStyle.Fixed3D;
            lblTipoCuenta.Font = new Font("Century Gothic", 12F);
            lblTipoCuenta.Location = new Point(149, 87);
            lblTipoCuenta.Name = "lblTipoCuenta";
            lblTipoCuenta.Size = new Size(202, 27);
            lblTipoCuenta.TabIndex = 14;
            lblTipoCuenta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSaldo
            // 
            lblSaldo.BorderStyle = BorderStyle.Fixed3D;
            lblSaldo.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSaldo.Location = new Point(179, 186);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(172, 30);
            lblSaldo.TabIndex = 13;
            lblSaldo.Text = "0,00";
            lblSaldo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSaldoCta
            // 
            lblSaldoCta.AutoSize = true;
            lblSaldoCta.Font = new Font("Century Gothic", 12F);
            lblSaldoCta.Location = new Point(12, 191);
            lblSaldoCta.Name = "lblSaldoCta";
            lblSaldoCta.Size = new Size(146, 21);
            lblSaldoCta.TabIndex = 12;
            lblSaldoCta.Text = "Saldo en Cuenta:";
            // 
            // lblDNI
            // 
            lblDNI.BorderStyle = BorderStyle.Fixed3D;
            lblDNI.Font = new Font("Century Gothic", 12F);
            lblDNI.Location = new Point(149, 152);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(202, 29);
            lblDNI.TabIndex = 11;
            lblDNI.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblNombre
            // 
            lblNombre.BorderStyle = BorderStyle.Fixed3D;
            lblNombre.Font = new Font("Century Gothic", 12F);
            lblNombre.Location = new Point(149, 119);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(202, 29);
            lblNombre.TabIndex = 10;
            lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNroCuenta
            // 
            txtNroCuenta.Font = new Font("Century Gothic", 12F);
            txtNroCuenta.Location = new Point(149, 55);
            txtNroCuenta.Name = "txtNroCuenta";
            txtNroCuenta.Size = new Size(202, 27);
            txtNroCuenta.TabIndex = 9;
            txtNroCuenta.TextAlign = HorizontalAlignment.Right;
            txtNroCuenta.KeyPress += txtNroCuenta_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 8;
            label2.Text = "Nro de Cuenta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F);
            label1.Location = new Point(12, 93);
            label1.Name = "label1";
            label1.Size = new Size(111, 21);
            label1.TabIndex = 6;
            label1.Text = "Tipo Cuenta:";
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Font = new Font("Century Gothic", 12F);
            lblEdad.Location = new Point(12, 153);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(43, 21);
            lblEdad.TabIndex = 4;
            lblEdad.Text = "DNI:";
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Font = new Font("Century Gothic", 12F);
            lblNombres.Location = new Point(12, 120);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(77, 21);
            lblNombres.TabIndex = 2;
            lblNombres.Text = "Nombre:";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btnExtraer);
            panel2.Controls.Add(txtExtraccion);
            panel2.Controls.Add(LblExtraccion);
            panel2.Location = new Point(12, 257);
            panel2.Name = "panel2";
            panel2.Size = new Size(368, 108);
            panel2.TabIndex = 3;
            // 
            // btnExtraer
            // 
            btnExtraer.Font = new Font("Century Gothic", 12F);
            btnExtraer.Location = new Point(20, 55);
            btnExtraer.Name = "btnExtraer";
            btnExtraer.Size = new Size(331, 38);
            btnExtraer.TabIndex = 9;
            btnExtraer.Text = "EXTRAER";
            btnExtraer.UseVisualStyleBackColor = true;
            btnExtraer.Click += btnExtraer_Click;
            // 
            // txtExtraccion
            // 
            txtExtraccion.Enabled = false;
            txtExtraccion.Font = new Font("Century Gothic", 12F);
            txtExtraccion.Location = new Point(179, 15);
            txtExtraccion.Name = "txtExtraccion";
            txtExtraccion.Size = new Size(172, 27);
            txtExtraccion.TabIndex = 8;
            txtExtraccion.TextAlign = HorizontalAlignment.Right;
            txtExtraccion.Enter += txtExtraccion_Enter;
            txtExtraccion.KeyPress += txtExtraccion_KeyPress;
            // 
            // LblExtraccion
            // 
            LblExtraccion.AutoSize = true;
            LblExtraccion.Font = new Font("Century Gothic", 12F);
            LblExtraccion.Location = new Point(12, 21);
            LblExtraccion.Name = "LblExtraccion";
            LblExtraccion.Size = new Size(151, 21);
            LblExtraccion.TabIndex = 7;
            LblExtraccion.Text = "Importe a Extraer:";
            // 
            // frmVista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(394, 377);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmVista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EXTRACCIONES";
            Load += frmVista_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtEdad;
        private Label lblEdad;
        private Label lblNombres;
        private Panel panel2;
        private Button btnExtraer;
        private TextBox txtExtraccion;
        private Label LblExtraccion;
        private TextBox txtNroCuenta;
        private Label label2;
        private Label label1;
        private Label lblNombre;
        private Label lblDNI;
        private Label lblSaldo;
        private Label lblSaldoCta;
        private Label lblTipoCuenta;
        private Label label3;
    }
}
