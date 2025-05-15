namespace Presentation
{
    partial class Form1
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
            FrmAltayBaja = new System.Windows.Forms.TabControl();
            tbcAlta = new System.Windows.Forms.TabPage();
            btnLimpiar = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            cbxRol = new System.Windows.Forms.ComboBox();
            lblRol = new System.Windows.Forms.Label();
            txtEmail = new System.Windows.Forms.TextBox();
            lblEmail = new System.Windows.Forms.Label();
            txtDni = new System.Windows.Forms.TextBox();
            lblDni = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblNombre = new System.Windows.Forms.Label();
            tbcBaja = new System.Windows.Forms.TabPage();
            lblBuscarDni = new System.Windows.Forms.Label();
            txtBuscarDni = new System.Windows.Forms.TextBox();
            btnBuscar = new System.Windows.Forms.Button();
            gpxDatosUsuario = new System.Windows.Forms.GroupBox();
            lblgpxNombre = new System.Windows.Forms.Label();
            lblgpxApellido = new System.Windows.Forms.Label();
            btnDarBaja = new System.Windows.Forms.Button();
            FrmAltayBaja.SuspendLayout();
            tbcAlta.SuspendLayout();
            tbcBaja.SuspendLayout();
            gpxDatosUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // FrmAltayBaja
            // 
            FrmAltayBaja.Controls.Add(tbcAlta);
            FrmAltayBaja.Controls.Add(tbcBaja);
            FrmAltayBaja.Dock = System.Windows.Forms.DockStyle.Fill;
            FrmAltayBaja.Location = new System.Drawing.Point(0, 0);
            FrmAltayBaja.Name = "FrmAltayBaja";
            FrmAltayBaja.SelectedIndex = 0;
            FrmAltayBaja.Size = new System.Drawing.Size(476, 405);
            FrmAltayBaja.TabIndex = 0;
            FrmAltayBaja.UseWaitCursor = true;
            // 
            // tbcAlta
            // 
            tbcAlta.Controls.Add(btnLimpiar);
            tbcAlta.Controls.Add(btnGuardar);
            tbcAlta.Controls.Add(cbxRol);
            tbcAlta.Controls.Add(lblRol);
            tbcAlta.Controls.Add(txtEmail);
            tbcAlta.Controls.Add(lblEmail);
            tbcAlta.Controls.Add(txtDni);
            tbcAlta.Controls.Add(lblDni);
            tbcAlta.Controls.Add(txtApellido);
            tbcAlta.Controls.Add(lblApellido);
            tbcAlta.Controls.Add(txtNombre);
            tbcAlta.Controls.Add(lblNombre);
            tbcAlta.Location = new System.Drawing.Point(4, 29);
            tbcAlta.Name = "tbcAlta";
            tbcAlta.Padding = new System.Windows.Forms.Padding(3);
            tbcAlta.Size = new System.Drawing.Size(468, 372);
            tbcAlta.TabIndex = 0;
            tbcAlta.Text = "Alta";
            tbcAlta.UseVisualStyleBackColor = true;
            tbcAlta.UseWaitCursor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new System.Drawing.Point(334, 257);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(94, 29);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.UseWaitCursor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new System.Drawing.Point(220, 257);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(94, 29);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.UseWaitCursor = true;
            // 
            // cbxRol
            // 
            cbxRol.FormattingEnabled = true;
            cbxRol.Items.AddRange(new object[] { "Usuario", "Administrador" });
            cbxRol.Location = new System.Drawing.Point(-1, 252);
            cbxRol.Name = "cbxRol";
            cbxRol.Size = new System.Drawing.Size(151, 28);
            cbxRol.TabIndex = 9;
            cbxRol.UseWaitCursor = true;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new System.Drawing.Point(1, 221);
            lblRol.Name = "lblRol";
            lblRol.Size = new System.Drawing.Size(31, 20);
            lblRol.TabIndex = 8;
            lblRol.Text = "Rol";
            lblRol.UseWaitCursor = true;
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(3, 187);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(125, 27);
            txtEmail.TabIndex = 7;
            txtEmail.UseWaitCursor = true;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(1, 166);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(46, 20);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email";
            lblEmail.UseWaitCursor = true;
            // 
            // txtDni
            // 
            txtDni.Location = new System.Drawing.Point(1, 136);
            txtDni.Name = "txtDni";
            txtDni.Size = new System.Drawing.Size(125, 27);
            txtDni.TabIndex = 5;
            txtDni.UseWaitCursor = true;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new System.Drawing.Point(1, 112);
            lblDni.Name = "lblDni";
            lblDni.Size = new System.Drawing.Size(35, 20);
            lblDni.TabIndex = 4;
            lblDni.Text = "DNI";
            lblDni.UseWaitCursor = true;
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(0, 82);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(125, 27);
            txtApellido.TabIndex = 3;
            txtApellido.UseWaitCursor = true;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new System.Drawing.Point(3, 59);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(66, 20);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido";
            lblApellido.UseWaitCursor = true;
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(0, 29);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(125, 27);
            txtNombre.TabIndex = 1;
            txtNombre.UseWaitCursor = true;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            lblNombre.Location = new System.Drawing.Point(3, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            lblNombre.UseWaitCursor = true;
            // 
            // tbcBaja
            // 
            tbcBaja.Controls.Add(btnDarBaja);
            tbcBaja.Controls.Add(gpxDatosUsuario);
            tbcBaja.Controls.Add(btnBuscar);
            tbcBaja.Controls.Add(txtBuscarDni);
            tbcBaja.Controls.Add(lblBuscarDni);
            tbcBaja.Location = new System.Drawing.Point(4, 29);
            tbcBaja.Name = "tbcBaja";
            tbcBaja.Padding = new System.Windows.Forms.Padding(3);
            tbcBaja.Size = new System.Drawing.Size(468, 372);
            tbcBaja.TabIndex = 1;
            tbcBaja.Text = "Baja";
            tbcBaja.UseVisualStyleBackColor = true;
            tbcBaja.UseWaitCursor = true;
            // 
            // lblBuscarDni
            // 
            lblBuscarDni.AutoSize = true;
            lblBuscarDni.Location = new System.Drawing.Point(17, 14);
            lblBuscarDni.Name = "lblBuscarDni";
            lblBuscarDni.Size = new System.Drawing.Size(109, 20);
            lblBuscarDni.TabIndex = 0;
            lblBuscarDni.Text = "Buscar por DNI";
            // 
            // txtBuscarDni
            // 
            txtBuscarDni.Location = new System.Drawing.Point(14, 45);
            txtBuscarDni.Name = "txtBuscarDni";
            txtBuscarDni.Size = new System.Drawing.Size(125, 27);
            txtBuscarDni.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new System.Drawing.Point(19, 80);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new System.Drawing.Size(94, 29);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // gpxDatosUsuario
            // 
            gpxDatosUsuario.Controls.Add(lblgpxApellido);
            gpxDatosUsuario.Controls.Add(lblgpxNombre);
            gpxDatosUsuario.Location = new System.Drawing.Point(19, 127);
            gpxDatosUsuario.Name = "gpxDatosUsuario";
            gpxDatosUsuario.Size = new System.Drawing.Size(250, 125);
            gpxDatosUsuario.TabIndex = 3;
            gpxDatosUsuario.TabStop = false;
            gpxDatosUsuario.Text = "Datos encontrados";
            // 
            // lblgpxNombre
            // 
            lblgpxNombre.AutoSize = true;
            lblgpxNombre.Location = new System.Drawing.Point(12, 25);
            lblgpxNombre.Name = "lblgpxNombre";
            lblgpxNombre.Size = new System.Drawing.Size(64, 20);
            lblgpxNombre.TabIndex = 0;
            lblgpxNombre.Text = "Nombre";
            // 
            // lblgpxApellido
            // 
            lblgpxApellido.AutoSize = true;
            lblgpxApellido.Location = new System.Drawing.Point(13, 57);
            lblgpxApellido.Name = "lblgpxApellido";
            lblgpxApellido.Size = new System.Drawing.Size(66, 20);
            lblgpxApellido.TabIndex = 1;
            lblgpxApellido.Text = "Apellido";
            // 
            // btnDarBaja
            // 
            btnDarBaja.Location = new System.Drawing.Point(21, 287);
            btnDarBaja.Name = "btnDarBaja";
            btnDarBaja.Size = new System.Drawing.Size(133, 29);
            btnDarBaja.TabIndex = 4;
            btnDarBaja.Text = "Dar de baja";
            btnDarBaja.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(476, 405);
            Controls.Add(FrmAltayBaja);
            Name = "Form1";
            Text = "Alta y Baja ";
            TransparencyKey = System.Drawing.Color.FromArgb(128, 128, 255);
            UseWaitCursor = true;
            FrmAltayBaja.ResumeLayout(false);
            tbcAlta.ResumeLayout(false);
            tbcAlta.PerformLayout();
            tbcBaja.ResumeLayout(false);
            tbcBaja.PerformLayout();
            gpxDatosUsuario.ResumeLayout(false);
            gpxDatosUsuario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl FrmAltayBaja;
        private System.Windows.Forms.TabPage tbcAlta;
        private System.Windows.Forms.TabPage tbcBaja;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cbxRol;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox gpxDatosUsuario;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarDni;
        private System.Windows.Forms.Label lblBuscarDni;
        private System.Windows.Forms.Button btnDarBaja;
        private System.Windows.Forms.Label lblgpxApellido;
        private System.Windows.Forms.Label lblgpxNombre;
    }
}