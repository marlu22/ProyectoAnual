using Presentation.Controles;
using Presentation.Theme;

namespace Presentation
{
    partial class CambioContrasenaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblRepetir;
        private RoundedTextBox txtActual;
        private RoundedTextBox txtNueva;
        private RoundedTextBox txtRepetir;
        private RoundedButton btnCambiar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblActual = new System.Windows.Forms.Label();
            lblNueva = new System.Windows.Forms.Label();
            lblRepetir = new System.Windows.Forms.Label();
            txtActual = new RoundedTextBox();
            txtNueva = new RoundedTextBox();
            txtRepetir = new RoundedTextBox();
            btnCambiar = new RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblActual
            // 
            lblActual.AutoSize = true;
            lblActual.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblActual.Location = new System.Drawing.Point(18, 33);
            lblActual.Name = "lblActual";
            lblActual.Size = new System.Drawing.Size(138, 20);
            lblActual.TabIndex = 0;
            lblActual.Text = "Contraseña actual:";
            // 
            // lblNueva
            // 
            lblNueva.AutoSize = true;
            lblNueva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblNueva.Location = new System.Drawing.Point(18, 73);
            lblNueva.Name = "lblNueva";
            lblNueva.Size = new System.Drawing.Size(139, 20);
            lblNueva.TabIndex = 2;
            lblNueva.Text = "Nueva contraseña:";
            // 
            // lblRepetir
            // 
            lblRepetir.AutoSize = true;
            lblRepetir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblRepetir.Location = new System.Drawing.Point(18, 113);
            lblRepetir.Name = "lblRepetir";
            lblRepetir.Size = new System.Drawing.Size(145, 20);
            lblRepetir.TabIndex = 4;
            lblRepetir.Text = "Repetir contraseña:";
            // 
            // txtActual
            // 
            txtActual.BackColor = System.Drawing.Color.FromArgb(40, 40, 56);
            txtActual.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtActual.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtActual.ForeColor = System.Drawing.Color.White;
            txtActual.Location = new System.Drawing.Point(160, 30);
            txtActual.Name = "txtActual";
            txtActual.PasswordChar = '●';
            txtActual.Size = new System.Drawing.Size(200, 23);
            txtActual.TabIndex = 1;
            // 
            // txtNueva
            // 
            txtNueva.BackColor = System.Drawing.Color.FromArgb(40, 40, 56);
            txtNueva.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtNueva.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtNueva.ForeColor = System.Drawing.Color.White;
            txtNueva.Location = new System.Drawing.Point(160, 70);
            txtNueva.Name = "txtNueva";
            txtNueva.PasswordChar = '●';
            txtNueva.Size = new System.Drawing.Size(200, 23);
            txtNueva.TabIndex = 3;
            // 
            // txtRepetir
            // 
            txtRepetir.BackColor = System.Drawing.Color.FromArgb(40, 40, 56);
            txtRepetir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtRepetir.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtRepetir.ForeColor = System.Drawing.Color.White;
            txtRepetir.Location = new System.Drawing.Point(160, 110);
            txtRepetir.Name = "txtRepetir";
            txtRepetir.PasswordChar = '●';
            txtRepetir.Size = new System.Drawing.Size(200, 23);
            txtRepetir.TabIndex = 5;
            // 
            // btnCambiar
            // 
            btnCambiar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCambiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCambiar.ForeColor = System.Drawing.Color.White;
            btnCambiar.Location = new System.Drawing.Point(237, 151);
            btnCambiar.Name = "btnCambiar";
            btnCambiar.Size = new System.Drawing.Size(100, 35);
            btnCambiar.TabIndex = 6;
            btnCambiar.Text = "Cambiar";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(43, 47, 58);
            iconPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.MailForward;
            iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 37;
            iconPictureBox1.Location = new System.Drawing.Point(354, 180);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new System.Drawing.Size(37, 41);
            iconPictureBox1.TabIndex = 7;
            iconPictureBox1.TabStop = false;
            // 
            // CambioContrasenaForm
            // 
            BackColor = System.Drawing.Color.FromArgb(43, 47, 58);
            ClientSize = new System.Drawing.Size(403, 233);
            Controls.Add(iconPictureBox1);
            Controls.Add(btnCambiar);
            Controls.Add(txtRepetir);
            Controls.Add(lblRepetir);
            Controls.Add(txtNueva);
            Controls.Add(lblNueva);
            Controls.Add(txtActual);
            Controls.Add(lblActual);
            Cursor = System.Windows.Forms.Cursors.Arrow;
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CambioContrasenaForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Cambiar Contraseña";
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}