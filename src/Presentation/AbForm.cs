using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentation;
using BusinessLogic.Services;

namespace Presentation
{
    public partial class AbForm : Form
    {
        private readonly IUserService _userService;

        public AbForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }
    }
}
