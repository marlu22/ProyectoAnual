namespace Presentation
{
    partial class RecuperarContrasenaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.GroupBox grpPreguntas;
        private System.Windows.Forms.FlowLayoutPanel pnlPreguntas;
        private System.Windows.Forms.Button btnRecuperar;
        private System.Windows.Forms.Label lblInfo;

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
            this.grpUsuario = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.grpPreguntas = new System.Windows.Forms.GroupBox();
            this.pnlPreguntas = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRecuperar = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpUsuario.SuspendLayout();
            this.grpPreguntas.SuspendLayout();
            this.SuspendLayout();
            //
            // grpUsuario
            //
            this.grpUsuario.Controls.Add(this.lblUsuario);
            this.grpUsuario.Controls.Add(this.txtUsuario);
            this.grpUsuario.Controls.Add(this.btnContinuar);
            this.grpUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUsuario.Location = new System.Drawing.Point(12, 35);
            this.grpUsuario.Name = "grpUsuario";
            this.grpUsuario.Padding = new System.Windows.Forms.Padding(10);
            this.grpUsuario.Size = new System.Drawing.Size(460, 70);
            this.grpUsuario.TabIndex = 0;
            this.grpUsuario.TabStop = false;
            this.grpUsuario.Text = "Paso 1: Ingrese su usuario";
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(13, 32);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(50, 15);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(69, 29);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(250, 23);
            this.txtUsuario.TabIndex = 1;
            //
            // btnContinuar
            //
            this.btnContinuar.Location = new System.Drawing.Point(335, 27);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(110, 25);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            //
            // grpPreguntas
            //
            this.grpPreguntas.Controls.Add(this.pnlPreguntas);
            this.grpPreguntas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPreguntas.Location = new System.Drawing.Point(12, 111);
            this.grpPreguntas.Name = "grpPreguntas";
            this.grpPreguntas.Padding = new System.Windows.Forms.Padding(10);
            this.grpPreguntas.Size = new System.Drawing.Size(460, 200);
            this.grpPreguntas.TabIndex = 1;
            this.grpPreguntas.TabStop = false;
            this.grpPreguntas.Text = "Paso 2: Responda sus preguntas de seguridad";
            this.grpPreguntas.Visible = false;
            //
            // pnlPreguntas
            //
            this.pnlPreguntas.AutoScroll = true;
            this.pnlPreguntas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreguntas.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlPreguntas.Location = new System.Drawing.Point(10, 26);
            this.pnlPreguntas.Name = "pnlPreguntas";
            this.pnlPreguntas.Size = new System.Drawing.Size(440, 164);
            this.pnlPreguntas.TabIndex = 0;
            this.pnlPreguntas.WrapContents = false;
            //
            // btnRecuperar
            //
            this.btnRecuperar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperar.Location = new System.Drawing.Point(282, 317);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.Size = new System.Drawing.Size(190, 35);
            this.btnRecuperar.TabIndex = 2;
            this.btnRecuperar.Text = "Recuperar Contraseña";
            this.btnRecuperar.UseVisualStyleBackColor = true;
            this.btnRecuperar.Visible = false;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(188, 21);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Recuperar Contraseña";
            //
            // RecuperarContrasenaForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnRecuperar);
            this.Controls.Add(this.grpPreguntas);
            this.Controls.Add(this.grpUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecuperarContrasenaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recuperar Contraseña";
            this.grpUsuario.ResumeLayout(false);
            this.grpUsuario.PerformLayout();
            this.grpPreguntas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
