using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Models;

public partial class MainForm : Form
{
    private readonly IUserService _userService;

    public MainForm(IUserService userService)
    {
        InitializeComponent();
        _userService = userService;
        LoadUsers();
    }

    private void LoadUsers()
    {
        var users = _userService.GetAllUsers();
        dgvUsers.DataSource = null; // Limpiar el DataGridView
        dgvUsers.DataSource = users; // Cargar la lista de usuarios
    }

    private void btnCreateUser_Click(object sender, EventArgs e)
    {
        var createUserForm = new CreateUserForm(_userService);
        createUserForm.ShowDialog();
        LoadUsers(); // Recargar la lista de usuarios
    }

    private void btnDeleteUser_Click(object sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count > 0)
        {
            var userId = (int)dgvUsers.SelectedRows[0].Cells["Id"].Value;
            _userService.DeleteUser(userId);
            MessageBox.Show("Usuario eliminado correctamente.");
            LoadUsers();
        }
        else
        {
            MessageBox.Show("Por favor, selecciona un usuario para eliminar.");
        }
    }
}
