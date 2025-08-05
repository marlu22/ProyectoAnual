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
            this.btnCambiarPreguntas = new RoundedButton();
            this.btnCambiarContrasena = new RoundedButton();
            this.iconPictureBox1 = new IconPictureBox();
            this.iconPictureBox2 = new IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            this.SuspendLayout();

            // Form
            this.BackColor = ThemeColors.FormBackground;

            // btnCambiarContrasena
            this.btnCambiarContrasena.Location = new System.Drawing.Point(50, 50);
            this.btnCambiarContrasena.Name = "btnCambiarContrasena";
            this.btnCambiarContrasena.Size = new System.Drawing.Size(220, 45);
            this.btnCambiarContrasena.TabIndex = 0;
            this.btnCambiarContrasena.Text = "Cambiar Contraseña";
            this.btnCambiarContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCambiarContrasena.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);


            // btnCambiarPreguntas
            this.btnCambiarPreguntas.Location = new System.Drawing.Point(50, 110);
            this.btnCambiarPreguntas.Name = "btnCambiarPreguntas";
            this.btnCambiarPreguntas.Size = new System.Drawing.Size(220, 45);
            this.btnCambiarPreguntas.TabIndex = 1;
            this.btnCambiarPreguntas.Text = "Cambiar Preguntas";
            this.btnCambiarPreguntas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCambiarPreguntas.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // iconPictureBox1
            this.iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox1.ForeColor = ThemeColors.Primary;
            this.iconPictureBox1.IconChar = IconChar.Lock;
            this.iconPictureBox1.IconColor = ThemeColors.Primary;
            this.iconPictureBox1.IconFont = IconFont.Auto;
            this.iconPictureBox1.IconSize = 35;
            this.iconPictureBox1.Location = new System.Drawing.Point(280, 55);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(35, 35);
            this.iconPictureBox1.TabIndex = 2;
            this.iconPictureBox1.TabStop = false;

            // iconPictureBox2
            this.iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox2.ForeColor = ThemeColors.Primary;
            this.iconPictureBox2.IconChar = IconChar.CircleQuestion;
            this.iconPictureBox2.IconColor = ThemeColors.Primary;
            this.iconPictureBox2.IconFont = IconFont.Auto;
            this.iconPictureBox2.IconSize = 35;
            this.iconPictureBox2.Location = new System.Drawing.Point(280, 115);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(35, 35);
            this.iconPictureBox2.TabIndex = 3;
            this.iconPictureBox2.TabStop = false;

            // UserForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 203);
            this.Controls.Add(this.iconPictureBox2);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.btnCambiarPreguntas);
            this.Controls.Add(this.btnCambiarContrasena);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seguridad";
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private RoundedButton btnCambiarPreguntas;
        private RoundedButton btnCambiarContrasena;
        private IconPictureBox iconPictureBox1;
        private IconPictureBox iconPictureBox2;
    }
}