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
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (usuario == "admin" && contrasena == "admin123")
            {
                Hide();
                var adminForm = new AdminForm(_userService);
                adminForm.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}