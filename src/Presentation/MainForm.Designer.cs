partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.DataGridView dgvUsers;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
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
        this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.dgvUsers = new System.Windows.Forms.DataGridView();
        this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
        this.btnCreateUser = new System.Windows.Forms.Button();
        this.btnDeleteUser = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
        this.tableLayoutPanel.SuspendLayout();
        this.buttonPanel.SuspendLayout();
        this.SuspendLayout();

        // 
        // tableLayoutPanel
        // 
        this.tableLayoutPanel.ColumnCount = 1;
        this.tableLayoutPanel.RowCount = 2;
        this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
        this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
        this.tableLayoutPanel.Controls.Add(this.dgvUsers, 0, 0);
        this.tableLayoutPanel.Controls.Add(this.buttonPanel, 0, 1);

        // 
        // dgvUsers
        // 
        this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvUsers.MultiSelect = false;

        // 
        // buttonPanel
        // 
        this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.buttonPanel.Controls.Add(this.btnCreateUser);
        this.buttonPanel.Controls.Add(this.btnDeleteUser);

        // 
        // btnCreateUser
        // 
        this.btnCreateUser.Text = "Crear Usuario";
        this.btnCreateUser.Width = 120;

        // 
        // btnDeleteUser
        // 
        this.btnDeleteUser.Text = "Eliminar Usuario";
        this.btnDeleteUser.Width = 120;

        // 
        // MainForm
        // 
        this.ClientSize = new System.Drawing.Size(600, 400);
        this.Controls.Add(this.tableLayoutPanel);
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Gestión de Usuarios";
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
        this.tableLayoutPanel.ResumeLayout(false);
        this.buttonPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }
}
