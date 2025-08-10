using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using BusinessLogic.Services;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Helpers;

namespace Presentation
{
    public partial class AdminForm : Form
    {
        private readonly IUserManagementService _managementService;
        private readonly IReferenceDataService _referenceService;
        private readonly IServiceProvider _serviceProvider;
        private readonly DataGridViewManager _userGridManager;
        private readonly ComboBoxLoader _comboBoxLoader;
        private string _username = string.Empty;
        private PoliticaSeguridadDto? _politica;
        private List<PersonaDto> _allPersonas = new List<PersonaDto>();
        private readonly List<int> _dirtyPersonaIds = new List<int>();

        public AdminForm(IUserManagementService managementService, IReferenceDataService referenceService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _managementService = managementService;
            _referenceService = referenceService;
            _serviceProvider = serviceProvider;
            _userGridManager = new DataGridViewManager(managementService, dgvUsuarios);
            _comboBoxLoader = new ComboBoxLoader(referenceService);

            SetupForm();
        }

        public void Initialize(string username)
        {
            _username = username;
            // Any other setup that depends on the username can go here.
        }

        private void SetupForm()
        {
            DataGridViewStyler.ApplyTheme(dgvUsuarios);
            DataGridViewStyler.ApplyTheme(dgvPersonas);

            lblPassword.Visible = false;
            txtPassword.Visible = false;

            btnNavigatePersonas.Click += (s, e) => ShowPanel(panelPersonas);
            btnNavigateUsuarios.Click += (s, e) => ShowPanel(panelUsuarios);
            btnNavigateGestion.Click += (s, e) => ShowPanel(panelGestionUsuarios);
            btnNavigateGestionPersonas.Click += (s, e) => ShowPanel(panelGestionPersonas);
            btnNavigateConfiguracion.Click += (s, e) => ShowPanel(panelConfiguracion);
            btnMiPerfil.Click += (s, e) =>
            {
                var user = _managementService.GetUserByUsername(_username);
                if (user != null)
                {
                    var persona = _managementService.GetPersonaById(user.IdPersona);
                    if (persona != null)
                    {
                        using (var form = _serviceProvider.GetRequiredService<ProfileForm>())
                        {
                            form.Initialize(user, persona);
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron los datos de la persona.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.", "Error");
                }
            };

            _comboBoxLoader.LoadTiposDoc(cbxTipoDoc);
            _comboBoxLoader.LoadProvincias(cbxProvincia);
            _comboBoxLoader.LoadGeneros(cbxGenero);
            LoadPersonas();
            _comboBoxLoader.LoadRoles(cbxRolUsuario);

            cbxProvincia.SelectedIndexChanged += CbxProvincia_SelectedIndexChanged;
            cbxPartido.SelectedIndexChanged += CbxPartido_SelectedIndexChanged;
            cbxPartido.Enabled = false;
            cbxLocalidad.Enabled = false;

            LoadPoliticaSeguridad();

            btnGuardarPersona.Click += BtnGuardarPersona_Click;
            btnCrearUsuario.Click += BtnCrearUsuario_Click;
            btnGuardarConfig.Click += BtnGuardarConfig_Click;

            txtBuscarUsuario.TextChanged += TxtBuscarUsuario_TextChanged;
            btnRefrescarUsuarios.Click += (s, e) =>
            {
                _userGridManager.LoadUsers();
                txtBuscarUsuario.Clear();
            };
            btnGuardarCambios.Click += (s, e) => _userGridManager.SaveChanges();
            btnEliminarUsuario.Click += (s, e) => _userGridManager.DeleteSelectedUser();
            //dgvUsuarios.CellEndEdit += DgvUsuarios_CellEndEdit; // This is now handled inside the manager
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            dtpFechaExpiracionGestion.ValueChanged += dtpFechaExpiracionGestion_ValueChanged;
            dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;

            txtBuscarPersona.TextChanged += TxtBuscarPersona_TextChanged;
            btnRefrescarPersonas.Click += (s, e) =>
            {
                LoadPersonasGrid();
                txtBuscarPersona.Clear();
            };
            btnGuardarCambiosPersona.Click += BtnGuardarCambiosPersona_Click;
            btnEliminarPersona.Click += BtnEliminarPersona_Click;
            dgvPersonas.CellEndEdit += DgvPersonas_CellEndEdit;

            _userGridManager.LoadUsers();
            LoadPersonasGrid();

            dtpFechaExpiracionGestion.ShowCheckBox = true;
            dtpFechaExpiracionGestion.Checked = false;
        }

        private void ShowPanel(Panel panelToShow)
        {
            panelPersonas.Visible = false;
            panelUsuarios.Visible = false;
            panelGestionUsuarios.Visible = false;
            panelGestionPersonas.Visible = false;
            panelConfiguracion.Visible = false;
            panelToShow.Visible = true;
        }

        private void TxtBuscarUsuario_TextChanged(object? sender, EventArgs e)
        {
            _userGridManager.FilterUsers(txtBuscarUsuario.Text);
        }

        private void TxtBuscarPersona_TextChanged(object? sender, EventArgs e)
        {
            var searchText = txtBuscarPersona.Text.ToLower().Trim();

            var filteredPersonas = _allPersonas.Where(p =>
                (p.Nombre?.ToLower() ?? "").Contains(searchText) ||
                (p.Apellido?.ToLower() ?? "").Contains(searchText) ||
                (p.NumDoc?.ToLower() ?? "").Contains(searchText)
            ).ToList();

            dgvPersonas.DataSource = filteredPersonas;
        }

        private void LoadPoliticaSeguridad()
        {
            _politica = _managementService.GetPoliticaSeguridad();
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
                // Default values if no policy is set
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

        private void BtnGuardarConfig_Click(object? sender, EventArgs e)
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

            if (_politica == null)
            {
                _politica = new PoliticaSeguridadDto { IdPolitica = 1 };
            }

            _politica.MayusYMinus = chkMayusculasMinusculas.Checked;
            _politica.LetrasYNumeros = chkNumeros.Checked;
            _politica.CaracterEspecial = chkCaracteresEspeciales.Checked;
            _politica.Autenticacion2FA = chkDobleFactor.Checked;
            _politica.NoRepetirAnteriores = chkNoRepetirContrasenas.Checked;
            _politica.SinDatosPersonales = chkVerificarDatosPersonales.Checked;
            _politica.MinCaracteres = minChars;
            _politica.CantPreguntas = cantPreg;

            _managementService.UpdatePoliticaSeguridad(_politica);
            MessageBox.Show("Configuración guardada correctamente.", "Info");
        }

        private void CbxProvincia_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbxProvincia.SelectedValue is int provinciaId)
            {
                _comboBoxLoader.LoadPartidos(cbxPartido, provinciaId);
            }
            cbxLocalidad.DataSource = null;
            cbxLocalidad.Enabled = false;
        }

        private void CbxPartido_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbxPartido.SelectedValue is int partidoId)
            {
                _comboBoxLoader.LoadLocalidades(cbxLocalidad, partidoId);
            }
        }

        private void LoadPersonas()
        {
            try
            {
                var personas = _managementService.GetPersonas();
                if (personas == null || !personas.Any())
                {
                    MessageBox.Show("No se encontraron personas en la base de datos.", "Advertencia");
                    cbxPersona.DataSource = null;
                    return;
                }
                cbxPersona.DataSource = personas;
                cbxPersona.DisplayMember = "NombreCompleto";
                cbxPersona.ValueMember = "IdPersona";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error");
            }
        }

        private void BtnGuardarPersona_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLegajo.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    cbxTipoDoc.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtNumDoc.Text) ||
                    string.IsNullOrWhiteSpace(txtCuil.Text) ||
                    string.IsNullOrWhiteSpace(txtCalle.Text) ||
                    string.IsNullOrWhiteSpace(txtAltura.Text) ||
                    cbxLocalidad.SelectedItem == null ||
                    cbxLocalidad.SelectedValue == null ||
                    cbxGenero.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtCelular.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error");
                    return;
                }

                var persona = new PersonaRequest
                {
                    Legajo = txtLegajo.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    TipoDoc = cbxTipoDoc.Text,
                    NumDoc = txtNumDoc.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Cuil = txtCuil.Text,
                    Calle = txtCalle.Text,
                    Altura = txtAltura.Text,
                    Localidad = cbxLocalidad.SelectedValue!.ToString()!,
                    Genero = cbxGenero.Text,
                    Correo = txtCorreo.Text,
                    Celular = txtCelular.Text,
                    FechaIngreso = dtpFechaIngreso.Value
                };
                _managementService.CrearPersona(persona);
                MessageBox.Show("Persona guardada correctamente", "Info");
                LoadPersonas();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCrearUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                    cbxPersona.SelectedValue == null ||
                    cbxRolUsuario.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error");
                    return;
                }

                var usuario = new UserRequest
                {
                    PersonaId = cbxPersona.SelectedValue.ToString()!,
                    Username = txtUsuario.Text,
                    Rol = cbxRolUsuario.Text
                };
                _managementService.CrearUsuario(usuario);
                MessageBox.Show("Usuario creado correctamente. La contraseña ha sido enviada al correo de la persona.", "Info");
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var selectedUser = (UserDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                dtpFechaExpiracionGestion.ValueChanged -= dtpFechaExpiracionGestion_ValueChanged;

                if (selectedUser.FechaExpiracion.HasValue)
                {
                    dtpFechaExpiracionGestion.Value = selectedUser.FechaExpiracion.Value;
                    dtpFechaExpiracionGestion.Checked = true;
                }
                else
                {
                    dtpFechaExpiracionGestion.Checked = false;
                }
                dtpFechaExpiracionGestion.ValueChanged += dtpFechaExpiracionGestion_ValueChanged;
            }
        }

        private void dtpFechaExpiracionGestion_ValueChanged(object? sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var selectedUser = (UserDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                var newValue = dtpFechaExpiracionGestion.Checked ? dtpFechaExpiracionGestion.Value : (DateTime?)null;

                if (selectedUser.FechaExpiracion != newValue)
                {
                    selectedUser.FechaExpiracion = newValue;
                    _userGridManager.AddDirtyUserId(selectedUser.IdUsuario);
                    dgvUsuarios.Refresh();
                }
            }
        }

        private void DgvPersonas_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var persona = (PersonaDto)dgvPersonas.Rows[e.RowIndex].DataBoundItem;
                if (!_dirtyPersonaIds.Contains(persona.IdPersona))
                {
                    _dirtyPersonaIds.Add(persona.IdPersona);
                }
            }
        }

        private void dgvUsuarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsuarios.Rows.Count)
            {
                var userDto = (UserDto)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
                if (userDto != null)
                {
                    if (!userDto.Habilitado)
                    {
                        dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                    }
                    else
                    {
                        dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
                        dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
                    }
                }
            }
        }

        private void BtnGuardarCambiosPersona_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvPersonas.DataSource is List<PersonaDto> personas)
                {
                    var personasToUpdate = personas.Where(p => _dirtyPersonaIds.Contains(p.IdPersona)).ToList();
                    foreach (var persona in personasToUpdate)
                    {
                        _managementService.UpdatePersona(persona);
                    }

                    if (personasToUpdate.Any())
                    {
                        MessageBox.Show("Cambios guardados exitosamente.", "Éxito");
                    }
                    else
                    {
                        MessageBox.Show("No hay cambios para guardar.", "Información");
                    }

                    _dirtyPersonaIds.Clear();
                    LoadPersonasGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error");
            }
        }

        private void BtnEliminarPersona_Click(object? sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una persona para eliminar.", "Advertencia");
                return;
            }

            var selectedRow = dgvPersonas.SelectedRows[0];
            var persona = (PersonaDto)selectedRow.DataBoundItem;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar a la persona '{persona.Nombre} {persona.Apellido}'?",
                                                 "Confirmar Eliminación",
                                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _managementService.DeletePersona(persona.IdPersona);
                    MessageBox.Show("Persona eliminada exitosamente.", "Éxito");
                    LoadPersonasGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar persona: {ex.Message}", "Error");
                }
            }
        }

        private void LoadPersonasGrid()
        {
            try
            {
                _allPersonas = _managementService.GetPersonas();
                dgvPersonas.DataSource = new List<PersonaDto>(_allPersonas);

                dgvPersonas.Columns["IdPersona"].Visible = false;
                dgvPersonas.Columns["IdTipoDoc"].Visible = false;
                dgvPersonas.Columns["IdLocalidad"].Visible = false;
                dgvPersonas.Columns["IdPartido"].Visible = false;
                dgvPersonas.Columns["IdProvincia"].Visible = false;
                dgvPersonas.Columns["IdGenero"].Visible = false;
                dgvPersonas.Columns["Calle"].Visible = false;
                dgvPersonas.Columns["Altura"].Visible = false;
                dgvPersonas.Columns["FechaIngreso"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error");
            }
        }

    }
}