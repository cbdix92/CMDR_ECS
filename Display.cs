using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace CMDR
{
    public sealed class Display : Form
    {
        public Display(int sizeX, int sizeY)
        {
            (Camera.SizeX, Camera.SizeY) = (sizeX, sizeY);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(sizeX, sizeY);
            this.Text = Assembly.GetEntryAssembly().GetName().ToString();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
