namespace Presentation
{
    partial class CambioContrasenaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblRepetir;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.TextBox txtRepetir;
        private System.Windows.Forms.Button btnCambiar;

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
            this.txtActual = new System.Windows.Forms.TextBox();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.txtRepetir = new System.Windows.Forms.TextBox();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblActual
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new System.Drawing.Point(20, 23);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(110, 15);
            this.lblActual.TabIndex = 0;
            this.lblActual.Text = "Contraseña actual:";

            // txtActual
            this.txtActual.Location = new System.Drawing.Point(150, 20);
            this.txtActual.Name = "txtActual";
            this.txtActual.PasswordChar = '●';
            this.txtActual.Size = new System.Drawing.Size(180, 23);
            this.txtActual.TabIndex = 1;

            // lblNueva
            this.lblNueva.AutoSize = true;
            this.lblNueva.Location = new System.Drawing.Point(20, 63);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Size = new System.Drawing.Size(112, 15);
            this.lblNueva.TabIndex = 2;
            this.lblNueva.Text = "Nueva contraseña:";

            // txtNueva
            this.txtNueva.Location = new System.Drawing.Point(150, 60);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.PasswordChar = '●';
            this.txtNueva.Size = new System.Drawing.Size(180, 23);
            this.txtNueva.TabIndex = 3;

            // lblRepetir
            this.lblRepetir.AutoSize = true;
            this.lblRepetir.Location = new System.Drawing.Point(20, 103);
            this.lblRepetir.Name = "lblRepetir";
            this.lblRepetir.Size = new System.Drawing.Size(115, 15);
            this.lblRepetir.TabIndex = 4;
            this.lblRepetir.Text = "Repetir contraseña:";

            // txtRepetir
            this.txtRepetir.Location = new System.Drawing.Point(150, 100);
            this.txtRepetir.Name = "txtRepetir";
            this.txtRepetir.PasswordChar = '●';
            this.txtRepetir.Size = new System.Drawing.Size(180, 23);
            this.txtRepetir.TabIndex = 5;

            // btnCambiar
            this.btnCambiar.Location = new System.Drawing.Point(230, 140);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(100, 30);
            this.btnCambiar.TabIndex = 6;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.UseVisualStyleBackColor = true;

            // CambioContrasenaForm
            this.ClientSize = new System.Drawing.Size(360, 190);
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