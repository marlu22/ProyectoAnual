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
                chkMayusculasMinusculas.Checked = _politica.MayusYMinus;
                chkNumeros.Checked = _politica.LetrasYNumeros;
                chkCaracteresEspeciales.Checked = _politica.CaracterEspecial;
                chkDobleFactor.Checked = _politica.Autenticacion2FA;
                chkNoRepetirContrasenas.Checked = _politica.NoRepetirAnteriores;
                chkVerificarDatosPersonales.Checked = _politica.SinDatosPersonales;
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

            _politica.MayusYMinus = chkMayusculasMinusculas.Checked;
            _politica.LetrasYNumeros = chkNumeros.Checked;
            _politica.CaracterEspecial = chkCaracteresEspeciales.Checked;
            _politica.Autenticacion2FA = chkDobleFactor.Checked;
            _politica.NoRepetirAnteriores = chkNoRepetirContrasenas.Checked;
            _politica.SinDatosPersonales = chkVerificarDatosPersonales.Checked;
            _politica.MinCaracteres = int.Parse(txtMinCaracteres.Text);
            _politica.CantPreguntas = int.Parse(txtCantPreguntas.Text);

            _userService.UpdatePoliticaSeguridad(_politica);
            MessageBox.Show("Configuración guardada correctamente.", "Info");
            Close();
        }
    }
}
