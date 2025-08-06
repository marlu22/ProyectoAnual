using System;
using System.Windows.Forms;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class UserForm : Form
    {
        private readonly IUserService _userService;
        private readonly string _username;

        public UserForm(IUserService userService, string username)
        {
            InitializeComponent();
            _userService = userService;
            _username = username;

            btnCambiarContrasena.Click += (s, e) =>
            {
                var form = new CambioContrasenaForm(_userService, _username);
                form.ShowDialog();
            };

            btnCambiarPreguntas.Click += (s, e) =>
            {
                var form = new PreguntasSeguridadForm(_userService, _username);
                form.ShowDialog();
            };
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
