// src/Presentation/TwoFactorAuthForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class TwoFactorAuthForm : Form
    {
        private readonly IUserService _userService;
        private readonly string _username;

        public TwoFactorAuthForm(IUserService userService, string username)
        {
            InitializeComponent();
            _userService = userService;
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

            var user = await _userService.Validate2faAsync(_username, txtCodigo.Text.Trim());

            if (user != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("El código de verificación es incorrecto o ha expirado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
