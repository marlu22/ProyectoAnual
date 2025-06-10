namespace Presentation
{
    partial class CambioContrasenaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblRepetir;
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
            lblNueva = new System.Windows.Forms.Label();
            lblRepetir = new System.Windows.Forms.Label();
            txtNueva = new System.Windows.Forms.TextBox();
            txtRepetir = new System.Windows.Forms.TextBox();
            btnCambiar = new System.Windows.Forms.Button();

            lblNueva.Text = "Nueva contraseña:";
            lblNueva.Location = new System.Drawing.Point(20, 20);
            lblNueva.Size = new System.Drawing.Size(120, 25);

            txtNueva.Location = new System.Drawing.Point(150, 20);
            txtNueva.Size = new System.Drawing.Size(180, 25);
            txtNueva.PasswordChar = '●';

            lblRepetir.Text = "Repetir contraseña:";
            lblRepetir.Location = new System.Drawing.Point(20, 60);
            lblRepetir.Size = new System.Drawing.Size(120, 25);

            txtRepetir.Location = new System.Drawing.Point(150, 60);
            txtRepetir.Size = new System.Drawing.Size(180, 25);
            txtRepetir.PasswordChar = '●';

            btnCambiar.Text = "Cambiar";
            btnCambiar.Location = new System.Drawing.Point(150, 100);
            btnCambiar.Size = new System.Drawing.Size(100, 30);

            ClientSize = new System.Drawing.Size(360, 150);
            Controls.Add(lblNueva);
            Controls.Add(txtNueva);
            Controls.Add(lblRepetir);
            Controls.Add(txtRepetir);
            Controls.Add(btnCambiar);
            Text = "Cambiar Contraseña";
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }
    }
}