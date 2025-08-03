// src/Presentation/AdminForm.cs
using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.Services;
using UserManagementSystem.BusinessLogic.Exceptions;
using BusinessLogic.Models;

namespace Presentation
{
    public partial class AdminForm : Form
    {
        private readonly IUserService _userService;

        public AdminForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;

            // Cargar combos al iniciar
            LoadTipoDoc();
            LoadLocalidades();
            LoadGeneros();
            LoadPersonas();
            LoadRoles();

            btnGuardarPersona.Click += BtnGuardarPersona_Click;
            btnCrearUsuario.Click += BtnCrearUsuario_Click;
            btnConfiguracion.Click += BtnConfiguracion_Click;
        }

        private void BtnConfiguracion_Click(object? sender, EventArgs e) // Fixed nullable annotations
        {
            var form = new ConfiguracionForm(_userService);
            form.ShowDialog();
        }

        private void LoadTipoDoc()
        {
            var tiposDoc = _userService.GetTiposDoc();
            if (tiposDoc == null || !tiposDoc.Any())
            {
                MessageBox.Show("No se encontraron tipos de documento.", "Advertencia");
                cbxTipoDoc.DataSource = null;
                return;
            }
            cbxTipoDoc.DataSource = tiposDoc;
            cbxTipoDoc.DisplayMember = "Nombre";
            cbxTipoDoc.ValueMember = "IdTipoDoc"; // Changed to match TipoDoc
        }

        private void LoadLocalidades()
        {
            var localidades = _userService.GetLocalidades();
            if (localidades == null || !localidades.Any())
            {
                MessageBox.Show("No se encontraron localidades.", "Advertencia");
                cbxLocalidad.DataSource = null;
                return;
            }
            cbxLocalidad.DataSource = localidades;
            cbxLocalidad.DisplayMember = "Nombre";
            cbxLocalidad.ValueMember = "IdLocalidad"; // Changed to match Localidad
        }

        private void LoadGeneros()
        {
            var generos = _userService.GetGeneros();
            if (generos == null || !generos.Any())
            {
                MessageBox.Show("No se encontraron géneros.", "Advertencia");
                cbxGenero.DataSource = null;
                return;
            }
            cbxGenero.DataSource = generos;
            cbxGenero.DisplayMember = "Nombre";
            cbxGenero.ValueMember = "IdGenero"; // Changed to match Genero
        }

        private void LoadPersonas()
        {
            try
            {
                var personas = _userService.GetPersonas();
                if (personas == null || !personas.Any())
                {
                    MessageBox.Show("No se encontraron personas en la base de datos.", "Advertencia");
                    cbxPersona.DataSource = null;
                    return;
                }
                cbxPersona.DataSource = personas;
                cbxPersona.DisplayMember = "NombreCompleto"; // Matches new Persona property
                cbxPersona.ValueMember = "IdPersona"; // Fixed from "Id" to "IdPersona"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error");
            }
        }

        private void LoadRoles()
        {
            var roles = _userService.GetRoles();
            if (roles == null || !roles.Any())
            {
                MessageBox.Show("No se encontraron roles.", "Advertencia");
                cbxRolUsuario.DataSource = null;
                return;
            }
            cbxRolUsuario.DataSource = roles;
            cbxRolUsuario.DisplayMember = "Nombre";
            cbxRolUsuario.ValueMember = "IdRol"; // Changed to match Rol
        }

        private void BtnGuardarPersona_Click(object? sender, EventArgs e) // Fixed nullable annotations
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLegajo.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    cbxTipoDoc.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtNumDoc.Text) ||
                    string.IsNullOrWhiteSpace(txtCuil.Text) ||
                    string.IsNullOrWhiteSpace(txtCalle.Text) ||
                    string.IsNullOrWhiteSpace(txtAltura.Text) ||
                    cbxLocalidad.SelectedItem == null ||
                    cbxGenero.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error");
                    return;
                }

                var persona = new PersonaRequest
                {
                    Legajo = txtLegajo.Text, // Changed to string to match Persona.Legajo
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    TipoDoc = cbxTipoDoc.Text, // Use .Text to get the string value
                    NumDoc = txtNumDoc.Text,
                    Cuil = txtCuil.Text,
                    Calle = txtCalle.Text,
                    Altura = txtAltura.Text,
                    Localidad = cbxLocalidad.Text, // Use .Text to get the string value
                    Genero = cbxGenero.Text, // Use .Text to get the string value
                    Correo = txtCorreo.Text
                };
                _userService.CrearPersona(persona);
                MessageBox.Show("Persona guardada correctamente", "Info");
                LoadPersonas();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCrearUsuario_Click(object? sender, EventArgs e) // Fixed nullable annotations
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    cbxPersona.SelectedValue == null ||
                    cbxRolUsuario.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error");
                    return;
                }

                var usuario = new UserRequest
                {
                    PersonaId = cbxPersona.SelectedValue.ToString()!, // Ensure string conversion
                    Username = txtUsuario.Text,
                    Password = txtPassword.Text,
                    Rol = cbxRolUsuario.Text // Use .Text to get the string value
                };
                _userService.CrearUsuario(usuario);
                MessageBox.Show("Usuario creado correctamente", "Info");
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}