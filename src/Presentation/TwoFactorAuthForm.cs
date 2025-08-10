// src/Presentation/TwoFactorAuthForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Models;

namespace Presentation
{
    public partial class TwoFactorAuthForm : Form
    {
        private readonly IUserAuthenticationService _authService;
        private readonly string _username;

        public AuthenticationResult? AuthResult { get; private set; }

        public TwoFactorAuthForm(IUserAuthenticationService authService, string username)
        {
            InitializeComponent();
            _authService = authService;
            _username = username;
            btnVerificar.Click += BtnVerificar_Click;
        }

        private async void BtnVerificar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Por favor, ingrese el código de verificación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AuthResult = await _authService.Validate2faAsync(_username, txtCodigo.Text.Trim());

            if (AuthResult.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(AuthResult.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
