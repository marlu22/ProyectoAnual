// src/Presentation/LoginForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Models;

namespace Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IUserManagementService _managementService;
        private readonly IReferenceDataService _referenceService;

        public LoginForm(
            IUserAuthenticationService authService,
            IUserManagementService managementService,
            IReferenceDataService referenceService)
        {
            InitializeComponent();
            _authService = authService;
            _managementService = managementService;
            _referenceService = referenceService;
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
                var twoFactorForm = new TwoFactorAuthForm(_authService, username);
                if (twoFactorForm.ShowDialog() == DialogResult.OK)
                {
                    authResult = twoFactorForm.AuthResult;
                }
                else
                {
                    return; // 2FA cancelled or failed
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
                    new CambioContrasenaForm(_authService, authResult.User.Username).ShowDialog();
                    break;
                case PostLoginAction.ShowAdminDashboard:
                    new AdminForm(_authService, _managementService, _referenceService, authResult.User.Username).ShowDialog();
                    break;
                case PostLoginAction.ShowUserDashboard:
                    new UserForm(_authService, _managementService, authResult.User.Username).ShowDialog();
                    break;
            }
            Show();
        }

        private void BtnRecuperarContrasena_Click(object? sender, EventArgs e)
        {
            var form = new RecuperarContrasenaForm(_authService);
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

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
         this.Close(); // Close the form when the icon is clicked   
        }
    }
}