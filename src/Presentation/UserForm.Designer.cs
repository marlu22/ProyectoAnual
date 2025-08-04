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
            btnCambiarPreguntas = new System.Windows.Forms.Button();
            btnCambiarContrasena = new System.Windows.Forms.Button();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnCambiarPreguntas
            // 
            btnCambiarPreguntas.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            btnCambiarPreguntas.FlatAppearance.BorderSize = 0;
            btnCambiarPreguntas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnCambiarPreguntas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCambiarPreguntas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnCambiarPreguntas.ForeColor = System.Drawing.Color.White;
            btnCambiarPreguntas.Location = new System.Drawing.Point(101, 114);
            btnCambiarPreguntas.Name = "btnCambiarPreguntas";
            btnCambiarPreguntas.Size = new System.Drawing.Size(171, 40);
            btnCambiarPreguntas.TabIndex = 1;
            btnCambiarPreguntas.Text = "Cambiar Preguntas";
            btnCambiarPreguntas.UseVisualStyleBackColor = false;
            // 
            // btnCambiarContrasena
            // 
            btnCambiarContrasena.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            btnCambiarContrasena.FlatAppearance.BorderSize = 0;
            btnCambiarContrasena.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnCambiarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCambiarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnCambiarContrasena.ForeColor = System.Drawing.Color.White;
            btnCambiarContrasena.Location = new System.Drawing.Point(101, 51);
            btnCambiarContrasena.Name = "btnCambiarContrasena";
            btnCambiarContrasena.Size = new System.Drawing.Size(171, 40);
            btnCambiarContrasena.TabIndex = 0;
            btnCambiarContrasena.Text = "Cambiar Contraseña";
            btnCambiarContrasena.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            iconPictureBox1.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Lock;
            iconPictureBox1.IconColor = System.Drawing.Color.FromArgb(59, 130, 246);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new System.Drawing.Point(278, 51);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new System.Drawing.Size(40, 40);
            iconPictureBox1.TabIndex = 2;
            iconPictureBox1.TabStop = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            iconPictureBox2.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.CircleQuestion;
            iconPictureBox2.IconColor = System.Drawing.Color.FromArgb(59, 130, 246);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new System.Drawing.Point(278, 114);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new System.Drawing.Size(40, 40);
            iconPictureBox2.TabIndex = 3;
            iconPictureBox2.TabStop = false;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(24, 24, 36);
            ClientSize = new System.Drawing.Size(382, 203);
            Controls.Add(iconPictureBox2);
            Controls.Add(iconPictureBox1);
            Controls.Add(btnCambiarPreguntas);
            Controls.Add(btnCambiarContrasena);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "UserForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Seguridad";
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnCambiarPreguntas;
        private System.Windows.Forms.Button btnCambiarContrasena;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
    }
}