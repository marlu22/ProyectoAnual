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
                    preguntasPanel.Visible = true; // Cambiado
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
            LimpiarPreguntas();
            int topPosition = 10; // Posición Y inicial

            foreach (var pregunta in preguntas)
            {
                var label = new Label
                {
                    Text = pregunta.Pregunta,
                    Left = 10,
                    Top = topPosition,
                    Width = preguntasPanel.Width - 20, // Ancho completo menos un margen
                    AutoSize = true // Permitir que el alto se ajuste
                };

                topPosition += label.Height + 5; // Espacio entre pregunta y respuesta

                var textBox = new TextBox
                {
                    Left = 10,
                    Top = topPosition,
                    Width = preguntasPanel.Width - 20,
                    Tag = pregunta.IdPregunta // Guardar el ID de la pregunta
                };

                topPosition += textBox.Height + 15; // Espacio para la siguiente pregunta

                preguntasPanel.Controls.Add(label);
                preguntasPanel.Controls.Add(textBox);
            }
        }

        private void LimpiarPreguntas()
        {
            _preguntasUsuario.Clear();
            preguntasPanel.Controls.Clear(); // Solo limpiar controles
            preguntasPanel.Visible = false;
            btnRecuperar.Visible = false;
        }

        private void BtnRecuperar_Click(object? sender, EventArgs? e)
        {
            try
            {
                var respuestas = new Dictionary<int, string>();
                foreach (Control control in preguntasPanel.Controls)
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