using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public partial class UserForm : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserManagementService _managementService;
        private readonly IServiceProvider _serviceProvider;
        private string _username = string.Empty;

        public UserForm(IAuthenticationService authService, IUserManagementService managementService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _managementService = managementService;
            _serviceProvider = serviceProvider;

            btnCambiarContrasena.Click += BtnCambiarContrasena_Click;
            btnCambiarPreguntas.Click += BtnCambiarPreguntas_Click;
            btnMiPerfil.Click += BtnMiPerfil_Click;
        }

        public void Initialize(string username)
        {
            _username = username;
        }

        private void BtnCambiarContrasena_Click(object? sender, EventArgs e)
        {
            using (var form = _serviceProvider.GetRequiredService<CambioContrasenaForm>())
            {
                form.Initialize(_username);
                form.ShowDialog();
            }
        }

        private void BtnCambiarPreguntas_Click(object? sender, EventArgs e)
        {
            using (var form = _serviceProvider.GetRequiredService<PreguntasSeguridadForm>())
            {
                form.Initialize(_username);
                form.ShowDialog();
            }
        }

        private void BtnMiPerfil_Click(object? sender, EventArgs e)
        {
            var user = _managementService.GetUserByUsername(_username);
            if (user != null)
            {
                var persona = _managementService.GetPersonaById(user.IdPersona);
                if (persona != null)
                {
                    using (var form = _serviceProvider.GetRequiredService<ProfileForm>())
                    {
                        form.Initialize(user, persona);
                        form.ShowDialog();
                    }
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
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
