partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.DataGridView dgvUsers;
    private System.Windows.Forms.Button btnCreateUser;
    private System.Windows.Forms.Button btnDeleteUser;

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
        this.dgvUsers = new System.Windows.Forms.DataGridView();
        this.btnCreateUser = new System.Windows.Forms.Button();
        this.btnDeleteUser = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
        this.SuspendLayout();

        // dgvUsers
        this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvUsers.Location = new System.Drawing.Point(12, 12);
        this.dgvUsers.Name = "dgvUsers";
        this.dgvUsers.Size = new System.Drawing.Size(460, 300);
        this.dgvUsers.TabIndex = 0;

        // btnCreateUser
        this.btnCreateUser.Location = new System.Drawing.Point(12, 330);
        this.btnCreateUser.Name = "btnCreateUser";
        this.btnCreateUser.Size = new System.Drawing.Size(100, 30);
        this.btnCreateUser.Text = "Create User";
        this.btnCreateUser.UseVisualStyleBackColor = true;
        this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);

        // btnDeleteUser
        this.btnDeleteUser.Location = new System.Drawing.Point(130, 330);
        this.btnDeleteUser.Name = "btnDeleteUser";
        this.btnDeleteUser.Size = new System.Drawing.Size(100, 30);
        this.btnDeleteUser.Text = "Delete User";
        this.btnDeleteUser.UseVisualStyleBackColor = true;
        this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

        // MainForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(484, 381);
        this.Controls.Add(this.dgvUsers);
        this.Controls.Add(this.btnCreateUser);
        this.Controls.Add(this.btnDeleteUser);
        this.Name = "MainForm";
        this.Text = "User Management";
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
        this.ResumeLayout(false);
    }
}
