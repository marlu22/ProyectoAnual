using Presentation.Controles;
using Presentation.Theme;

namespace Presentation
{
    partial class TwoFactorAuthForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblInfo;
        private RoundedTextBox txtCodigo;
        private RoundedButton btnVerificar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtCodigo = new RoundedTextBox();
            this.btnVerificar = new RoundedButton();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.BackColor = ThemeColors.FormBackground;
            this.ForeColor = ThemeColors.TextPrimary;

            // tableLayoutPanel
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblInfo, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtCodigo, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnVerificar, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(340, 180);
            this.tableLayoutPanel.TabIndex = 0;

            // lblInfo
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(23, 20);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(294, 50);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Se ha enviado un código a su correo. Por favor, ingréselo a continuación.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.ForeColor = ThemeColors.TextPrimary;

            // txtCodigo
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.Location = new System.Drawing.Point(23, 75);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(294, 28);
            this.txtCodigo.TabIndex = 1;

            // btnVerificar
            this.btnVerificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerificar.Location = new System.Drawing.Point(217, 113);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(100, 35);
            this.btnVerificar.TabIndex = 2;
            this.btnVerificar.Text = "Verificar";

            // TwoFactorAuthForm
            this.AcceptButton = this.btnVerificar;
            this.ClientSize = new System.Drawing.Size(340, 180);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TwoFactorAuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Verificación de Dos Factores";
            this.tableLayoutPanel.ResumeLayout(false);
            this.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
