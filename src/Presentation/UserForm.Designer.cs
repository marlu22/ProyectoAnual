namespace Presentation
{
    partial class UserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCambiarContrasena = new System.Windows.Forms.Button();
            this.btnCambiarPreguntas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // btnCambiarContrasena
            //
            this.btnCambiarContrasena.Location = new System.Drawing.Point(12, 12);
            this.btnCambiarContrasena.Name = "btnCambiarContrasena";
            this.btnCambiarContrasena.Size = new System.Drawing.Size(150, 23);
            this.btnCambiarContrasena.TabIndex = 0;
            this.btnCambiarContrasena.Text = "Cambiar Contraseña";
            this.btnCambiarContrasena.UseVisualStyleBackColor = true;
            //
            // btnCambiarPreguntas
            //
            this.btnCambiarPreguntas.Location = new System.Drawing.Point(12, 41);
            this.btnCambiarPreguntas.Name = "btnCambiarPreguntas";
            this.btnCambiarPreguntas.Size = new System.Drawing.Size(150, 23);
            this.btnCambiarPreguntas.TabIndex = 1;
            this.btnCambiarPreguntas.Text = "Cambiar Preguntas";
            this.btnCambiarPreguntas.UseVisualStyleBackColor = true;
            //
            // UserForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 79);
            this.Controls.Add(this.btnCambiarPreguntas);
            this.Controls.Add(this.btnCambiarContrasena);
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCambiarContrasena;
        private System.Windows.Forms.Button btnCambiarPreguntas;
    }
}
