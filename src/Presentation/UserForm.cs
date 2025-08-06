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

            btnMiPerfil.Click += (s, e) =>
            {
                var user = _userService.GetUserByUsername(_username);
                if (user != null)
                {
                    var persona = _userService.GetPersonaById(user.IdPersona);
                    if (persona != null)
                    {
                        var form = new ProfileForm(user, persona);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron los datos de la persona.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.", "Error");
                }
            };
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
