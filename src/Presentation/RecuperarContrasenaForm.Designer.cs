namespace Presentation
{
    partial class RecuperarContrasenaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPregunta1;
        private System.Windows.Forms.TextBox txtRespuesta1;
        private System.Windows.Forms.Label lblPregunta2;
        private System.Windows.Forms.TextBox txtRespuesta2;
        private System.Windows.Forms.Button btnRecuperar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsuario = new System.Windows.Forms.Label();
            txtUsuario = new System.Windows.Forms.TextBox();
            lblPregunta1 = new System.Windows.Forms.Label();
            txtRespuesta1 = new System.Windows.Forms.TextBox();
            lblPregunta2 = new System.Windows.Forms.Label();
            txtRespuesta2 = new System.Windows.Forms.TextBox();
            btnRecuperar = new System.Windows.Forms.Button();

            lblUsuario.Text = "Usuario:";
            lblUsuario.Location = new System.Drawing.Point(20, 20);
            lblUsuario.Size = new System.Drawing.Size(100, 25);

            txtUsuario.Location = new System.Drawing.Point(130, 20);
            txtUsuario.Size = new System.Drawing.Size(180, 25);

            lblPregunta1.Text = "Pregunta 1:";
            lblPregunta1.Location = new System.Drawing.Point(20, 60);
            lblPregunta1.Size = new System.Drawing.Size(100, 25);

            txtRespuesta1.Location = new System.Drawing.Point(130, 60);
            txtRespuesta1.Size = new System.Drawing.Size(180, 25);

            lblPregunta2.Text = "Pregunta 2:";
            lblPregunta2.Location = new System.Drawing.Point(20, 100);
            lblPregunta2.Size = new System.Drawing.Size(100, 25);

            txtRespuesta2.Location = new System.Drawing.Point(130, 100);
            txtRespuesta2.Size = new System.Drawing.Size(180, 25);

            btnRecuperar.Text = "Recuperar";
            btnRecuperar.Location = new System.Drawing.Point(130, 140);
            btnRecuperar.Size = new System.Drawing.Size(100, 30);

            ClientSize = new System.Drawing.Size(340, 190);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblPregunta1);
            Controls.Add(txtRespuesta1);
            Controls.Add(lblPregunta2);
            Controls.Add(txtRespuesta2);
            Controls.Add(btnRecuperar);
            Text = "Recuperar Contraseña";
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }
    }
}