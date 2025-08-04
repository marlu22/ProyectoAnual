// src/Presentation/CambioContrasenaForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace Presentation
{
    public partial class CambioContrasenaForm : Form
    {
        private readonly IUserService _userService;
        private readonly string _usuario;

        public CambioContrasenaForm(IUserService userService, string usuario)
        {
            InitializeComponent();
            _userService = userService;
            _usuario = usuario;
            btnCambiar.Click += BtnCambiar_Click;
        }

        private void BtnCambiar_Click(object? sender, EventArgs? e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtActual.Text) ||
                    string.IsNullOrWhiteSpace(txtNueva.Text) ||
                    string.IsNullOrWhiteSpace(txtRepetir.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error");
                    return;
                }

                string actual = txtActual.Text;
                string nueva = txtNueva.Text;
                string repetir = txtRepetir.Text;

                if (nueva != repetir)
                {
                    MessageBox.Show("Las contraseñas nuevas no coinciden.", "Error");
                    return;
                }

                _userService.CambiarContrasena(_usuario, nueva, actual);
                MessageBox.Show("Contraseña cambiada correctamente.", "Info");

                // Check if this is the first time the user is changing the password
                // If so, force them to set security questions.
                var user = _userService.GetPreguntasDeUsuario(_usuario);
                if (user == null || user.Count == 0)
                {
                    var preguntasForm = new PreguntasSeguridadForm(_userService, _usuario);
                    preguntasForm.ShowDialog();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}