// src/Presentation/ConfiguracionForm.cs
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using DataAccess.Entities;

namespace Presentation
{
    public partial class ConfiguracionForm : Form
    {
        private readonly IUserService _userService;
        private PoliticaSeguridad? _politica;

        public ConfiguracionForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            btnGuardar.Click += BtnGuardar_Click;
        }

        private void ConfiguracionForm_Load(object sender, EventArgs e)
        {
            _politica = _userService.GetPoliticaSeguridad();
            if (_politica != null)
            {
                chkMayusculasMinusculas.Checked = _politica.MayusYMinus;
                chkNumeros.Checked = _politica.LetrasYNumeros;
                chkCaracteresEspeciales.Checked = _politica.CaracterEspecial;
                chkDobleFactor.Checked = _politica.Autenticacion2FA;
                chkNoRepetirContrasenas.Checked = _politica.NoRepetirAnteriores;
                chkVerificarDatosPersonales.Checked = _politica.SinDatosPersonales;
                txtMinCaracteres.Text = _politica.MinCaracteres.ToString();
                txtCantPreguntas.Text = _politica.CantPreguntas.ToString();
            }
            else
            {
                chkMayusculasMinusculas.Checked = false;
                chkNumeros.Checked = false;
                chkCaracteresEspeciales.Checked = false;
                chkDobleFactor.Checked = false;
                chkNoRepetirContrasenas.Checked = false;
                chkVerificarDatosPersonales.Checked = false;
                txtMinCaracteres.Text = "8";
                txtCantPreguntas.Text = "0";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e) // Remove nullable annotations
        {
            if (!int.TryParse(txtMinCaracteres.Text, out var minChars) || minChars <= 0)
            {
                MessageBox.Show("Por favor, ingrese un número válido de caracteres mínimos.", "Error");
                return;
            }
            if (!int.TryParse(txtCantPreguntas.Text, out var cantPreg) || cantPreg < 0)
            {
                MessageBox.Show("Por favor, ingrese un número válido de preguntas de seguridad.", "Error");
                return;
            }

            _politica = new PoliticaSeguridad
            {
                IdPolitica = _politica?.IdPolitica ?? 1,
                MayusYMinus = chkMayusculasMinusculas.Checked,
                LetrasYNumeros = chkNumeros.Checked,
                CaracterEspecial = chkCaracteresEspeciales.Checked,
                Autenticacion2FA = chkDobleFactor.Checked,
                NoRepetirAnteriores = chkNoRepetirContrasenas.Checked,
                SinDatosPersonales = chkVerificarDatosPersonales.Checked,
                MinCaracteres = minChars,
                CantPreguntas = cantPreg
            };

            _userService.UpdatePoliticaSeguridad(_politica);
            MessageBox.Show("Configuración guardada correctamente.", "Info");
            Close();
        }
    }
}