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
            this.lblActual = new System.Windows.Forms.Label();
            this.lblNueva = new System.Windows.Forms.Label();
            this.lblRepetir = new System.Windows.Forms.Label();
            this.txtActual = new RoundedTextBox();
            this.txtNueva = new RoundedTextBox();
            this.txtRepetir = new RoundedTextBox();
            this.btnCambiar = new RoundedButton();
            this.SuspendLayout();

            // Form
            this.BackColor = ThemeColors.FormBackground;
            this.ForeColor = ThemeColors.TextPrimary;

            // lblActual
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new System.Drawing.Point(30, 33);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(110, 15);
            this.lblActual.TabIndex = 0;
            this.lblActual.Text = "Contraseña actual:";
            this.lblActual.ForeColor = ThemeColors.TextPrimary;

            // txtActual
            this.txtActual.Location = new System.Drawing.Point(160, 30);
            this.txtActual.Name = "txtActual";
            this.txtActual.PasswordChar = '●';
            this.txtActual.Size = new System.Drawing.Size(200, 28);
            this.txtActual.TabIndex = 1;

            // lblNueva
            this.lblNueva.AutoSize = true;
            this.lblNueva.Location = new System.Drawing.Point(30, 73);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Size = new System.Drawing.Size(112, 15);
            this.lblNueva.TabIndex = 2;
            this.lblNueva.Text = "Nueva contraseña:";
            this.lblNueva.ForeColor = ThemeColors.TextPrimary;

            // txtNueva
            this.txtNueva.Location = new System.Drawing.Point(160, 70);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.PasswordChar = '●';
            this.txtNueva.Size = new System.Drawing.Size(200, 28);
            this.txtNueva.TabIndex = 3;

            // lblRepetir
            this.lblRepetir.AutoSize = true;
            this.lblRepetir.Location = new System.Drawing.Point(30, 113);
            this.lblRepetir.Name = "lblRepetir";
            this.lblRepetir.Size = new System.Drawing.Size(115, 15);
            this.lblRepetir.TabIndex = 4;
            this.lblRepetir.Text = "Repetir contraseña:";
            this.lblRepetir.ForeColor = ThemeColors.TextPrimary;

            // txtRepetir
            this.txtRepetir.Location = new System.Drawing.Point(160, 110);
            this.txtRepetir.Name = "txtRepetir";
            this.txtRepetir.PasswordChar = '●';
            this.txtRepetir.Size = new System.Drawing.Size(200, 28);
            this.txtRepetir.TabIndex = 5;

            // btnCambiar
            this.btnCambiar.Location = new System.Drawing.Point(260, 150);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(100, 35);
            this.btnCambiar.TabIndex = 6;
            this.btnCambiar.Text = "Cambiar";

            // CambioContrasenaForm
            this.ClientSize = new System.Drawing.Size(400, 210);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.txtRepetir);
            this.Controls.Add(this.lblRepetir);
            this.Controls.Add(this.txtNueva);
            this.Controls.Add(this.lblNueva);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.lblActual);
            this.Text = "Cambiar Contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }
    }
}