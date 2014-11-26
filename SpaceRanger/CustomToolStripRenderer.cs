using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRanger
{
    class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(187, 101, 26)), rectangle);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(48, 70, 95)), 1, 0, rectangle.Width - 2, rectangle.Height - 1);
            }
        }
    }
}
