// src/Presentation/PreguntasSeguridadForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class PreguntasSeguridadForm : Form
    {
        private readonly IUserService _userService;
        private readonly string _username;

        public PreguntasSeguridadForm(IUserService userService, string username)
        {
            InitializeComponent();
            _userService = userService;
            _username = username;
            btnGuardar.Click += BtnGuardar_Click;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRespuesta1.Text) || string.IsNullOrWhiteSpace(txtRespuesta2.Text))
            {
                MessageBox.Show("Por favor, responda ambas preguntas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _userService.GuardarRespuestasSeguridad(_username, new string[] { txtRespuesta1.Text, txtRespuesta2.Text });
                MessageBox.Show("Respuestas de seguridad guardadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar las respuestas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
