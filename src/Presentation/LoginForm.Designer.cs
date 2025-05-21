namespace Presentation
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();

            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);

            // 
            // lblUsuario
            // 
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // txtUsuario
            // 
            this.txtUsuario.Dock = System.Windows.Forms.DockStyle.Fill;

            // 
            // lblContrasena
            // 
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // txtContrasena
            // 
            this.txtContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContrasena.PasswordChar = '●';

            // 
            // btnLogin
            // 
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.Width = 120;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Right;

            // 
            // Add controls to tableLayoutPanel
            // 
            this.tableLayoutPanel.Controls.Add(this.lblUsuario, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtUsuario, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblContrasena, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtContrasena, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.btnLogin, 1, 2);

            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.ClientSize = new System.Drawing.Size(350, 140);
            this.Controls.Add(this.tableLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
        }
    }
}