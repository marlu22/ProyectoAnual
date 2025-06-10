using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Exceptions;

namespace Presentation
{
    public partial class RecuperarContrasenaForm : Form
    {
        private readonly IUserService _userService;

        public RecuperarContrasenaForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            btnRecuperar.Click += BtnRecuperar_Click;
        }

        private void BtnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string[] respuestas = { txtRespuesta1.Text, txtRespuesta2.Text }; // según cantidad de preguntas

                _userService.RecuperarContrasena(usuario, respuestas);
                MessageBox.Show("Se envió una nueva contraseña a su correo.", "Info");
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