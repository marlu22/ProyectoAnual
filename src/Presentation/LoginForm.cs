using System;
using System.Linq;
using System.Windows.Forms;
using DataAccess; // Ajusta si tu contexto está en otro namespace

namespace Presentation
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object? sender, EventArgs e) // Opcional: ajusta nulabilidad
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, complete ambos campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Usa tu contexto real
            using (var db = new ApplicationDbContext(/* opciones si es necesario */))
            {
                var user = db.Usuarios.FirstOrDefault(u => u.UsuarioNombre == usuario);

                if (user != null && VerificarContrasena(contrasena, user.ContrasenaScript))
                {
                    // Login exitoso
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para verificar la contraseña (ajusta según tu lógica de hash)
        private bool VerificarContrasena(string contrasenaIngresada, byte[] hashAlmacenado)
        {
            // Aquí deberías comparar el hash de la contraseña ingresada con el almacenado
            // Ejemplo simple (NO seguro, solo para ilustrar):
            // return Encoding.UTF8.GetBytes(contrasenaIngresada).SequenceEqual(hashAlmacenado);

            // Si usas SHA-256:
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var hashIngresado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasenaIngresada));
                return hashIngresado.SequenceEqual(hashAlmacenado);
            }
        }
    }
}