using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessLogic.Services;
using DataAccess.Entities;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace Presentation
{
    public partial class RecuperarContrasenaForm : Form
    {
        private readonly IUserService _userService;
        private List<PreguntaSeguridad> _preguntasUsuario = new List<PreguntaSeguridad>();

        public RecuperarContrasenaForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;

            // Configure initial state
            preguntasLayoutPanel.Visible = false;
            btnRecuperar.Visible = false;

            // Wire up events
            btnContinuar.Click += BtnContinuar_Click;
            btnRecuperar.Click += BtnRecuperar_Click;
        }

        private void BtnContinuar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var usuario = txtUsuario.Text.Trim();
                _preguntasUsuario = _userService.GetPreguntasDeUsuario(usuario);

                if (_preguntasUsuario.Count > 0)
                {
                    MostrarPreguntas(_preguntasUsuario);
                    preguntasLayoutPanel.Visible = true;
                    btnRecuperar.Visible = true;
                    txtUsuario.Enabled = false; // Prevent user from changing username
                    btnContinuar.Enabled = false; // Prevent clicking again
                }
                else
                {
                    MessageBox.Show("El usuario no tiene preguntas de seguridad configuradas o el usuario no existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarPreguntas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar preguntas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarPreguntas();
            }
        }

        private void MostrarPreguntas(List<PreguntaSeguridad> preguntas)
        {
            LimpiarPreguntas(); // Limpia controles y estilos de fila anteriores

            preguntasLayoutPanel.RowCount = preguntas.Count;
            for (int i = 0; i < preguntas.Count; i++)
            {
                var pregunta = preguntas[i];

                // Añadir un estilo de fila para la nueva fila
                preguntasLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var label = new Label
                {
                    Text = pregunta.Pregunta,
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    Margin = new Padding(3, 0, 3, 10) // Margen inferior para espaciar
                };

                var textBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Tag = pregunta.IdPregunta, // Store question ID
                    Margin = new Padding(3, 0, 3, 10) // Margen inferior
                };

                preguntasLayoutPanel.Controls.Add(label, 0, i);
                preguntasLayoutPanel.Controls.Add(textBox, 1, i);
            }
            // Forzar al panel a redibujar su contenido
            preguntasLayoutPanel.PerformLayout();
        }

        private void LimpiarPreguntas()
        {
            _preguntasUsuario.Clear();
            preguntasLayoutPanel.Controls.Clear();
            preguntasLayoutPanel.RowStyles.Clear(); // Limpiar los estilos de fila
            preguntasLayoutPanel.RowCount = 0;
            preguntasLayoutPanel.Visible = false;
            btnRecuperar.Visible = false;
        }

        private void BtnRecuperar_Click(object? sender, EventArgs? e)
        {
            try
            {
                var respuestas = new Dictionary<int, string>();
                foreach (Control control in preguntasLayoutPanel.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            MessageBox.Show("Por favor, responda todas las preguntas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (textBox.Tag is int idPregunta)
                        {
                            respuestas.Add(idPregunta, textBox.Text);
                        }
                    }
                }

                if (respuestas.Count != _preguntasUsuario.Count)
                {
                     MessageBox.Show("No se pudieron recolectar todas las respuestas. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                }

                string usuario = txtUsuario.Text.Trim();
                _userService.RecuperarContrasena(usuario, respuestas);
                MessageBox.Show("Si las respuestas son correctas, se envió una nueva contraseña a su correo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}