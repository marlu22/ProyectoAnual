using Presentation.Controles;
using Presentation.Theme;

namespace Presentation
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblContrasena;

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
            lblContrasena = new System.Windows.Forms.Label();
            chkMostrarContrasena = new System.Windows.Forms.CheckBox();
            txtContrasena = new RoundedTextBox();
            txtUsuario = new RoundedTextBox();
            btnLogin = new RoundedButton();
            btnRecuperarContrasena = new RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = System.Drawing.Color.FromArgb(43, 47, 58);
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(lblUsuario, 0, 0);
            tableLayoutPanel.Controls.Add(txtUsuario, 1, 0);
            tableLayoutPanel.Controls.Add(lblContrasena, 0, 1);
            tableLayoutPanel.Controls.Add(txtContrasena, 1, 1);
            tableLayoutPanel.Controls.Add(chkMostrarContrasena, 1, 2);
            tableLayoutPanel.Controls.Add(btnLogin, 1, 3);
            tableLayoutPanel.Controls.Add(btnRecuperarContrasena, 1, 4);
            tableLayoutPanel.Controls.Add(iconPictureBox1, 0, 2);
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
            tableLayoutPanel.Size = new System.Drawing.Size(800, 252);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblUsuario
            // 
            lblUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblUsuario.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            lblUsuario.Location = new System.Drawing.Point(13, 10);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(384, 35);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblContrasena
            // 
            lblContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            lblContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblContrasena.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            lblContrasena.Location = new System.Drawing.Point(13, 45);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new System.Drawing.Size(384, 35);
            lblContrasena.TabIndex = 2;
            lblContrasena.Text = "Contraseña:";
            lblContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            chkMostrarContrasena.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            chkMostrarContrasena.Location = new System.Drawing.Point(403, 83);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new System.Drawing.Size(158, 24);
            chkMostrarContrasena.TabIndex = 6;
            chkMostrarContrasena.Text = "Mostrar contraseña";
            chkMostrarContrasena.UseVisualStyleBackColor = true;
            chkMostrarContrasena.CheckedChanged += ChkMostrarContrasena_CheckedChanged;
            // 
            // txtContrasena
            // 
            txtContrasena.BackColor = System.Drawing.Color.FromArgb(40, 40, 56);
            txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtContrasena.ForeColor = System.Drawing.Color.White;
            txtContrasena.Location = new System.Drawing.Point(403, 48);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new System.Drawing.Size(384, 23);
            txtContrasena.TabIndex = 3;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = System.Drawing.Color.FromArgb(40, 40, 56);
            txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtUsuario.ForeColor = System.Drawing.Color.White;
            txtUsuario.Location = new System.Drawing.Point(403, 13);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(384, 23);
            txtUsuario.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Dock = System.Windows.Forms.DockStyle.Right;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.Location = new System.Drawing.Point(667, 113);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(120, 39);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar sesión";
            // 
            // btnRecuperarContrasena
            // 
            btnRecuperarContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            btnRecuperarContrasena.FlatAppearance.BorderSize = 0;
            btnRecuperarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRecuperarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRecuperarContrasena.ForeColor = System.Drawing.Color.White;
            btnRecuperarContrasena.Location = new System.Drawing.Point(403, 158);
            btnRecuperarContrasena.Name = "btnRecuperarContrasena";
            btnRecuperarContrasena.Size = new System.Drawing.Size(384, 81);
            btnRecuperarContrasena.TabIndex = 5;
            btnRecuperarContrasena.Text = "¿Olvidaste tu contraseña?";
            btnRecuperarContrasena.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(43, 47, 58);
            iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FaceLaughBeam;
            iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Regular;
            iconPictureBox1.IconSize = 24;
            iconPictureBox1.Location = new System.Drawing.Point(13, 83);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new System.Drawing.Size(384, 24);
            iconPictureBox1.TabIndex = 7;
            iconPictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            ClientSize = new System.Drawing.Size(800, 252);
            Controls.Add(tableLayoutPanel);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }
        private RoundedTextBox txtUsuario;
        private RoundedTextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkMostrarContrasena;
        private RoundedButton btnLogin;
        private RoundedButton btnRecuperarContrasena;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}