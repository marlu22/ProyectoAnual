using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmAlerta : Form
    {
        private System.Windows.Forms.Timer timer;
        private bool fadeIn = true;

        public frmAlerta(string mensaje)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.lblAlerta.Text = mensaje;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30; // Velocidad de transición
            timer.Tick += Timer_Tick;
        }

        public void EmpezarFade(bool aparecer)
        {
            fadeIn = aparecer;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double paso = 0.10;

            if (fadeIn)
            {
                if (this.Opacity < 1)
                    this.Opacity += paso;
                else
                    timer.Stop();
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity -= paso;
                else
                {
                    timer.Stop();
                    this.Hide();
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 2500; // 2,5 segundos
            t.Tick += (s, args) =>
            {
                t.Stop();
                this.EmpezarFade(false);

            };
            t.Start();
        }

        private void lblAlerta_Click(object sender, EventArgs e)
        {
            this.EmpezarFade(false);// Desaparecer con fade
        }

        private void frmAlerta_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.EmpezarFade(false); // Desaparecer con fade
        }

        private void frmAlerta_Load(object sender, EventArgs e)
        {

        }
    }
}
