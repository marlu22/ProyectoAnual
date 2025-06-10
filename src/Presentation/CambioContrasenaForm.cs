using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Exceptions;

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

        private void BtnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                string nueva = txtNueva.Text;
                string repetir = txtRepetir.Text;

                if (nueva != repetir)
                    throw new ValidationException("Las contraseñas no coinciden.");

                _userService.CambiarContrasena(_usuario, nueva);
                MessageBox.Show("Contraseña cambiada correctamente.", "Info");
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