using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;
using CMDR.Systems;
using GLFW;

namespace CMDR
{
    public sealed class Display : Form
    {
        internal static Window Window;
        public Display(int width, int height, string title)
        {
            (Camera.Width, Camera.Height) = (width, height);
            
            try
            {
                Glfw.Init();
            }
            catch
            {
                GLFW.Exception.GetErrorMessage(Glfw.GetError(out string error));
                throw new System.Exception(error);
            }

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
            Glfw.MakeContextCurrent(Window);


            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(width, height);
            this.Text = Assembly.GetEntryAssembly().GetName().ToString();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GameLoop.Running = false;
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
