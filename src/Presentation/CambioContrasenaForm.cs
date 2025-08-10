// src/Presentation/CambioContrasenaForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Exceptions;

namespace Presentation
{
    public partial class CambioContrasenaForm : Form
    {
        private readonly IUserAuthenticationService _authService;
        private readonly string _usuario;

        public CambioContrasenaForm(IUserAuthenticationService authService, string usuario)
        {
            InitializeComponent();
            _authService = authService;
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

                _authService.CambiarContrasena(_usuario, nueva, actual);
                MessageBox.Show("Contraseña cambiada correctamente.", "Info");

                // Check if this is the first time the user is changing the password
                // If so, force them to set security questions.
                var user = _authService.GetPreguntasDeUsuario(_usuario);
                if (user == null || user.Count == 0)
                {
                    var preguntasForm = new PreguntasSeguridadForm(_authService, _usuario);
                    preguntasForm.ShowDialog();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ValidationException ex)
            {
                using (var errorForm = new frmNotification(ex.Message))
                {
                    errorForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}