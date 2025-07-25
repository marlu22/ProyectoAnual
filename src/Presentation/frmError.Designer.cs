namespace Presentation
{
    partial class frmError
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblExceptionType = new System.Windows.Forms.Label();
            this.txtExceptionType = new System.Windows.Forms.TextBox();
            this.lblStackTrace = new System.Windows.Forms.Label();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // lblMessage
            //
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(55, 13);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message:";
            //
            // txtMessage
            //
            this.txtMessage.Location = new System.Drawing.Point(110, 12);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(362, 60);
            this.txtMessage.TabIndex = 1;
            //
            // lblExceptionType
            //
            this.lblExceptionType.AutoSize = true;
            this.lblExceptionType.Location = new System.Drawing.Point(12, 81);
            this.lblExceptionType.Name = "lblExceptionType";
            this.lblExceptionType.Size = new System.Drawing.Size(86, 13);
            this.lblExceptionType.TabIndex = 2;
            this.lblExceptionType.Text = "Exception Type:";
            //
            // txtExceptionType
            //
            this.txtExceptionType.Location = new System.Drawing.Point(110, 78);
            this.txtExceptionType.Name = "txtExceptionType";
            this.txtExceptionType.ReadOnly = true;
            this.txtExceptionType.Size = new System.Drawing.Size(362, 20);
            this.txtExceptionType.TabIndex = 3;
            //
            // lblStackTrace
            //
            this.lblStackTrace.AutoSize = true;
            this.lblStackTrace.Location = new System.Drawing.Point(12, 107);
            this.lblStackTrace.Name = "lblStackTrace";
            this.lblStackTrace.Size = new System.Drawing.Size(69, 13);
            this.lblStackTrace.TabIndex = 4;
            this.lblStackTrace.Text = "Stack Trace:";
            //
            // txtStackTrace
            //
            this.txtStackTrace.Location = new System.Drawing.Point(15, 123);
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStackTrace.Size = new System.Drawing.Size(457, 126);
            this.txtStackTrace.TabIndex = 5;
            //
            // frmError
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.txtStackTrace);
            this.Controls.Add(this.lblStackTrace);
            this.Controls.Add(this.txtExceptionType);
            this.Controls.Add(this.lblExceptionType);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Name = "frmError";
            this.Text = "Error Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblExceptionType;
        private System.Windows.Forms.TextBox txtExceptionType;
        private System.Windows.Forms.Label lblStackTrace;
        private System.Windows.Forms.TextBox txtStackTrace;
    }
}
