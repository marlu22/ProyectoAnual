// src/Presentation/LoginForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;

        public LoginForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            btnLogin.Click += BtnLogin_Click;
            btnRecuperarContrasena.Click += BtnRecuperarContrasena_Click;
        }

        private void BtnLogin_Click(object? sender, EventArgs? e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            var user = _userService.Authenticate(usuario, contrasena);

            if (user != null)
            {
                if (user.CambioContrasenaObligatorio)
                {
                    Hide();
                    var cambioContrasenaForm = new CambioContrasenaForm(_userService, user.Username);
                    cambioContrasenaForm.ShowDialog();
                    Show();
                }
                else
                {
                    if (user.Rol == "Administrador")
                    {
                        Hide();
                        var adminForm = new AdminForm(_userService);
                        adminForm.ShowDialog();
                        Show();
                    }
                    else
                    {
                        Hide();
                        var userForm = new UserForm(_userService, user.Username);
                        userForm.ShowDialog();
                        Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRecuperarContrasena_Click(object? sender, EventArgs? e)
        {
            var form = new RecuperarContrasenaForm(_userService);
            form.ShowDialog();
        }
    }
}