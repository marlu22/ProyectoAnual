using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.Models;
using BusinessLogic.Services;

namespace Presentation.Helpers
{
    public class DataGridViewManager
    {
        private readonly IUserManagementService _managementService;
        private readonly DataGridView _dataGridView;
        private List<UserDto> _allUsers;
        private readonly List<int> _dirtyUserIds = new List<int>();

        public DataGridViewManager(IUserManagementService managementService, DataGridView dataGridView)
        {
            _managementService = managementService;
            _dataGridView = dataGridView;
            _allUsers = new List<UserDto>();

            _dataGridView.CellEndEdit += DgvUsuarios_CellEndEdit;
        }

        public void LoadUsers()
        {
            try
            {
                _allUsers = _managementService.GetAllUsers();
                _dataGridView.DataSource = new List<UserDto>(_allUsers);
                // Configure columns as needed, this might need to be passed in or handled more generically
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error");
            }
        }

        public void FilterUsers(string searchText)
        {
            var filteredUsers = _allUsers.Where(u =>
                (u.Username?.ToLower() ?? "").Contains(searchText.ToLower().Trim()) ||
                (u.NombreCompleto?.ToLower() ?? "").Contains(searchText.ToLower().Trim())
            ).ToList();
            _dataGridView.DataSource = filteredUsers;
        }

        private void DgvUsuarios_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var userDto = (UserDto)_dataGridView.Rows[e.RowIndex].DataBoundItem;
                if (!_dirtyUserIds.Contains(userDto.IdUsuario))
                {
                    _dirtyUserIds.Add(userDto.IdUsuario);
                }
            }
        }

        public void AddDirtyUserId(int id)
        {
            if (!_dirtyUserIds.Contains(id))
            {
                _dirtyUserIds.Add(id);
            }
        }

        public void SaveChanges()
        {
            try
            {
                if (_dataGridView.DataSource is List<UserDto> userDtos)
                {
                    var usersToUpdate = userDtos.Where(u => _dirtyUserIds.Contains(u.IdUsuario)).ToList();
                    foreach (var userDto in usersToUpdate)
                    {
                        _managementService.UpdateUser(userDto);
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

        public void DeleteSelectedUser()
        {
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Advertencia");
                return;
            }

            var selectedRow = _dataGridView.SelectedRows[0];
            var userDto = (UserDto)selectedRow.DataBoundItem;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar al usuario '{userDto.Username}'?",
                                                 "Confirmar Eliminación",
                                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _managementService.DeleteUser(userDto.IdUsuario);
                    MessageBox.Show("Usuario eliminado exitosamente.", "Éxito");
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error");
                }
            }
        }
    }
}
