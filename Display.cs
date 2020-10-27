using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;
using CMDR.Systems;

namespace CMDR
{
    public sealed class Display : Form
    {
        public Display(int width, int height)
        {
            (Camera.Width, Camera.Height) = (width, height);
            
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(width, height);
            this.Text = Assembly.GetEntryAssembly().GetName().ToString();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        public void Start()
        {

            Render.SetDisplay(this);
            Application.EnableVisualStyles();
            GameLoop.Start();
            Application.Run(this);
        }
    }
}
