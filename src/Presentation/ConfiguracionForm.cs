using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using DataAccess.Entities;

namespace Presentation
{
    public partial class ConfiguracionForm : Form
    {
        private readonly IUserService _userService;
        private PoliticaSeguridad _politica;

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
                chkMayusculasMinusculas.Checked = _politica.CombinarMayusculasMinusculas;
                chkNumeros.Checked = _politica.RequerirNumeros;
                chkCaracteresEspeciales.Checked = _politica.RequerirCaracteresEspeciales;
                chkDobleFactor.Checked = _politica.UsarDobleFactor;
                chkNoRepetirContrasenas.Checked = _politica.NoRepetirContrasenasAnteriores;
                chkVerificarDatosPersonales.Checked = _politica.VerificarDatosPersonales;
                txtMinCaracteres.Text = _politica.MinCaracteres.ToString();
                txtCantPreguntas.Text = _politica.CantPreguntas.ToString();
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (_politica == null)
            {
                _politica = new PoliticaSeguridad();
            }

            _politica.CombinarMayusculasMinusculas = chkMayusculasMinusculas.Checked;
            _politica.RequerirNumeros = chkNumeros.Checked;
            _politica.RequerirCaracteresEspeciales = chkCaracteresEspeciales.Checked;
            _politica.UsarDobleFactor = chkDobleFactor.Checked;
            _politica.NoRepetirContrasenasAnteriores = chkNoRepetirContrasenas.Checked;
            _politica.VerificarDatosPersonales = chkVerificarDatosPersonales.Checked;
            _politica.MinCaracteres = int.Parse(txtMinCaracteres.Text);
            _politica.CantPreguntas = int.Parse(txtCantPreguntas.Text);

            _userService.UpdatePoliticaSeguridad(_politica);
            MessageBox.Show("Configuración guardada correctamente.", "Info");
            Close();
        }
    }
}
