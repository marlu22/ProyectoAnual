using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using BusinessLogic.Models;

public partial class CreateUserForm : Form
{
    private readonly IUserService _userService;

    public CreateUserForm(IUserService userService)
    {
        InitializeComponent();
        _userService = userService;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var request = new UserRequest
        {
            Username = txtUsername.Text,
            Password = txtPassword.Text,
            Email = txtEmail.Text
        };

        _userService.CreateUser(request);
        MessageBox.Show("Usuario creado correctamente.");
        Close();
    }
}
