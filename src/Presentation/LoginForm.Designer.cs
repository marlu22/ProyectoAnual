using Presentation.Controles;
using Presentation.Theme;

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
        private System.Windows.Forms.CheckBox chkMostrarContrasena;

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
            txtUsuario = new RoundedTextBox();
            lblContrasena = new System.Windows.Forms.Label();
            txtContrasena = new RoundedTextBox();
            btnLogin = new RoundedButton();
            btnRecuperarContrasena = new RoundedButton();
            chkMostrarContrasena = new System.Windows.Forms.CheckBox();
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
            tableLayoutPanel.Controls.Add(chkMostrarContrasena, 1, 2);
            tableLayoutPanel.Controls.Add(btnLogin, 1, 3);
            tableLayoutPanel.Controls.Add(btnRecuperarContrasena, 1, 4);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new System.Drawing.Size(350, 250);
            tableLayoutPanel.TabIndex = 0;
            tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
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
            lblUsuario.ForeColor = ThemeColors.TextPrimary;
            lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            // 
            // txtUsuario
            // 
            txtUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            txtUsuario.Location = new System.Drawing.Point(128, 13);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(209, 25);
            txtUsuario.TabIndex = 1;
            txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtUsuario.BackColor = ThemeColors.Surface;
            txtUsuario.ForeColor = ThemeColors.TextPrimary;
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
            lblContrasena.ForeColor = ThemeColors.TextPrimary;
            lblContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            // 
            // txtContrasena
            // 
            txtContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            txtContrasena.Location = new System.Drawing.Point(128, 48);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new System.Drawing.Size(209, 25);
            txtContrasena.TabIndex = 3;
            txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtContrasena.BackColor = ThemeColors.Surface;
            txtContrasena.ForeColor = ThemeColors.TextPrimary;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.Location = new System.Drawing.Point(128, 83);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new System.Drawing.Size(139, 19);
            chkMostrarContrasena.TabIndex = 6;
            chkMostrarContrasena.Text = "Mostrar contraseña";
            chkMostrarContrasena.UseVisualStyleBackColor = true;
            chkMostrarContrasena.ForeColor = ThemeColors.TextSecondary;
            chkMostrarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            chkMostrarContrasena.CheckedChanged += new System.EventHandler(this.ChkMostrarContrasena_CheckedChanged);
            // 
            // btnLogin
            // 
            btnLogin.Dock = System.Windows.Forms.DockStyle.Right;
            btnLogin.Location = new System.Drawing.Point(217, 113);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(120, 39);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar sesión";
            btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            // 
            // btnRecuperarContrasena
            // 
            btnRecuperarContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            btnRecuperarContrasena.Name = "btnRecuperarContrasena";
            btnRecuperarContrasena.Size = new System.Drawing.Size(209, 39);
            btnRecuperarContrasena.TabIndex = 5;
            btnRecuperarContrasena.Text = "¿Olvidaste tu contraseña?";
            btnRecuperarContrasena.UseVisualStyleBackColor = false;
            ((RoundedButton)btnRecuperarContrasena).BaseColor = ThemeColors.Surface;
            ((RoundedButton)btnRecuperarContrasena).HoverColor = ThemeColors.Border;
            btnRecuperarContrasena.ForeColor = ThemeColors.TextSecondary;
            btnRecuperarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRecuperarContrasena.FlatAppearance.BorderSize = 0;
            btnRecuperarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            ClientSize = new System.Drawing.Size(800, 600);
            Controls.Add(tableLayoutPanel);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            BackColor = ThemeColors.FormBackground;
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