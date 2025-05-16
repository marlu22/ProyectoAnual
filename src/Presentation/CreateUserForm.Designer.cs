partial class CreateUserForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
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
        tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        lblUsername = new System.Windows.Forms.Label();
        txtUsername = new System.Windows.Forms.TextBox();
        lblPassword = new System.Windows.Forms.Label();
        txtPassword = new System.Windows.Forms.TextBox();
        lblEmail = new System.Windows.Forms.Label();
        txtEmail = new System.Windows.Forms.TextBox();
        buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
        btnSave = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        tableLayoutPanel.SuspendLayout();
        buttonPanel.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel
        // 
        tableLayoutPanel.ColumnCount = 2;
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
        tableLayoutPanel.Controls.Add(lblUsername, 0, 0);
        tableLayoutPanel.Controls.Add(txtUsername, 1, 0);
        tableLayoutPanel.Controls.Add(lblPassword, 0, 1);
        tableLayoutPanel.Controls.Add(txtPassword, 1, 1);
        tableLayoutPanel.Controls.Add(lblEmail, 0, 2);
        tableLayoutPanel.Controls.Add(txtEmail, 1, 2);
        tableLayoutPanel.Controls.Add(buttonPanel, 1, 3);
        tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel.Name = "tableLayoutPanel";
        tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
        tableLayoutPanel.RowCount = 4;
        tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
        tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
        tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
        tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
        tableLayoutPanel.Size = new System.Drawing.Size(400, 180);
        tableLayoutPanel.TabIndex = 0;
        // 
        // lblUsername
        // 
        lblUsername.Dock = System.Windows.Forms.DockStyle.Fill;
        lblUsername.Location = new System.Drawing.Point(13, 10);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new System.Drawing.Size(127, 35);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "Usuario:";
        lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // txtUsername
        // 
        txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
        txtUsername.Location = new System.Drawing.Point(146, 13);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new System.Drawing.Size(241, 25);
        txtUsername.TabIndex = 1;
        // 
        // lblPassword
        // 
        lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
        lblPassword.Location = new System.Drawing.Point(13, 45);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new System.Drawing.Size(127, 35);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Contraseña:";
        lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // txtPassword
        // 
        txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
        txtPassword.Location = new System.Drawing.Point(146, 48);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '●';
        txtPassword.Size = new System.Drawing.Size(241, 25);
        txtPassword.TabIndex = 3;
        // 
        // lblEmail
        // 
        lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
        lblEmail.Location = new System.Drawing.Point(13, 80);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new System.Drawing.Size(127, 35);
        lblEmail.TabIndex = 4;
        lblEmail.Text = "Email:";
        lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // txtEmail
        // 
        txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
        txtEmail.Location = new System.Drawing.Point(146, 83);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new System.Drawing.Size(241, 25);
        txtEmail.TabIndex = 5;
        // 
        // buttonPanel
        // 
        buttonPanel.Controls.Add(btnSave);
        buttonPanel.Controls.Add(btnCancel);
        buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        buttonPanel.Location = new System.Drawing.Point(146, 118);
        buttonPanel.Name = "buttonPanel";
        buttonPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
        buttonPanel.Size = new System.Drawing.Size(241, 49);
        buttonPanel.TabIndex = 6;
        // 
        // btnSave
        // 
        btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
        btnSave.Location = new System.Drawing.Point(148, 8);
        btnSave.Name = "btnSave";
        btnSave.Size = new System.Drawing.Size(90, 23);
        btnSave.TabIndex = 0;
        btnSave.Text = "Guardar";
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        btnCancel.Location = new System.Drawing.Point(52, 8);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(90, 23);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Cancelar";
        // 
        // CreateUserForm
        // 
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new System.Drawing.Size(400, 180);
        Controls.Add(tableLayoutPanel);
        Font = new System.Drawing.Font("Segoe UI", 10F);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CreateUserForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Alta de Usuario";
        tableLayoutPanel.ResumeLayout(false);
        tableLayoutPanel.PerformLayout();
        buttonPanel.ResumeLayout(false);
        ResumeLayout(false);
    }
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
}
