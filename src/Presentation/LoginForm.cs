using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IServiceProvider _serviceProvider;

        public LoginForm(IUserAuthenticationService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider;
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

            var authResult = await _authService.AuthenticateAsync(username, password);

            if (authResult.Requires2fa)
            {
                using (var twoFactorForm = _serviceProvider.GetRequiredService<TwoFactorAuthForm>())
                {
                    twoFactorForm.Initialize(username);
                    if (twoFactorForm.ShowDialog() == DialogResult.OK)
                    {
                        authResult = twoFactorForm.AuthResult;
                    }
                    else
                    {
                        return; // 2FA cancelled or failed
                    }
                }
            }

            if (authResult == null || !authResult.Success)
            {
                MessageBox.Show(authResult?.ErrorMessage ?? "Ocurrió un error desconocido.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (authResult.User == null)
            {
                 MessageBox.Show("Ocurrió un error inesperado al obtener los datos del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }

            Hide();
            switch (authResult.NextAction)
            {
                case PostLoginAction.ChangePassword:
                    using (var form = _serviceProvider.GetRequiredService<CambioContrasenaForm>())
                    {
                        form.Initialize(authResult.User.Username);
                        form.ShowDialog();
                    }
                    break;
                case PostLoginAction.ShowAdminDashboard:
                    using (var form = _serviceProvider.GetRequiredService<AdminForm>())
                    {
                        form.Initialize(authResult.User.Username);
                        form.ShowDialog();
                    }
                    break;
                case PostLoginAction.ShowUserDashboard:
                    using (var form = _serviceProvider.GetRequiredService<UserForm>())
                    {
                        form.Initialize(authResult.User.Username);
                        form.ShowDialog();
                    }
                    break;
            }
            Close(); // Close login form when another form is shown
        }

        private void BtnRecuperarContrasena_Click(object? sender, EventArgs e)
        {
            using (var form = _serviceProvider.GetRequiredService<RecuperarContrasenaForm>())
            {
                form.ShowDialog();
            }
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

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
         this.Close(); // Close the form when the icon is clicked   
        }
    }
}