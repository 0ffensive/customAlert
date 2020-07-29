using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomAlertBoxDemo.Controls
{
    public class MyGroupBox : GroupBox
    {
        public MyGroupBox()
        {
            base.BackColor = Color.Transparent;
            //base.ForeColor = Color.DarkBlue;

        }
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        public Color ActualBackColor { get; set; } = Color.Transparent;

        public Color EdgesColor { get; set; } = Color.Blue;

        protected override void OnPaint(PaintEventArgs e)
        {

            Size tSize = TextRenderer.MeasureText(this.Text, this.Font);

            Rectangle borderRect = e.ClipRectangle;

            borderRect.Y += tSize.Height / 2;

            borderRect.Height -= tSize.Height / 2;

            GraphicsPath gPath = FormsHelper.CreatePath(0, borderRect.Y, (float)(this.Width - 1), borderRect.Height - 1, 5, true, true, true, true);

            e.Graphics.FillPath(new SolidBrush(ActualBackColor), gPath);

            e.Graphics.DrawPath(new Pen(EdgesColor), gPath);

            borderRect.X += 6;
            borderRect.Y -= 7;

            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), borderRect);
            //e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), borderRect);
        }
    }
}
