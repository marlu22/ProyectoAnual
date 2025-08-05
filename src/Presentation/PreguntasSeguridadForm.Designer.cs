using Presentation.Controles;
using Presentation.Theme;
using System.Drawing;

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
            this.btnGuardar = new RoundedButton();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            //
            // btnGuardar
            //
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(387, 246);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 33);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            //
            // flowLayoutPanel
            //
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(460, 228);
            this.flowLayoutPanel.TabIndex = 5;
            this.flowLayoutPanel.BackColor = Color.Transparent;
            //
            // PreguntasSeguridadForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 291);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.btnGuardar);
            this.Name = "PreguntasSeguridadForm";
            this.Text = "Preguntas de Seguridad";
            this.BackColor = ThemeColors.FormBackground;
            this.Load += new System.EventHandler(this.PreguntasSeguridadForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private RoundedButton btnGuardar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}
