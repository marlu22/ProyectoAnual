namespace Presentation
{
    partial class AbForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAlta;
        private System.Windows.Forms.TabPage tabBaja;
        private System.Windows.Forms.TableLayoutPanel altaLayout;
        private System.Windows.Forms.Label lblNombre, lblApellido, lblDni, lblEmail, lblRol;
        private System.Windows.Forms.TextBox txtNombre, txtApellido, txtDni, txtEmail;
        private System.Windows.Forms.ComboBox cbxRol;
        private System.Windows.Forms.Button btnGuardar, btnLimpiar;

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
            tabControl = new System.Windows.Forms.TabControl();
            tabAlta = new System.Windows.Forms.TabPage();
            altaLayout = new System.Windows.Forms.TableLayoutPanel();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblDni = new System.Windows.Forms.Label();
            txtDni = new System.Windows.Forms.TextBox();
            lblEmail = new System.Windows.Forms.Label();
            txtEmail = new System.Windows.Forms.TextBox();
            lblRol = new System.Windows.Forms.Label();
            cbxRol = new System.Windows.Forms.ComboBox();
            altaButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnGuardar = new System.Windows.Forms.Button();
            btnLimpiar = new System.Windows.Forms.Button();
            tabBaja = new System.Windows.Forms.TabPage();
            bajaLayout = new System.Windows.Forms.TableLayoutPanel();
            lblDniBaja = new System.Windows.Forms.Label();
            txtDniBaja = new System.Windows.Forms.TextBox();
            bajaButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnEliminar = new System.Windows.Forms.Button();
            btnBuscar = new System.Windows.Forms.Button();
            tabControl.SuspendLayout();
            tabAlta.SuspendLayout();
            altaLayout.SuspendLayout();
            altaButtonPanel.SuspendLayout();
            tabBaja.SuspendLayout();
            bajaLayout.SuspendLayout();
            bajaButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabAlta);
            tabControl.Controls.Add(tabBaja);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(400, 300);
            tabControl.TabIndex = 0;
            // 
            // tabAlta
            // 
            tabAlta.Controls.Add(altaLayout);
            tabAlta.Location = new System.Drawing.Point(4, 26);
            tabAlta.Name = "tabAlta";
            tabAlta.Size = new System.Drawing.Size(392, 270);
            tabAlta.TabIndex = 0;
            tabAlta.Text = "Alta";
            // 
            // altaLayout
            // 
            altaLayout.ColumnCount = 2;
            altaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            altaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            altaLayout.Controls.Add(lblNombre, 0, 0);
            altaLayout.Controls.Add(txtNombre, 1, 0);
            altaLayout.Controls.Add(lblApellido, 0, 1);
            altaLayout.Controls.Add(txtApellido, 1, 1);
            altaLayout.Controls.Add(lblDni, 0, 2);
            altaLayout.Controls.Add(txtDni, 1, 2);
            altaLayout.Controls.Add(lblEmail, 0, 3);
            altaLayout.Controls.Add(txtEmail, 1, 3);
            altaLayout.Controls.Add(lblRol, 0, 4);
            altaLayout.Controls.Add(cbxRol, 1, 4);
            altaLayout.Controls.Add(altaButtonPanel, 1, 5);
            altaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            altaLayout.Location = new System.Drawing.Point(0, 0);
            altaLayout.Name = "altaLayout";
            altaLayout.Padding = new System.Windows.Forms.Padding(10);
            altaLayout.RowCount = 6;
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            altaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            altaLayout.Size = new System.Drawing.Size(392, 270);
            altaLayout.TabIndex = 0;
            // 
            // lblNombre
            // 
            lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            lblNombre.Location = new System.Drawing.Point(13, 10);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(124, 35);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            txtNombre.Location = new System.Drawing.Point(143, 13);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(236, 25);
            txtNombre.TabIndex = 1;
            // 
            // lblApellido
            // 
            lblApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            lblApellido.Location = new System.Drawing.Point(13, 45);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(124, 35);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            txtApellido.Location = new System.Drawing.Point(143, 48);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(236, 25);
            txtApellido.TabIndex = 3;
            // 
            // lblDni
            // 
            lblDni.Dock = System.Windows.Forms.DockStyle.Fill;
            lblDni.Location = new System.Drawing.Point(13, 80);
            lblDni.Name = "lblDni";
            lblDni.Size = new System.Drawing.Size(124, 35);
            lblDni.TabIndex = 4;
            lblDni.Text = "DNI:";
            // 
            // txtDni
            // 
            txtDni.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDni.Location = new System.Drawing.Point(143, 83);
            txtDni.Name = "txtDni";
            txtDni.Size = new System.Drawing.Size(236, 25);
            txtDni.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            lblEmail.Location = new System.Drawing.Point(13, 115);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(124, 35);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            txtEmail.Location = new System.Drawing.Point(143, 118);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(236, 25);
            txtEmail.TabIndex = 7;
            // 
            // lblRol
            // 
            lblRol.Dock = System.Windows.Forms.DockStyle.Fill;
            lblRol.Location = new System.Drawing.Point(13, 150);
            lblRol.Name = "lblRol";
            lblRol.Size = new System.Drawing.Size(124, 35);
            lblRol.TabIndex = 8;
            lblRol.Text = "Rol:";
            // 
            // cbxRol
            // 
            cbxRol.Dock = System.Windows.Forms.DockStyle.Fill;
            cbxRol.Items.AddRange(new object[] { "Usuario", "Administrador" });
            cbxRol.Location = new System.Drawing.Point(143, 153);
            cbxRol.Name = "cbxRol";
            cbxRol.Size = new System.Drawing.Size(236, 25);
            cbxRol.TabIndex = 9;
            // 
            // altaButtonPanel
            // 
            altaButtonPanel.Controls.Add(btnGuardar);
            altaButtonPanel.Controls.Add(btnLimpiar);
            altaButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            altaButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            altaButtonPanel.Location = new System.Drawing.Point(143, 188);
            altaButtonPanel.Name = "altaButtonPanel";
            altaButtonPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            altaButtonPanel.Size = new System.Drawing.Size(236, 69);
            altaButtonPanel.TabIndex = 10;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new System.Drawing.Point(143, 8);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(90, 23);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new System.Drawing.Point(47, 8);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(90, 23);
            btnLimpiar.TabIndex = 1;
            btnLimpiar.Text = "Limpiar";
            // 
            // tabBaja
            // 
            tabBaja.Controls.Add(bajaLayout);
            tabBaja.Location = new System.Drawing.Point(4, 24);
            tabBaja.Name = "tabBaja";
            tabBaja.Size = new System.Drawing.Size(392, 272);
            tabBaja.TabIndex = 1;
            tabBaja.Text = "Baja";
            // 
            // bajaLayout
            // 
            bajaLayout.ColumnCount = 2;
            bajaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            bajaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            bajaLayout.Controls.Add(lblDniBaja, 0, 0);
            bajaLayout.Controls.Add(txtDniBaja, 1, 0);
            bajaLayout.Controls.Add(bajaButtonPanel, 1, 2);
            bajaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            bajaLayout.Location = new System.Drawing.Point(0, 0);
            bajaLayout.Name = "bajaLayout";
            bajaLayout.Padding = new System.Windows.Forms.Padding(10);
            bajaLayout.RowCount = 3;
            bajaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            bajaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            bajaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            bajaLayout.Size = new System.Drawing.Size(392, 272);
            bajaLayout.TabIndex = 0;
            // 
            // lblDniBaja
            // 
            lblDniBaja.Dock = System.Windows.Forms.DockStyle.Fill;
            lblDniBaja.Location = new System.Drawing.Point(13, 10);
            lblDniBaja.Name = "lblDniBaja";
            lblDniBaja.Size = new System.Drawing.Size(124, 35);
            lblDniBaja.TabIndex = 0;
            lblDniBaja.Text = "DNI:";
            // 
            // txtDniBaja
            // 
            txtDniBaja.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDniBaja.Location = new System.Drawing.Point(143, 13);
            txtDniBaja.Name = "txtDniBaja";
            txtDniBaja.Size = new System.Drawing.Size(236, 25);
            txtDniBaja.TabIndex = 1;
            // 
            // bajaButtonPanel
            // 
            bajaButtonPanel.Controls.Add(btnEliminar);
            bajaButtonPanel.Controls.Add(btnBuscar);
            bajaButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            bajaButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            bajaButtonPanel.Location = new System.Drawing.Point(143, 83);
            bajaButtonPanel.Name = "bajaButtonPanel";
            bajaButtonPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            bajaButtonPanel.Size = new System.Drawing.Size(236, 176);
            bajaButtonPanel.TabIndex = 2;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new System.Drawing.Point(143, 8);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(90, 23);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new System.Drawing.Point(47, 8);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new System.Drawing.Size(90, 23);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            // 
            // AbForm
            // 
            ClientSize = new System.Drawing.Size(400, 300);
            Controls.Add(tabControl);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AbForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Alta y Baja de Usuario";
            tabControl.ResumeLayout(false);
            tabAlta.ResumeLayout(false);
            altaLayout.ResumeLayout(false);
            altaLayout.PerformLayout();
            altaButtonPanel.ResumeLayout(false);
            tabBaja.ResumeLayout(false);
            bajaLayout.ResumeLayout(false);
            bajaLayout.PerformLayout();
            bajaButtonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        private System.Windows.Forms.FlowLayoutPanel altaButtonPanel;
        private System.Windows.Forms.TableLayoutPanel bajaLayout;
        private System.Windows.Forms.Label lblDniBaja;
        private System.Windows.Forms.TextBox txtDniBaja;
        private System.Windows.Forms.FlowLayoutPanel bajaButtonPanel;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnBuscar;
    }
}