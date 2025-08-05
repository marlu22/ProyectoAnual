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

        private async void BtnLogin_Click(object? sender, EventArgs? e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUsuario.Text.Trim();
            string password = txtContrasena.Text.Trim();

            var authResult = await _userService.AuthenticateAsync(username, password);

            if (authResult.Success)
            {
                BusinessLogic.Models.UserResponse? user = authResult.User;

                if (authResult.Requires2fa)
                {
                    var twoFactorForm = new TwoFactorAuthForm(_userService, username);
                    if (twoFactorForm.ShowDialog() == DialogResult.OK)
                    {
                        user = twoFactorForm.User;
                    }
                    else
                    {
                        return; // 2FA cancelled or failed
                    }
                }

                if (user == null)
                {
                    MessageBox.Show("Ocurrió un error inesperado después de la verificación. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                MessageBox.Show(authResult.ErrorMessage, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRecuperarContrasena_Click(object? sender, EventArgs? e)
        {
            var form = new RecuperarContrasenaForm(_userService);
            form.ShowDialog();
        }

        private void ChkMostrarContrasena_CheckedChanged(object? sender, EventArgs e)
        {
            if (chkMostrarContrasena.Checked)
            {
                txtContrasena.PasswordChar = '\0'; // Show password
            }
            else
            {
                txtContrasena.PasswordChar = '●'; // Hide password
            }
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}