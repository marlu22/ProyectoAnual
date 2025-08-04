using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Presentation.Controles
{
    public class RoundedTextBox : TextBox
    {
        public RoundedTextBox()
        {
            BorderStyle = BorderStyle.None;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            BackColor = Color.FromArgb(40, 40, 56);
            ForeColor = Color.White;
            Padding = new Padding(8, 6, 8, 6);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, 15, 15, 180, 90);
                path.AddArc(rect.Right - 15, rect.Y, 15, 15, 270, 90);
                path.AddArc(rect.Right - 15, rect.Bottom - 15, 15, 15, 0, 90);
                path.AddArc(rect.X, rect.Bottom - 15, 15, 15, 90, 90);
                path.CloseFigure();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(59, 130, 246), 2))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F) // WM_PAINT
            {
                using (var g = CreateGraphics())
                {
                    OnPaint(new PaintEventArgs(g, ClientRectangle));
                }
            }
        }
    }
}