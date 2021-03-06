﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreComponent.Core.UI
{
    public class DoubleBufferedTabPage : System.Windows.Forms.TabPage
    {
        public DoubleBufferedTabPage()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
    }

    public class DoubleBufferedTabControl : System.Windows.Forms.TabControl
    {

        public DoubleBufferedTabControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.SetStyle(ControlStyles.UserPaint, false);
            base.OnPaint(pevent);
            System.Drawing.Rectangle o = pevent.ClipRectangle;
            System.Drawing.Graphics.FromImage(buffer).Clear(System.Drawing.SystemColors.Control);
            if (o.Width > 0 && o.Height > 0)
                DrawToBitmap(buffer, new System.Drawing.Rectangle(0, 0, Width, o.Height));
            pevent.Graphics.DrawImageUnscaled(buffer, 0, 0);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            buffer = new System.Drawing.Bitmap(Width, Height);
        }
        private System.Drawing.Bitmap buffer;
    }
}
