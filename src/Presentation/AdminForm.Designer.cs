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
            tabUsuarios = new System.Windows.Forms.TabPage();
            usuarioLayout = new System.Windows.Forms.TableLayoutPanel();
            cbxPersona = new System.Windows.Forms.ComboBox();
            txtUsuario = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            cbxRolUsuario = new System.Windows.Forms.ComboBox();
            btnCrearUsuario = new System.Windows.Forms.Button();
            tabGestionUsuarios = new System.Windows.Forms.TabPage();
            gestionUsuariosLayout = new System.Windows.Forms.TableLayoutPanel();
            dgvUsuarios = new System.Windows.Forms.DataGridView();
            buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnEliminarUsuario = new System.Windows.Forms.Button();
            btnGuardarCambios = new System.Windows.Forms.Button();
            btnRefrescarUsuarios = new System.Windows.Forms.Button();
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
            tabControl.SuspendLayout();
            tabPersonas.SuspendLayout();
            personaLayout.SuspendLayout();
            tabUsuarios.SuspendLayout();
            usuarioLayout.SuspendLayout();
            tabGestionUsuarios.SuspendLayout();
            gestionUsuariosLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            buttonPanel.SuspendLayout();
            tabConfiguracion.SuspendLayout();
            configuracionLayout.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPersonas);
            tabControl.Controls.Add(tabUsuarios);
            tabControl.Controls.Add(tabGestionUsuarios);
            tabControl.Controls.Add(tabConfiguracion);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(500, 400);
            tabControl.TabIndex = 0;
            // 
            // tabPersonas
            // 
            tabPersonas.Controls.Add(personaLayout);
            tabPersonas.Location = new System.Drawing.Point(4, 26);
            tabPersonas.Name = "tabPersonas";
            tabPersonas.Size = new System.Drawing.Size(492, 370);
            tabPersonas.TabIndex = 0;
            tabPersonas.Text = "Añadir Persona";
            // 
            // personaLayout
            // 
            personaLayout.ColumnCount = 2;
            personaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            personaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            personaLayout.Controls.Add(txtLegajo, 1, 0);
            personaLayout.Controls.Add(txtNombre, 1, 1);
            personaLayout.Controls.Add(txtApellido, 1, 2);
            personaLayout.Controls.Add(cbxTipoDoc, 1, 3);
            personaLayout.Controls.Add(txtNumDoc, 1, 4);
            personaLayout.Controls.Add(txtCuil, 1, 5);
            personaLayout.Controls.Add(txtCalle, 1, 6);
            personaLayout.Controls.Add(txtAltura, 1, 7);
            personaLayout.Controls.Add(cbxLocalidad, 1, 8);
            personaLayout.Controls.Add(cbxGenero, 1, 9);
            personaLayout.Controls.Add(txtCorreo, 1, 10);
            personaLayout.Controls.Add(btnGuardarPersona, 1, 11);
            personaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            personaLayout.Location = new System.Drawing.Point(0, 0);
            personaLayout.Name = "personaLayout";
            personaLayout.RowCount = 11;
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
            personaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            personaLayout.Size = new System.Drawing.Size(492, 370);
            personaLayout.TabIndex = 0;
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new System.Drawing.Point(175, 3);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new System.Drawing.Size(100, 25);
            txtLegajo.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(175, 33);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(100, 25);
            txtNombre.TabIndex = 3;
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(175, 63);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(100, 25);
            txtApellido.TabIndex = 5;
            // 
            // cbxTipoDoc
            // 
            cbxTipoDoc.Location = new System.Drawing.Point(175, 93);
            cbxTipoDoc.Name = "cbxTipoDoc";
            cbxTipoDoc.Size = new System.Drawing.Size(121, 25);
            cbxTipoDoc.TabIndex = 7;
            // 
            // txtNumDoc
            // 
            txtNumDoc.Location = new System.Drawing.Point(175, 123);
            txtNumDoc.Name = "txtNumDoc";
            txtNumDoc.Size = new System.Drawing.Size(100, 25);
            txtNumDoc.TabIndex = 9;
            // 
            // txtCuil
            // 
            txtCuil.Location = new System.Drawing.Point(175, 153);
            txtCuil.Name = "txtCuil";
            txtCuil.Size = new System.Drawing.Size(100, 25);
            txtCuil.TabIndex = 11;
            // 
            // txtCalle
            // 
            txtCalle.Location = new System.Drawing.Point(175, 183);
            txtCalle.Name = "txtCalle";
            txtCalle.Size = new System.Drawing.Size(100, 25);
            txtCalle.TabIndex = 13;
            // 
            // txtAltura
            // 
            txtAltura.Location = new System.Drawing.Point(175, 213);
            txtAltura.Name = "txtAltura";
            txtAltura.Size = new System.Drawing.Size(100, 25);
            txtAltura.TabIndex = 15;
            // 
            // cbxLocalidad
            // 
            cbxLocalidad.Location = new System.Drawing.Point(175, 243);
            cbxLocalidad.Name = "cbxLocalidad";
            cbxLocalidad.Size = new System.Drawing.Size(121, 25);
            cbxLocalidad.TabIndex = 17;
            // 
            // cbxGenero
            // 
            cbxGenero.Location = new System.Drawing.Point(175, 273);
            cbxGenero.Name = "cbxGenero";
            cbxGenero.Size = new System.Drawing.Size(121, 25);
            cbxGenero.TabIndex = 19;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new System.Drawing.Point(175, 303);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new System.Drawing.Size(100, 25);
            txtCorreo.TabIndex = 21;
            // 
            // btnGuardarPersona
            // 
            btnGuardarPersona.Location = new System.Drawing.Point(175, 333);
            btnGuardarPersona.Name = "btnGuardarPersona";
            btnGuardarPersona.Size = new System.Drawing.Size(75, 23);
            btnGuardarPersona.TabIndex = 22;
            btnGuardarPersona.Text = "Guardar Persona";
            // 
            // tabUsuarios
            // 
            tabUsuarios.Controls.Add(usuarioLayout);
            tabUsuarios.Location = new System.Drawing.Point(4, 24);
            tabUsuarios.Name = "tabUsuarios";
            tabUsuarios.Size = new System.Drawing.Size(492, 372);
            tabUsuarios.TabIndex = 1;
            tabUsuarios.Text = "Crear Usuario";
            // 
            // usuarioLayout
            // 
            usuarioLayout.ColumnCount = 2;
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            usuarioLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            usuarioLayout.Controls.Add(cbxPersona, 1, 0);
            usuarioLayout.Controls.Add(txtUsuario, 1, 1);
            usuarioLayout.Controls.Add(txtPassword, 1, 2);
            usuarioLayout.Controls.Add(cbxRolUsuario, 1, 3);
            usuarioLayout.Controls.Add(btnCrearUsuario, 1, 4);
            usuarioLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            usuarioLayout.Location = new System.Drawing.Point(0, 0);
            usuarioLayout.Name = "usuarioLayout";
            usuarioLayout.RowCount = 6;
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            usuarioLayout.Size = new System.Drawing.Size(492, 372);
            usuarioLayout.TabIndex = 0;
            // 
            // cbxPersona
            // 
            cbxPersona.Location = new System.Drawing.Point(175, 3);
            cbxPersona.Name = "cbxPersona";
            cbxPersona.Size = new System.Drawing.Size(121, 25);
            cbxPersona.TabIndex = 1;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new System.Drawing.Point(175, 33);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(100, 25);
            txtUsuario.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(175, 63);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(100, 25);
            txtPassword.TabIndex = 5;
            // 
            // cbxRolUsuario
            // 
            cbxRolUsuario.Location = new System.Drawing.Point(175, 93);
            cbxRolUsuario.Name = "cbxRolUsuario";
            cbxRolUsuario.Size = new System.Drawing.Size(121, 25);
            cbxRolUsuario.TabIndex = 7;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Location = new System.Drawing.Point(175, 123);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new System.Drawing.Size(75, 23);
            btnCrearUsuario.TabIndex = 8;
            btnCrearUsuario.Text = "Crear Usuario";
            // 
            // tabGestionUsuarios
            // 
            tabGestionUsuarios.Controls.Add(gestionUsuariosLayout);
            tabGestionUsuarios.Location = new System.Drawing.Point(4, 24);
            tabGestionUsuarios.Name = "tabGestionUsuarios";
            tabGestionUsuarios.Size = new System.Drawing.Size(492, 372);
            tabGestionUsuarios.TabIndex = 2;
            tabGestionUsuarios.Text = "Gestion de Usuarios";
            // 
            // gestionUsuariosLayout
            // 
            gestionUsuariosLayout.ColumnCount = 3;
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            gestionUsuariosLayout.Controls.Add(dgvUsuarios, 0, 0);
            gestionUsuariosLayout.Controls.Add(buttonPanel, 0, 1);
            gestionUsuariosLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            gestionUsuariosLayout.Location = new System.Drawing.Point(0, 0);
            gestionUsuariosLayout.Name = "gestionUsuariosLayout";
            gestionUsuariosLayout.RowCount = 2;
            gestionUsuariosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            gestionUsuariosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            gestionUsuariosLayout.Size = new System.Drawing.Size(492, 372);
            gestionUsuariosLayout.TabIndex = 0;
            // 
            // dgvUsuarios
            // 
            gestionUsuariosLayout.SetColumnSpan(dgvUsuarios, 3);
            dgvUsuarios.Location = new System.Drawing.Point(3, 3);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new System.Drawing.Size(240, 150);
            dgvUsuarios.TabIndex = 0;
            // 
            // buttonPanel
            // 
            gestionUsuariosLayout.SetColumnSpan(buttonPanel, 3);
            buttonPanel.Controls.Add(btnEliminarUsuario);
            buttonPanel.Controls.Add(btnGuardarCambios);
            buttonPanel.Controls.Add(btnRefrescarUsuarios);
            buttonPanel.Location = new System.Drawing.Point(3, 319);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(200, 50);
            buttonPanel.TabIndex = 1;
            // 
            // btnEliminarUsuario
            // 
            btnEliminarUsuario.Location = new System.Drawing.Point(3, 3);
            btnEliminarUsuario.Name = "btnEliminarUsuario";
            btnEliminarUsuario.Size = new System.Drawing.Size(75, 23);
            btnEliminarUsuario.TabIndex = 0;
            btnEliminarUsuario.Text = "Eliminar Usuario";
            // 
            // btnGuardarCambios
            // 
            btnGuardarCambios.Location = new System.Drawing.Point(84, 3);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new System.Drawing.Size(75, 23);
            btnGuardarCambios.TabIndex = 1;
            btnGuardarCambios.Text = "Guardar Cambios";
            // 
            // btnRefrescarUsuarios
            // 
            btnRefrescarUsuarios.Location = new System.Drawing.Point(3, 32);
            btnRefrescarUsuarios.Name = "btnRefrescarUsuarios";
            btnRefrescarUsuarios.Size = new System.Drawing.Size(75, 23);
            btnRefrescarUsuarios.TabIndex = 2;
            btnRefrescarUsuarios.Text = "Refrescar";
            // 
            // tabConfiguracion
            // 
            tabConfiguracion.Controls.Add(configuracionLayout);
            tabConfiguracion.Location = new System.Drawing.Point(4, 24);
            tabConfiguracion.Name = "tabConfiguracion";
            tabConfiguracion.Size = new System.Drawing.Size(492, 372);
            tabConfiguracion.TabIndex = 3;
            tabConfiguracion.Text = "Configuración";
            // 
            // configuracionLayout
            // 
            configuracionLayout.ColumnCount = 2;
            configuracionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            configuracionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            configuracionLayout.Controls.Add(chkMayusculasMinusculas, 0, 0);
            configuracionLayout.Controls.Add(chkNumeros, 0, 1);
            configuracionLayout.Controls.Add(chkCaracteresEspeciales, 0, 2);
            configuracionLayout.Controls.Add(chkDobleFactor, 0, 3);
            configuracionLayout.Controls.Add(chkNoRepetirContrasenas, 0, 4);
            configuracionLayout.Controls.Add(chkVerificarDatosPersonales, 0, 5);
            configuracionLayout.Controls.Add(txtMinCaracteres, 1, 6);
            configuracionLayout.Controls.Add(txtCantPreguntas, 1, 7);
            configuracionLayout.Controls.Add(btnGuardarConfig, 1, 8);
            configuracionLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            configuracionLayout.Location = new System.Drawing.Point(0, 0);
            configuracionLayout.Name = "configuracionLayout";
            configuracionLayout.RowCount = 9;
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            configuracionLayout.Size = new System.Drawing.Size(492, 372);
            configuracionLayout.TabIndex = 0;
            // 
            // chkMayusculasMinusculas
            // 
            chkMayusculasMinusculas.Location = new System.Drawing.Point(3, 3);
            chkMayusculasMinusculas.Name = "chkMayusculasMinusculas";
            chkMayusculasMinusculas.Size = new System.Drawing.Size(271, 24);
            chkMayusculasMinusculas.TabIndex = 0;
            chkMayusculasMinusculas.Text = "Combinar mayúsculas y minúsculas";
            // 
            // chkNumeros
            // 
            chkNumeros.Location = new System.Drawing.Point(3, 33);
            chkNumeros.Name = "chkNumeros";
            chkNumeros.Size = new System.Drawing.Size(271, 24);
            chkNumeros.TabIndex = 1;
            chkNumeros.Text = "Requerir números";
            // 
            // chkCaracteresEspeciales
            // 
            chkCaracteresEspeciales.Location = new System.Drawing.Point(3, 63);
            chkCaracteresEspeciales.Name = "chkCaracteresEspeciales";
            chkCaracteresEspeciales.Size = new System.Drawing.Size(271, 24);
            chkCaracteresEspeciales.TabIndex = 2;
            chkCaracteresEspeciales.Text = "Requerir caracteres especiales";
            // 
            // chkDobleFactor
            // 
            chkDobleFactor.Location = new System.Drawing.Point(3, 93);
            chkDobleFactor.Name = "chkDobleFactor";
            chkDobleFactor.Size = new System.Drawing.Size(271, 24);
            chkDobleFactor.TabIndex = 3;
            chkDobleFactor.Text = "Usar doble factor";
            // 
            // chkNoRepetirContrasenas
            // 
            chkNoRepetirContrasenas.Location = new System.Drawing.Point(3, 123);
            chkNoRepetirContrasenas.Name = "chkNoRepetirContrasenas";
            chkNoRepetirContrasenas.Size = new System.Drawing.Size(271, 24);
            chkNoRepetirContrasenas.TabIndex = 4;
            chkNoRepetirContrasenas.Text = "No repetir contraseñas anteriores";
            // 
            // chkVerificarDatosPersonales
            // 
            chkVerificarDatosPersonales.Location = new System.Drawing.Point(3, 153);
            chkVerificarDatosPersonales.Name = "chkVerificarDatosPersonales";
            chkVerificarDatosPersonales.Size = new System.Drawing.Size(271, 24);
            chkVerificarDatosPersonales.TabIndex = 5;
            chkVerificarDatosPersonales.Text = "Verificar datos personales";
            // 
            // txtMinCaracteres
            // 
            txtMinCaracteres.Location = new System.Drawing.Point(347, 183);
            txtMinCaracteres.Name = "txtMinCaracteres";
            txtMinCaracteres.Size = new System.Drawing.Size(100, 25);
            txtMinCaracteres.TabIndex = 7;
            // 
            // txtCantPreguntas
            // 
            txtCantPreguntas.Location = new System.Drawing.Point(347, 213);
            txtCantPreguntas.Name = "txtCantPreguntas";
            txtCantPreguntas.Size = new System.Drawing.Size(100, 25);
            txtCantPreguntas.TabIndex = 9;
            // 
            // btnGuardarConfig
            // 
            btnGuardarConfig.Location = new System.Drawing.Point(347, 243);
            btnGuardarConfig.Name = "btnGuardarConfig";
            btnGuardarConfig.Size = new System.Drawing.Size(75, 23);
            btnGuardarConfig.TabIndex = 10;
            btnGuardarConfig.Text = "Guardar";
            // 
            // AdminForm
            // 
            ClientSize = new System.Drawing.Size(500, 400);
            Controls.Add(tabControl);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdminForm";
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
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            buttonPanel.ResumeLayout(false);
            tabConfiguracion.ResumeLayout(false);
            configuracionLayout.ResumeLayout(false);
            configuracionLayout.PerformLayout();
            ResumeLayout(false);
        }
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    }
}