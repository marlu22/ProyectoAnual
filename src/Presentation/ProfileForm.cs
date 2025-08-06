using System;
using System.Windows.Forms;
using DataAccess.Entities;
using BusinessLogic.Models;

namespace Presentation
{
    public partial class ProfileForm : Form
    {
        public ProfileForm(Usuario user, Persona persona)
        {
            InitializeComponent();
            LoadProfileData(user, persona);
        }

        private void LoadProfileData(Usuario user, Persona persona)
        {
            // User data
            lblUsername.Text = user.UsuarioNombre;
            lblRol.Text = user.Rol?.Nombre ?? "N/A";

            // Persona data
            lblNombre.Text = persona.Nombre;
            lblApellido.Text = persona.Apellido;
            lblLegajo.Text = persona.Legajo.ToString();
            lblTipoDoc.Text = persona.TipoDoc?.Nombre ?? "N/A";
            lblNumDoc.Text = persona.NumDoc;
            lblFechaNacimiento.Text = persona.FechaNacimiento?.ToString("dd/MM/yyyy") ?? "N/A";
            lblCuil.Text = persona.Cuil;
            lblLocalidad.Text = persona.Localidad?.Nombre ?? "N/A";
            lblCalle.Text = persona.Calle;
            lblAltura.Text = persona.Altura;
            lblGenero.Text = persona.Genero?.Nombre ?? "N/A";
            lblCorreo.Text = persona.Correo;
            lblCelular.Text = persona.Celular;
            lblFechaIngreso.Text = persona.FechaIngreso.ToString("dd/MM/yyyy");
        }
    }
}
