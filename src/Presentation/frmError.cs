using System;
using System.Windows.Forms;

namespace Presentation
{
    public partial class frmError : Form
    {
        public frmError(Exception ex)
        {
            InitializeComponent();
            txtMessage.Text = ex.Message;
            txtExceptionType.Text = ex.GetType().FullName;
            txtStackTrace.Text = ex.StackTrace;
        }
    }
}
