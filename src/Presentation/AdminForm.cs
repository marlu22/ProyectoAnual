// src/Presentation/AdminForm.cs
using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Exceptions;
using BusinessLogic.Models; // Ensure this is included

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
            btnRecuperarContrasena.Click += BtnRecuperarContrasena_Click;
            btnConfiguracion.Click += BtnConfiguracion_Click;
        }

        private void BtnConfiguracion_Click(object? sender, EventArgs? e)
        {
            var form = new ConfiguracionForm(_userService);
            form.ShowDialog();
        }

        private void LoadTipoDoc()
        {
            cbxTipoDoc.DataSource = _userService.GetTiposDoc();
            cbxTipoDoc.DisplayMember = "Nombre";
            cbxTipoDoc.ValueMember = "Nombre";
        }

        private void LoadLocalidades()
        {
            cbxLocalidad.DataSource = _userService.GetLocalidades();
            cbxLocalidad.DisplayMember = "Nombre";
            cbxLocalidad.ValueMember = "Nombre";
        }

        private void LoadGeneros()
        {
            cbxGenero.DataSource = _userService.GetGeneros();
            cbxGenero.DisplayMember = "Nombre";
            cbxGenero.ValueMember = "Nombre";
        }

        private void LoadPersonas()
        {
            var personas = _userService.GetPersonas();
            cbxPersona.DataSource = personas;
            cbxPersona.DisplayMember = "NombreCompleto";
            cbxPersona.ValueMember = "Id";
        }

        private void LoadRoles()
        {
            cbxRolUsuario.DataSource = _userService.GetRoles();
            cbxRolUsuario.DisplayMember = "Nombre";
            cbxRolUsuario.ValueMember = "Nombre";
        }

        private void BtnGuardarPersona_Click(object? sender, EventArgs? e)
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
                    Legajo = int.Parse(txtLegajo.Text), // Parse string to int
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    TipoDoc = cbxTipoDoc.SelectedItem.ToString()!,
                    NumDoc = txtNumDoc.Text,
                    Cuil = txtCuil.Text,
                    Calle = txtCalle.Text,
                    Altura = txtAltura.Text,
                    Localidad = cbxLocalidad.SelectedItem.ToString()!,
                    Genero = cbxGenero.SelectedItem.ToString()!,
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

        private void BtnCrearUsuario_Click(object? sender, EventArgs? e)
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
                    PersonaId = cbxPersona.SelectedValue.ToString()!,
                    Username = txtUsuario.Text,
                    Password = txtPassword.Text,
                    Rol = cbxRolUsuario.SelectedItem.ToString()!
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

        private void BtnRecuperarContrasena_Click(object? sender, EventArgs? e)
        {
            var form = new RecuperarContrasenaForm(_userService);
            form.ShowDialog();
        }
    }
}