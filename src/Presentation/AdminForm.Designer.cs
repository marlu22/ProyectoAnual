namespace Presentation
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPersonas;
        private System.Windows.Forms.TabPage tabUsuarios;
        private System.Windows.Forms.TabPage tabGestionUsuarios;

        // Controles para "Añadir Persona"
        private System.Windows.Forms.TableLayoutPanel personaLayout;
        private System.Windows.Forms.TextBox txtLegajo, txtNombre, txtApellido, txtNumDoc, txtCuil, txtCalle, txtAltura, txtCorreo;
        private System.Windows.Forms.ComboBox cbxTipoDoc, cbxLocalidad, cbxGenero;
        private System.Windows.Forms.Button btnGuardarPersona;

        // Controles para "Crear Usuario"
        private System.Windows.Forms.TableLayoutPanel usuarioLayout;
        private System.Windows.Forms.ComboBox cbxPersona, cbxRolUsuario;
        private System.Windows.Forms.TextBox txtUsuario, txtPassword;
        private System.Windows.Forms.Button btnCrearUsuario;

        // Controles para "Configuracion"
        private System.Windows.Forms.TabPage tabConfiguracion;
        private System.Windows.Forms.TableLayoutPanel configuracionLayout;
        private System.Windows.Forms.CheckBox chkMayusculasMinusculas;
        private System.Windows.Forms.CheckBox chkNumeros;
        private System.Windows.Forms.CheckBox chkCaracteresEspeciales;
        private System.Windows.Forms.CheckBox chkDobleFactor;
        private System.Windows.Forms.CheckBox chkNoRepetirContrasenas;
        private System.Windows.Forms.CheckBox chkVerificarDatosPersonales;
        private System.Windows.Forms.TextBox txtMinCaracteres;
        private System.Windows.Forms.TextBox txtCantPreguntas;
        private System.Windows.Forms.Button btnGuardarConfig;

        // Controles para "Gestion de Usuarios"
        private System.Windows.Forms.TableLayoutPanel gestionUsuariosLayout;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnRefrescarUsuarios, btnGuardarCambios, btnEliminarUsuario;

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
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
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

            usuarioLayout.ColumnCount = 2;
            usuarioLayout.RowCount = 6;
            usuarioLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
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

            tabUsuarios.Text = "Crear Usuario";
            tabUsuarios.Controls.Add(usuarioLayout);

            // ----------- TAB GESTION DE USUARIOS -----------
            tabGestionUsuarios = new System.Windows.Forms.TabPage();
            gestionUsuariosLayout = new System.Windows.Forms.TableLayoutPanel();
            dgvUsuarios = new System.Windows.Forms.DataGridView();
            btnRefrescarUsuarios = new System.Windows.Forms.Button();
            btnGuardarCambios = new System.Windows.Forms.Button();
            btnEliminarUsuario = new System.Windows.Forms.Button();

            gestionUsuariosLayout.ColumnCount = 3;
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.RowCount = 2;
            gestionUsuariosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            gestionUsuariosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            gestionUsuariosLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            gestionUsuariosLayout.Controls.Add(dgvUsuarios, 0, 0);
            gestionUsuariosLayout.SetColumnSpan(dgvUsuarios, 3);

            btnRefrescarUsuarios.Text = "Refrescar";
            btnGuardarCambios.Text = "Guardar Cambios";
            btnEliminarUsuario.Text = "Eliminar Usuario";

            var buttonPanel = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
            };
            buttonPanel.Controls.Add(btnEliminarUsuario);
            buttonPanel.Controls.Add(btnGuardarCambios);
            buttonPanel.Controls.Add(btnRefrescarUsuarios);

            gestionUsuariosLayout.Controls.Add(buttonPanel, 0, 1);
            gestionUsuariosLayout.SetColumnSpan(buttonPanel, 3);

            tabGestionUsuarios.Text = "Gestion de Usuarios";
            tabGestionUsuarios.Controls.Add(gestionUsuariosLayout);

            // ----------- TAB CONFIGURACION -----------
            tabConfiguracion = new System.Windows.Forms.TabPage();
            configuracionLayout = new System.Windows.Forms.TableLayoutPanel();
            chkMayusculasMinusculas = new System.Windows.Forms.CheckBox();
            chkNumeros = new System.Windows.Forms.CheckBox();
            chkCaracteresEspeciales = new System.Windows.Forms.CheckBox();
            chkDobleFactor = new System.Windows.Forms.CheckBox();
            chkNoRepetirContrasenas = new System.Windows.Forms.CheckBox();
            chkVerificarDatosPersonales = new System.Windows.Forms.CheckBox();
            txtMinCaracteres = new System.Windows.Forms.TextBox();
            txtCantPreguntas = new System.Windows.Forms.TextBox();
            btnGuardarConfig = new System.Windows.Forms.Button();

            configuracionLayout.ColumnCount = 2;
            configuracionLayout.RowCount = 9;
            configuracionLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            configuracionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            configuracionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));

            chkMayusculasMinusculas.Text = "Combinar mayúsculas y minúsculas";
            chkNumeros.Text = "Requerir números";
            chkCaracteresEspeciales.Text = "Requerir caracteres especiales";
            chkDobleFactor.Text = "Usar doble factor";
            chkNoRepetirContrasenas.Text = "No repetir contraseñas anteriores";
            chkVerificarDatosPersonales.Text = "Verificar datos personales";
            btnGuardarConfig.Text = "Guardar";

            configuracionLayout.Controls.Add(chkMayusculasMinusculas, 0, 0);
            configuracionLayout.Controls.Add(chkNumeros, 0, 1);
            configuracionLayout.Controls.Add(chkCaracteresEspeciales, 0, 2);
            configuracionLayout.Controls.Add(chkDobleFactor, 0, 3);
            configuracionLayout.Controls.Add(chkNoRepetirContrasenas, 0, 4);
            configuracionLayout.Controls.Add(chkVerificarDatosPersonales, 0, 5);
            configuracionLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Mínimo de caracteres:" }, 0, 6);
            configuracionLayout.Controls.Add(txtMinCaracteres, 1, 6);
            configuracionLayout.Controls.Add(new System.Windows.Forms.Label() { Text = "Cantidad de preguntas:" }, 0, 7);
            configuracionLayout.Controls.Add(txtCantPreguntas, 1, 7);
            configuracionLayout.Controls.Add(btnGuardarConfig, 1, 8);

            tabConfiguracion.Text = "Configuración";
            tabConfiguracion.Controls.Add(configuracionLayout);

            // ----------- TAB CONTROL PRINCIPAL -----------
            tabControl.Controls.Add(tabPersonas);
            tabControl.Controls.Add(tabUsuarios);
            tabControl.Controls.Add(tabGestionUsuarios);
            tabControl.Controls.Add(tabConfiguracion);
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
            tabGestionUsuarios.ResumeLayout(false);
            gestionUsuariosLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvUsuarios)).EndInit();
            tabConfiguracion.ResumeLayout(false);
            configuracionLayout.ResumeLayout(false);
            configuracionLayout.PerformLayout();
            ResumeLayout(false);
        }
    }
}