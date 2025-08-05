using Presentation.Controles;
using Presentation.Theme;
using FontAwesome.Sharp;

namespace Presentation
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnCambiarPreguntas = new RoundedButton();
            btnCambiarContrasena = new RoundedButton();
            iconPictureBox1 = new IconPictureBox();
            iconPictureBox2 = new IconPictureBox();
            iconPictureBox3 = new IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            SuspendLayout();
            // 
            // btnCambiarPreguntas
            // 
            btnCambiarPreguntas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCambiarPreguntas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCambiarPreguntas.ForeColor = System.Drawing.Color.White;
            btnCambiarPreguntas.Location = new System.Drawing.Point(50, 110);
            btnCambiarPreguntas.Name = "btnCambiarPreguntas";
            btnCambiarPreguntas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            btnCambiarPreguntas.Size = new System.Drawing.Size(220, 45);
            btnCambiarPreguntas.TabIndex = 1;
            btnCambiarPreguntas.Text = "Cambiar Preguntas";
            btnCambiarPreguntas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCambiarContrasena
            // 
            btnCambiarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCambiarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCambiarContrasena.ForeColor = System.Drawing.Color.White;
            btnCambiarContrasena.Location = new System.Drawing.Point(50, 50);
            btnCambiarContrasena.Name = "btnCambiarContrasena";
            btnCambiarContrasena.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            btnCambiarContrasena.Size = new System.Drawing.Size(220, 45);
            btnCambiarContrasena.TabIndex = 0;
            btnCambiarContrasena.Text = "Cambiar Contraseña";
            btnCambiarContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            iconPictureBox1.IconChar = IconChar.Lock;
            iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            iconPictureBox1.IconFont = IconFont.Auto;
            iconPictureBox1.IconSize = 35;
            iconPictureBox1.Location = new System.Drawing.Point(280, 55);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new System.Drawing.Size(35, 35);
            iconPictureBox1.TabIndex = 2;
            iconPictureBox1.TabStop = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            iconPictureBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            iconPictureBox2.IconChar = IconChar.CircleQuestion;
            iconPictureBox2.IconColor = System.Drawing.SystemColors.ControlText;
            iconPictureBox2.IconFont = IconFont.Auto;
            iconPictureBox2.IconSize = 35;
            iconPictureBox2.Location = new System.Drawing.Point(280, 115);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new System.Drawing.Size(35, 35);
            iconPictureBox2.TabIndex = 3;
            iconPictureBox2.TabStop = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = System.Drawing.Color.FromArgb(43, 47, 58);
            iconPictureBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox3.IconChar = IconChar.MailForward;
            iconPictureBox3.IconColor = System.Drawing.SystemColors.ControlLightLight;
            iconPictureBox3.IconFont = IconFont.Auto;
            iconPictureBox3.IconSize = 40;
            iconPictureBox3.Location = new System.Drawing.Point(325, 155);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new System.Drawing.Size(40, 40);
            iconPictureBox3.TabIndex = 7;
            iconPictureBox3.TabStop = false;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.FromArgb(43, 37, 58);
            ClientSize = new System.Drawing.Size(377, 207);
            Controls.Add(iconPictureBox3);
            Controls.Add(iconPictureBox2);
            Controls.Add(iconPictureBox1);
            Controls.Add(btnCambiarPreguntas);
            Controls.Add(btnCambiarContrasena);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "UserForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Seguridad";
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RoundedButton btnCambiarPreguntas;
        private RoundedButton btnCambiarContrasena;
        private IconPictureBox iconPictureBox1;
        private IconPictureBox iconPictureBox2;
        private IconPictureBox iconPictureBox3;
    }
}