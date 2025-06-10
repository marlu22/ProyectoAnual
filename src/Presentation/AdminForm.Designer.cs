namespace Presentation
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPersonas;
        private System.Windows.Forms.TabPage tabUsuarios;

        // Controles para "Añadir Persona"
        private System.Windows.Forms.TableLayoutPanel personaLayout;
        private System.Windows.Forms.TextBox txtLegajo, txtNombre, txtApellido, txtNumDoc, txtCuil, txtCalle, txtAltura, txtCorreo;
        private System.Windows.Forms.ComboBox cbxTipoDoc, cbxLocalidad, cbxGenero;
        private System.Windows.Forms.Button btnGuardarPersona;

        // Controles para "Crear Usuario"
        private System.Windows.Forms.TableLayoutPanel usuarioLayout;
        private System.Windows.Forms.ComboBox cbxPersona, cbxRolUsuario;
        private System.Windows.Forms.TextBox txtUsuario, txtPassword;
        private System.Windows.Forms.Button btnCrearUsuario, btnRecuperarContrasena;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new System.Windows.Forms.TabControl();
            tabPersonas = new System.Windows.Forms.TabPage();
            tabUsuarios = new System.Windows.Forms.TabPage();

            // ----------- TAB PERSONAS -----------
            personaLayout = new System.Windows.Forms.TableLayoutPanel();
            txtLegajo = new System.Windows.Forms.TextBox();
            txtNombre = new System.Windows.Forms.TextBox();
            txtApellido = new System.Windows.Forms.TextBox();
            cbxTipoDoc = new System.Windows.Forms.ComboBox();
            txtNumDoc = new System.Windows.Forms.TextBox();
            txtCuil = new System.Windows.Forms.TextBox();
            txtCalle = new System.Windows.Forms.TextBox();
            txtAltura = new System.Windows.Forms.TextBox();
            cbxLocalidad = new System.Windows.Forms.ComboBox();
            cbxGenero = new System.Windows.Forms.ComboBox();
            txtCorreo = new System.Windows.Forms.TextBox();
            btnGuardarPersona = new System.Windows.Forms.Button();

            personaLayout.ColumnCount = 2;
            personaLayout.RowCount = 11;
            personaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            personaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            personaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            for (int i = 0; i < 11; i++)
                personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));

            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Legajo:" }, 0, 0);
            personaLayout.Controls.Add(txtLegajo, 1, 0);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Nombre:" }, 0, 1);
            personaLayout.Controls.Add(txtNombre, 1, 1);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Apellido:" }, 0, 2);
            personaLayout.Controls.Add(txtApellido, 1, 2);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Tipo Doc:" }, 0, 3);
            personaLayout.Controls.Add(cbxTipoDoc, 1, 3);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "N° Doc:" }, 0, 4);
            personaLayout.Controls.Add(txtNumDoc, 1, 4);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "CUIL:" }, 0, 5);
            personaLayout.Controls.Add(txtCuil, 1, 5);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Calle:" }, 0, 6);
            personaLayout.Controls.Add(txtCalle, 1, 6);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Altura:" }, 0, 7);
            personaLayout.Controls.Add(txtAltura, 1, 7);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Localidad:" }, 0, 8);
            personaLayout.Controls.Add(cbxLocalidad, 1, 8);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Género:" }, 0, 9);
            personaLayout.Controls.Add(cbxGenero, 1, 9);
            personaLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Correo:" }, 0, 10);
            personaLayout.Controls.Add(txtCorreo, 1, 10);

            btnGuardarPersona.Text = "Guardar Persona";
            personaLayout.Controls.Add(btnGuardarPersona, 1, 11);

            tabPersonas.Text = "Añadir Persona";
            tabPersonas.Controls.Add(personaLayout);

            // ----------- TAB USUARIOS -----------
            usuarioLayout = new System.Windows.Forms.TableLayoutPanel();
            cbxPersona = new System.Windows.Forms.ComboBox();
            txtUsuario = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            cbxRolUsuario = new System.Windows.Forms.ComboBox();
            btnCrearUsuario = new System.Windows.Forms.Button();
            btnRecuperarContrasena = new System.Windows.Forms.Button();

            usuarioLayout.ColumnCount = 2;
            usuarioLayout.RowCount = 6;
            usuarioLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            for (int i = 0; i < 6; i++)
                usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));

            usuarioLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Persona:" }, 0, 0);
            usuarioLayout.Controls.Add(cbxPersona, 1, 0);
            usuarioLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Usuario:" }, 0, 1);
            usuarioLayout.Controls.Add(txtUsuario, 1, 1);
            usuarioLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Contraseña:" }, 0, 2);
            usuarioLayout.Controls.Add(txtPassword, 1, 2);
            usuarioLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Rol:" }, 0, 3);
            usuarioLayout.Controls.Add(cbxRolUsuario, 1, 3);

            btnCrearUsuario.Text = "Crear Usuario";
            usuarioLayout.Controls.Add(btnCrearUsuario, 1, 4);

            btnRecuperarContrasena.Text = "¿Olvidaste tu contraseña?";
            btnRecuperarContrasena.Dock = System.Windows.Forms.DockStyle.Right;
            btnRecuperarContrasena.Click += BtnRecuperarContrasena_Click;
            usuarioLayout.Controls.Add(btnRecuperarContrasena, 1, 5);

            tabUsuarios.Text = "Crear Usuario";
            tabUsuarios.Controls.Add(usuarioLayout);

            // ----------- TAB CONTROL PRINCIPAL -----------
            tabControl.Controls.Add(tabPersonas);
            tabControl.Controls.Add(tabUsuarios);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;

            // ----------- FORM PRINCIPAL -----------
            ClientSize = new System.Drawing.Size(500, 400);
            Controls.Add(tabControl);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Panel de Administración";

            tabControl.ResumeLayout(false);
            tabPersonas.ResumeLayout(false);
            personaLayout.ResumeLayout(false);
            personaLayout.PerformLayout();
            tabUsuarios.ResumeLayout(false);
            usuarioLayout.ResumeLayout(false);
            usuarioLayout.PerformLayout();
            ResumeLayout(false);
        }
    }
}