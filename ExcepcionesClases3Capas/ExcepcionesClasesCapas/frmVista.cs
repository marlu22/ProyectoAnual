using Logica.ExepcionesPersonalizadas;
using Vista;


namespace ExcepcionesClasesCapas
{
    public partial class frmVista : Form
    {
        Logica.LogicaBuscarCliente BuscaCliente = new Logica.LogicaBuscarCliente();
        Logica.LogicaRetiroFondos Retirar = new Logica.LogicaRetiroFondos();

        public frmVista()
        {
            InitializeComponent();
        }

        private void frmVista_Load(object sender, EventArgs e)
        {
            //Cargo los datos de prueba (Esto es solo a modo de ejemplo 
            //ya que la capa vista JAMAS debe acceder a la de datos)
            ModeloDatos.ListaSaldosClientes.InicializarLista();
        }

        private void btnExtraer_Click(object sender, EventArgs e)
        {
            retiro();
        }

        private void retiro()
        {
            try
            {
                Retirar.NroCuenta = txtNroCuenta.Text;
                Retirar.Importe = txtExtraccion.Text;
                Retirar.Retirar();
                buscarCliente();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ventanaError("ERROR: ARGUMENTO FUERA DE RANGO" + Environment.NewLine + ex.Message);
                if (ex.ParamName == "edad") txtEdad.Focus();
                else if (ex.ParamName == "importe") txtExtraccion.Focus();
            }
            catch (ExceptionConversion ex)
            {
                ventanaError("ERROR: NO SE PUDO CONVERTIR EL TIPO DE DATO" + Environment.NewLine + ex.Message);
                if (ex.ParamName == "edad") txtEdad.Focus();
                else if (ex.ParamName == "importe") txtExtraccion.Focus();
            }
            catch (ExceptionFondosInsuficientes ex)
            {
                ventanaError("ERROR: FONDOS INSUFICIENTES" + Environment.NewLine + ex.Message);
                if (ex.ParamName == "importe") txtExtraccion.Focus();
            }
            catch (ArgumentException ex)
            {
                ventanaError($"ERROR: {ex.Message}");
                if (ex.Message.Contains("importe")) txtExtraccion.Focus();
            }
            catch (Exception ex)
            {
                ventanaError($"ERROR: {ex.Message}");
            }

        }
        private void ventanaError(string mensaje)
        {
            frmAlerta panelFlotante = new frmAlerta(mensaje);

            panelFlotante.Show(this);
            panelFlotante.EmpezarFade(true);
            panelFlotante.Focus();
        }

        private void txtExtraccion_Enter(object sender, EventArgs e)
        {
            txtExtraccion.SelectAll();
        }

        private void txtNroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buscarCliente();
            }
        }

        private void buscarCliente()
        {
            try
            {
                BuscaCliente.NroCuenta = txtNroCuenta.Text;

                lblNombre.Text = BuscaCliente.Nombre;
                lblTipoCuenta.Text = BuscaCliente.TipoCuenta;
                lblDNI.Text = BuscaCliente.DNI;
                lblSaldo.Text = BuscaCliente.Saldo;

                txtExtraccion.Enabled = true;
                txtExtraccion.Focus();
            }
            catch (ExceptionConversion ex)
            {
                ventanaError("ERROR: NO SE PUDO CONVERTIR EL TIPO DE DATO" + Environment.NewLine + ex.Message);
            }
            catch (ArgumentException ex)
            {
                ventanaError($"ERROR: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                ventanaError($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                ventanaError($"ERROR: {ex.Message}");
            }

        }

        private void txtExtraccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                retiro();
            }

        }
    }
}
