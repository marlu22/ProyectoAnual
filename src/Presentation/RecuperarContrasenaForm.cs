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
            btnRecuperar.Click += BtnRecuperar_Click;
            txtUsuario.Leave += TxtUsuario_Leave;

            // Ocultar preguntas y respuestas hasta que se carguen
            lblPregunta1.Visible = false;
            txtRespuesta1.Visible = false;
            lblPregunta2.Visible = false;
            txtRespuesta2.Visible = false;
        }

        private void TxtUsuario_Leave(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    // Limpiar y ocultar si el usuario borra el texto
                    _preguntasUsuario.Clear();
                    lblPregunta1.Visible = false;
                    txtRespuesta1.Visible = false;
                    lblPregunta2.Visible = false;
                    txtRespuesta2.Visible = false;
                    return;
                }

                var usuario = txtUsuario.Text.Trim();
                _preguntasUsuario = _userService.GetPreguntasDeUsuario(usuario);

                if (_preguntasUsuario.Count > 0)
                {
                    // Mostrar preguntas y campos de respuesta.
                    // La UI actual solo soporta 2, pero vamos a ser un poco flexibles
                    lblPregunta1.Text = _preguntasUsuario[0].Pregunta;
                    lblPregunta1.Visible = true;
                    txtRespuesta1.Visible = true;

                    if (_preguntasUsuario.Count > 1)
                    {
                        lblPregunta2.Text = _preguntasUsuario[1].Pregunta;
                        lblPregunta2.Visible = true;
                        txtRespuesta2.Visible = true;
                    }
                    else
                    {
                        lblPregunta2.Visible = false;
                        txtRespuesta2.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("El usuario no tiene preguntas de seguridad configuradas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _preguntasUsuario.Clear();
                    lblPregunta1.Visible = false;
                    txtRespuesta1.Visible = false;
                    lblPregunta2.Visible = false;
                    txtRespuesta2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar preguntas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _preguntasUsuario.Clear();
                lblPregunta1.Visible = false;
                txtRespuesta1.Visible = false;
                lblPregunta2.Visible = false;
                txtRespuesta2.Visible = false;
            }
        }

        private void BtnRecuperar_Click(object? sender, EventArgs? e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de usuario.", "Error");
                    return;
                }

                if (_preguntasUsuario == null || _preguntasUsuario.Count == 0)
                {
                    MessageBox.Show("Por favor, cargue las preguntas de seguridad para el usuario (salga del campo de texto del usuario para cargarlas).", "Error");
                    return;
                }

                var respuestas = new Dictionary<int, string>();

                // Validar y agregar respuestas basado en las preguntas visibles
                if (lblPregunta1.Visible && string.IsNullOrWhiteSpace(txtRespuesta1.Text))
                {
                    MessageBox.Show("Por favor, responda la pregunta 1.", "Error");
                    return;
                }
                if (lblPregunta1.Visible)
                {
                    respuestas.Add(_preguntasUsuario[0].IdPregunta, txtRespuesta1.Text);
                }

                if (lblPregunta2.Visible && string.IsNullOrWhiteSpace(txtRespuesta2.Text))
                {
                    MessageBox.Show("Por favor, responda la pregunta 2.", "Error");
                    return;
                }
                if (lblPregunta2.Visible)
                {
                    respuestas.Add(_preguntasUsuario[1].IdPregunta, txtRespuesta2.Text);
                }

                if (respuestas.Count == 0)
                {
                    MessageBox.Show("No se proporcionaron respuestas.", "Error");
                    return;
                }

                string usuario = txtUsuario.Text.Trim();
                _userService.RecuperarContrasena(usuario, respuestas);
                MessageBox.Show("Si las respuestas son correctas, se envió una nueva contraseña a su correo.", "Info");
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