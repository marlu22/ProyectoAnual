// src/Presentation/PreguntasSeguridadForm.Designer.cs
namespace Presentation
{
    partial class PreguntasSeguridadForm
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
            this.lblPregunta1 = new System.Windows.Forms.Label();
            this.txtRespuesta1 = new System.Windows.Forms.TextBox();
            this.lblPregunta2 = new System.Windows.Forms.Label();
            this.txtRespuesta2 = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblPregunta1
            //
            this.lblPregunta1.AutoSize = true;
            this.lblPregunta1.Location = new System.Drawing.Point(12, 15);
            this.lblPregunta1.Name = "lblPregunta1";
            this.lblPregunta1.Size = new System.Drawing.Size(158, 15);
            this.lblPregunta1.TabIndex = 0;
            this.lblPregunta1.Text = "¿Nombre de su primer mascota?";
            //
            // txtRespuesta1
            //
            this.txtRespuesta1.Location = new System.Drawing.Point(12, 33);
            this.txtRespuesta1.Name = "txtRespuesta1";
            this.txtRespuesta1.Size = new System.Drawing.Size(260, 23);
            this.txtRespuesta1.TabIndex = 1;
            //
            // lblPregunta2
            //
            this.lblPregunta2.AutoSize = true;
            this.lblPregunta2.Location = new System.Drawing.Point(12, 70);
            this.lblPregunta2.Name = "lblPregunta2";
            this.lblPregunta2.Size = new System.Drawing.Size(135, 15);
            this.lblPregunta2.TabIndex = 2;
            this.lblPregunta2.Text = "¿Lugar de nacimiento?";
            //
            // txtRespuesta2
            //
            this.txtRespuesta2.Location = new System.Drawing.Point(12, 88);
            this.txtRespuesta2.Name = "txtRespuesta2";
            this.txtRespuesta2.Size = new System.Drawing.Size(260, 23);
            this.txtRespuesta2.TabIndex = 3;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(100, 130);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            //
            // PreguntasSeguridadForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtRespuesta2);
            this.Controls.Add(this.lblPregunta2);
            this.Controls.Add(this.txtRespuesta1);
            this.Controls.Add(this.lblPregunta1);
            this.Name = "PreguntasSeguridadForm";
            this.Text = "Preguntas de Seguridad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPregunta1;
        private System.Windows.Forms.TextBox txtRespuesta1;
        private System.Windows.Forms.Label lblPregunta2;
        private System.Windows.Forms.TextBox txtRespuesta2;
        private System.Windows.Forms.Button btnGuardar;
    }
}
