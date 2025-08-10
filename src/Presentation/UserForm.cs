using System;
using System.Windows.Forms;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class UserForm : Form
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IUserManagementService _managementService;
        private readonly string _username;

        public UserForm(IUserAuthenticationService authService, IUserManagementService managementService, string username)
        {
            InitializeComponent();
            _authService = authService;
            _managementService = managementService;
            _username = username;

            btnCambiarContrasena.Click += (s, e) =>
            {
                var form = new CambioContrasenaForm(_authService, _username);
                form.ShowDialog();
            };

            btnCambiarPreguntas.Click += (s, e) =>
            {
                var form = new PreguntasSeguridadForm(_authService, _username);
                form.ShowDialog();
            };

            btnMiPerfil.Click += (s, e) =>
            {
                var user = _managementService.GetUserByUsername(_username);
                if (user != null)
                {
                    var persona = _managementService.GetPersonaById(user.IdPersona);
                    if (persona != null)
                    {
                        var form = new ProfileForm(user, persona); // This form also needs refactoring
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
