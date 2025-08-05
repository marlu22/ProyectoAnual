// src/Presentation/frmNotification.cs
using System;
using System.Windows.Forms;
using Presentation.Theme;

namespace Presentation
{
    public partial class frmNotification : Form
    {
        public frmNotification(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.BackColor = ThemeColors.Danger;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
