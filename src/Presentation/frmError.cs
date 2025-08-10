using System;
using System.Windows.Forms;

namespace Presentation
{
    public partial class frmError : Form
    {
        public frmError()
        {
            InitializeComponent();
        }

        public void SetError(Exception ex)
        {
            txtMessage.Text = ex.Message;
            txtExceptionType.Text = ex.GetType().FullName;
            txtStackTrace.Text = ex.StackTrace;
        }
    }
}
