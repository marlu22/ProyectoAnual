partial class CreateUserForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblEmail;

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
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.lblUsername = new System.Windows.Forms.Label();
        this.lblPassword = new System.Windows.Forms.Label();
        this.lblEmail = new System.Windows.Forms.Label();
        this.SuspendLayout();

        // lblUsername
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(12, 15);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(58, 15);
        this.lblUsername.Text = "Username";

        // txtUsername
        this.txtUsername.Location = new System.Drawing.Point(100, 12);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(200, 23);

        // lblPassword
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(12, 50);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(57, 15);
        this.lblPassword.Text = "Password";

        // txtPassword
        this.txtPassword.Location = new System.Drawing.Point(100, 47);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.Size = new System.Drawing.Size(200, 23);
        this.txtPassword.UseSystemPasswordChar = true;

        // lblEmail
        this.lblEmail.AutoSize = true;
        this.lblEmail.Location = new System.Drawing.Point(12, 85);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Size = new System.Drawing.Size(36, 15);
        this.lblEmail.Text = "Email";

        // txtEmail
        this.txtEmail.Location = new System.Drawing.Point(100, 82);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new System.Drawing.Size(200, 23);

        // btnSave
        this.btnSave.Location = new System.Drawing.Point(100, 120);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(75, 23);
        this.btnSave.Text = "Save";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

        // CreateUserForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(320, 160);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.txtUsername);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.btnSave);
        this.Name = "CreateUserForm";
        this.Text = "Create User";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
