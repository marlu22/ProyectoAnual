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
        private System.Windows.Forms.Button btnRecuperarContrasena;

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
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            lblUsuario = new System.Windows.Forms.Label();
            txtUsuario = new System.Windows.Forms.TextBox();
            lblContrasena = new System.Windows.Forms.Label();
            txtContrasena = new System.Windows.Forms.TextBox();
            btnLogin = new System.Windows.Forms.Button();
            btnRecuperarContrasena = new System.Windows.Forms.Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            tableLayoutPanel.Controls.Add(lblUsuario, 0, 0);
            tableLayoutPanel.Controls.Add(txtUsuario, 1, 0);
            tableLayoutPanel.Controls.Add(lblContrasena, 0, 1);
            tableLayoutPanel.Controls.Add(txtContrasena, 1, 1);
            tableLayoutPanel.Controls.Add(btnLogin, 1, 2);
            tableLayoutPanel.Controls.Add(btnRecuperarContrasena, 1, 3);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new System.Drawing.Size(350, 220);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblUsuario
            // 
            lblUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            lblUsuario.Location = new System.Drawing.Point(13, 10);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(109, 35);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsuario
            // 
            txtUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            txtUsuario.Location = new System.Drawing.Point(128, 13);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(209, 25);
            txtUsuario.TabIndex = 1;
            // 
            // lblContrasena
            // 
            lblContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            lblContrasena.Location = new System.Drawing.Point(13, 45);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new System.Drawing.Size(109, 35);
            lblContrasena.TabIndex = 2;
            lblContrasena.Text = "Contraseña:";
            lblContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtContrasena
            // 
            txtContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            txtContrasena.Location = new System.Drawing.Point(128, 48);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new System.Drawing.Size(209, 25);
            txtContrasena.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Dock = System.Windows.Forms.DockStyle.Right;
            btnLogin.Location = new System.Drawing.Point(217, 83);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(120, 84);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar sesión";
            // 
            // btnRecuperarContrasena
            // 
            btnRecuperarContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            btnRecuperarContrasena.Name = "btnRecuperarContrasena";
            btnRecuperarContrasena.Size = new System.Drawing.Size(209, 29);
            btnRecuperarContrasena.TabIndex = 5;
            btnRecuperarContrasena.Text = "¿Olvidaste tu contraseña?";
            btnRecuperarContrasena.UseVisualStyleBackColor = true;
            btnRecuperarContrasena.Click += BtnRecuperarContrasena_Click;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            ClientSize = new System.Drawing.Size(350, 220);
            Controls.Add(tableLayoutPanel);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}