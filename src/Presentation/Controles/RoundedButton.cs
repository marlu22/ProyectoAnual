using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Presentation.Controles
{
    public class RoundedButton : Button
    {
        private Color _baseColor = Color.FromArgb(59, 130, 246);
        private Color _hoverColor = Color.FromArgb(37, 99, 235);
        private Color _currentColor;
        private Timer _animationTimer;
        private bool _hovering = false;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _currentColor = _baseColor;

            _animationTimer = new Timer();
            _animationTimer.Interval = 15;
            _animationTimer.Tick += AnimationTick;

            MouseEnter += (s, e) => { _hovering = true; _animationTimer.Start(); };
            MouseLeave += (s, e) => { _hovering = false; _animationTimer.Start(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = ClientRectangle;
            using (var path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, 20, 20, 180, 90);
                path.AddArc(rect.Right - 20, rect.Y, 20, 20, 270, 90);
                path.AddArc(rect.Right - 20, rect.Bottom - 20, 20, 20, 0, 90);
                path.AddArc(rect.X, rect.Bottom - 20, 20, 20, 90, 90);
                path.CloseFigure();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(_currentColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
                TextRenderer.DrawText(e.Graphics, Text, Font, rect, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            var targetColor = _hovering ? _hoverColor : _baseColor;
            int r = Animate(_currentColor.R, targetColor.R);
            int g = Animate(_currentColor.G, targetColor.G);
            int b = Animate(_currentColor.B, targetColor.B);

            var nextColor = Color.FromArgb(r, g, b);
            if (nextColor == _currentColor)
            {
                _animationTimer.Stop();
            }
            _currentColor = nextColor;
            Invalidate();
        }

        private int Animate(int current, int target)
        {
            if (current == target) return current;
            int diff = target - current;
            int step = Math.Sign(diff) * Math.Max(1, Math.Abs(diff) / 5);
            return current + step;
        }
    }
}