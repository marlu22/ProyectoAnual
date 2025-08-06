// src/Presentation/AdminForm.cs
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using BusinessLogic.Services;
using UserManagementSystem.BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace Presentation
{
    public partial class AdminForm : Form
    {
        private readonly IUserService _userService;
        private readonly string _username;
        private PoliticaSeguridad? _politica;
        private readonly List<int> _dirtyUserIds = new List<int>();

        public AdminForm(IUserService userService, string username)
        {
            InitializeComponent();
            _username = username;

            // Hide password controls for user creation as it's now automated
            lblPassword.Visible = false;
            txtPassword.Visible = false;

            _userService = userService;

            // Navigation
            btnNavigatePersonas.Click += (s, e) => ShowPanel(panelPersonas);
            btnNavigateUsuarios.Click += (s, e) => ShowPanel(panelUsuarios);
            btnNavigateGestion.Click += (s, e) => ShowPanel(panelGestionUsuarios);
            btnNavigateGestionPersonas.Click += (s, e) => ShowPanel(panelGestionPersonas);
            btnNavigateConfiguracion.Click += (s, e) => ShowPanel(panelConfiguracion);
            btnMiPerfil.Click += (s, e) =>
            {
                var user = _userService.GetUserByUsername(_username);
                if (user != null)
                {
                    var persona = _userService.GetPersonaById(user.IdPersona);
                    if (persona != null)
                    {
                        var form = new ProfileForm(user, persona);
                        form.ShowDialog();
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

            // Cargar combos al iniciar
            LoadTipoDoc();
            LoadProvincias();
            LoadGeneros();
            LoadPersonas();
            LoadRoles();

            // Setup cascading dropdowns
            cbxProvincia.SelectedIndexChanged += CbxProvincia_SelectedIndexChanged;
            cbxPartido.SelectedIndexChanged += CbxPartido_SelectedIndexChanged;

            cbxPartido.Enabled = false;
            cbxLocalidad.Enabled = false;

            LoadPoliticaSeguridad();

            btnGuardarPersona.Click += BtnGuardarPersona_Click;
            btnCrearUsuario.Click += BtnCrearUsuario_Click;
            btnGuardarConfig.Click += BtnGuardarConfig_Click;

            // Gestion de Usuarios
            btnRefrescarUsuarios.Click += (s, e) => LoadUsers();
            btnRefrescarPersonas.Click += (s, e) => LoadPersonasGrid();
            btnGuardarCambios.Click += BtnGuardarCambios_Click;
            btnEliminarUsuario.Click += BtnEliminarUsuario_Click;
            dgvUsuarios.CellEndEdit += DgvUsuarios_CellEndEdit;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            dtpFechaExpiracionGestion.ValueChanged += dtpFechaExpiracionGestion_ValueChanged;
            dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;


            LoadUsers();
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

        private void LoadUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                var userDtos = users.Select(u => new UserDto
                {
                    IdUsuario = u.IdUsuario,
                    Username = u.UsuarioNombre,
                    NombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : "N/A",
                    Rol = u.Rol?.Nombre,
                    IdRol = u.IdRol,
                    CambioContrasenaObligatorio = u.CambioContrasenaObligatorio,
                    FechaExpiracion = u.FechaExpiracion,
                    Habilitado = u.FechaBloqueo > DateTime.Now
                }).ToList();

                dgvUsuarios.DataSource = userDtos;

                // Configure columns
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
                dgvUsuarios.Columns["IdRol"].Visible = false;
                dgvUsuarios.Columns["NombreCompleto"].ReadOnly = true;
                dgvUsuarios.Columns["CambioContrasenaObligatorio"].ReadOnly = true;

                if (dgvUsuarios.Columns["FechaExpiracion"] != null)
                {
                    dgvUsuarios.Columns["FechaExpiracion"].HeaderText = "Fecha de Expiración";
                    dgvUsuarios.Columns["FechaExpiracion"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvUsuarios.Columns["Habilitado"] != null)
                {
                    dgvUsuarios.Columns["Habilitado"].HeaderText = "Habilitado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error");
            }
        }

        private void DgvUsuarios_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var userDto = (UserDto)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
                if (!_dirtyUserIds.Contains(userDto.IdUsuario))
                {
                    _dirtyUserIds.Add(userDto.IdUsuario);
                }
            }
        }

        private void BtnGuardarCambios_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.DataSource is List<UserDto> userDtos)
                {
                    var usersToUpdate = userDtos.Where(u => _dirtyUserIds.Contains(u.IdUsuario)).ToList();
                    foreach (var userDto in usersToUpdate)
                    {
                        _userService.UpdateUser(userDto);
                    }

                    if (usersToUpdate.Any())
                    {
                        MessageBox.Show("Cambios guardados exitosamente.", "Éxito");
                    }
                    else
                    {
                        MessageBox.Show("No hay cambios para guardar.", "Información");
                    }

                    _dirtyUserIds.Clear();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error");
            }
        }

        private void BtnEliminarUsuario_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Advertencia");
                return;
            }

            var selectedRow = dgvUsuarios.SelectedRows[0];
            var userDto = (UserDto)selectedRow.DataBoundItem;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar al usuario '{userDto.Username}'?",
                                                 "Confirmar Eliminación",
                                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _userService.DeleteUser(userDto.IdUsuario);
                    MessageBox.Show("Usuario eliminado exitosamente.", "Éxito");
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error");
                }
            }
        }

        private void LoadPoliticaSeguridad()
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
                _politica = new PoliticaSeguridad { IdPolitica = 1 };
            }

            _politica.MayusYMinus = chkMayusculasMinusculas.Checked;
            _politica.LetrasYNumeros = chkNumeros.Checked;
            _politica.CaracterEspecial = chkCaracteresEspeciales.Checked;
            _politica.Autenticacion2FA = chkDobleFactor.Checked;
            _politica.NoRepetirAnteriores = chkNoRepetirContrasenas.Checked;
            _politica.SinDatosPersonales = chkVerificarDatosPersonales.Checked;
            _politica.MinCaracteres = minChars;
            _politica.CantPreguntas = cantPreg;

            _userService.UpdatePoliticaSeguridad(_politica);
            MessageBox.Show("Configuración guardada correctamente.", "Info");
        }

        private void LoadTipoDoc()
        {
            var tiposDoc = _userService.GetTiposDoc();
            if (tiposDoc == null || !tiposDoc.Any())
            {
                MessageBox.Show("No se encontraron tipos de documento.", "Advertencia");
                cbxTipoDoc.DataSource = null;
                return;
            }
            cbxTipoDoc.DataSource = tiposDoc;
            cbxTipoDoc.DisplayMember = "Nombre";
            cbxTipoDoc.ValueMember = "IdTipoDoc"; // Changed to match TipoDoc
        }

        private void LoadProvincias()
        {
            var provincias = _userService.GetProvincias();
            if (provincias != null && provincias.Any())
            {
                cbxProvincia.DataSource = provincias;
                cbxProvincia.DisplayMember = "Nombre";
                cbxProvincia.ValueMember = "IdProvincia";
            }
        }

        private void CbxProvincia_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbxProvincia.SelectedValue is int provinciaId)
            {
                var partidos = _userService.GetPartidosByProvinciaId(provinciaId);
                if (partidos != null && partidos.Any())
                {
                    cbxPartido.DataSource = partidos;
                    cbxPartido.DisplayMember = "Nombre";
                    cbxPartido.ValueMember = "IdPartido";
                    cbxPartido.Enabled = true;
                }
                else
                {
                    cbxPartido.DataSource = null;
                    cbxPartido.Enabled = false;
                }
            }
            cbxLocalidad.DataSource = null;
            cbxLocalidad.Enabled = false;
        }

        private void CbxPartido_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbxPartido.SelectedValue is int partidoId)
            {
                LoadLocalidadesByPartido(partidoId);
            }
        }

        private void LoadLocalidadesByPartido(int partidoId)
        {
            var localidades = _userService.GetLocalidadesByPartidoId(partidoId);
            if (localidades != null && localidades.Any())
            {
                cbxLocalidad.DataSource = localidades;
                cbxLocalidad.DisplayMember = "Nombre";
                cbxLocalidad.ValueMember = "IdLocalidad";
                cbxLocalidad.Enabled = true;
            }
            else
            {
                cbxLocalidad.DataSource = null;
                cbxLocalidad.Enabled = false;
            }
        }

        private void LoadGeneros()
        {
            var generos = _userService.GetGeneros();
            if (generos == null || !generos.Any())
            {
                MessageBox.Show("No se encontraron géneros.", "Advertencia");
                cbxGenero.DataSource = null;
                return;
            }
            cbxGenero.DataSource = generos;
            cbxGenero.DisplayMember = "Nombre";
            cbxGenero.ValueMember = "IdGenero"; // Changed to match Genero
        }

        private void LoadPersonas()
        {
            try
            {
                var personas = _userService.GetPersonas();
                if (personas == null || !personas.Any())
                {
                    MessageBox.Show("No se encontraron personas en la base de datos.", "Advertencia");
                    cbxPersona.DataSource = null;
                    return;
                }
                cbxPersona.DataSource = personas;
                cbxPersona.DisplayMember = "NombreCompleto"; // Matches new Persona property
                cbxPersona.ValueMember = "IdPersona"; // Fixed from "Id" to "IdPersona"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error");
            }
        }

        private void LoadRoles()
        {
            var roles = _userService.GetRoles();
            if (roles == null || !roles.Any())
            {
                MessageBox.Show("No se encontraron roles.", "Advertencia");
                cbxRolUsuario.DataSource = null;
                return;
            }
            cbxRolUsuario.DataSource = roles;
            cbxRolUsuario.DisplayMember = "Nombre";
            cbxRolUsuario.ValueMember = "IdRol"; // Changed to match Rol
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
                _userService.CrearPersona(persona);
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

        private void BtnCrearUsuario_Click(object? sender, EventArgs e) // Fixed nullable annotations
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
                    PersonaId = cbxPersona.SelectedValue.ToString()!, // Ensure string conversion
                    Username = txtUsuario.Text,
                    Rol = cbxRolUsuario.Text // Use .Text to get the string value
                };
                _userService.CrearUsuario(usuario);
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
                // Temporarily unsubscribe to prevent the ValueChanged event from firing
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
                // Re-subscribe to the event
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
                    if (!_dirtyUserIds.Contains(selectedUser.IdUsuario))
                    {
                        _dirtyUserIds.Add(selectedUser.IdUsuario);
                    }
                    dgvUsuarios.Refresh();
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

        private void btnGuardarPersona_Click_1(object sender, EventArgs e)
        {

        }

        private void LoadPersonasGrid()
        {
            try
            {
                var personas = _userService.GetPersonas();
                dgvPersonas.DataSource = personas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error");
            }
        }
    }
}