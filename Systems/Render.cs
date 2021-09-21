using System;
using System.Windows.Forms;
using System.Drawing;
using GLFW;
using OpenTK.Graphics.OpenGL4;

using CMDR.Components;

namespace CMDR.Systems
{
    internal static class Render
    {
        internal static Scene Scene { get => SceneManager.ActiveScene; }

        public static byte ZDepth;

        internal static System.Drawing.Drawing2D.MatrixOrder _append = System.Drawing.Drawing2D.MatrixOrder.Append;


        internal static int VAO;
        internal static int VBO;
        internal static void ClearScreen()
        {
            //Buffer.Graphics.Clear(Color.Black);
        }
        internal static void ScreenBuffer(long ticks)
        {

            Debugger.Draw(ticks);

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();

            (float camX,float camY) = (Camera.X, Camera.Y);

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            GL.BindVertexArray(VAO);

            foreach(SGameObject gameObject in Camera.GetRenderable(transforms))
            {
                int i = gameObject.Get<RenderData>();
                System.Drawing.Image image = (System.Drawing.Image)renderables[i].GetRender(ticks).Clone();
                Transform transform = transforms[gameObject.Get<Transform>()];

                float rad = transform.RotRad;
                double w = Math.Abs((Math.Cos(rad) * image.Width) + Math.Abs((Math.Sin(rad) * image.Height)));
                double h = Math.Abs((Math.Sin(rad) * image.Width) + Math.Abs((Math.Cos(rad) * image.Height)));
                Bitmap bmp = new Bitmap((int)Math.Ceiling(w), (int)Math.Ceiling(h));
                Graphics G = Graphics.FromImage(image);
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                G.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                System.Drawing.Drawing2D.Matrix m = G.Transform;// new System.Drawing.Drawing2D.Matrix();

                m.Scale(transform.Scale, transform.Scale, _append);
                m.RotateAt(transform.RotDeg, new PointF((float)(image.Width / 2), (float)(image.Height / 2)), _append);
                G.Transform = m;
                G.DrawImage(image, 0, 0);
                //Buffer.Graphics.DrawImage(image, transform.X - camX, transform.Y - camY);
                //Buffer.Render(G);
                G.Dispose();
                Debugger.DrawBoundingBox(gameObject);
            }
            Glfw.SwapBuffers(Display.Window);
        }
        internal static void Update(long ticks)
        {
            ClearScreen();
            // Draw images to internal buffer
            ScreenBuffer(ticks);
            // Draw images to the screen
            //Buffer.Render();
        }
    }
}
