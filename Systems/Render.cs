using System;
using System.Windows.Forms;
using System.Drawing;

using CMDR.Components;

namespace CMDR.Systems
{
    internal static class Render
    {
        internal static Form Display;
        internal static BufferedGraphics Buffer;
        internal static BufferedGraphicsContext Buffer_Context;
        internal static Scene Scene { get => SceneManager.ActiveScene; }

        public static byte ZDepth;

        internal static void SetDisplay(Display display)
        {
            Display = display;
            Buffer_Context = BufferedGraphicsManager.Current;
            Buffer = Buffer_Context.Allocate(Display.CreateGraphics(), new Rectangle(0, 0, Display.Width, Display.Height));
        }
        internal static void ClearScreen()
        {
            Buffer.Graphics.Clear(Color.Black);
        }
        internal static void ScreenBuffer(long ticks)
        {
            Debugger.Draw(ticks);

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();

            (float camX,float camY) = (Camera.X, Camera.Y);

            foreach(SGameObject gameObject in Camera.GetRenderable())
            {
                int i = gameObject.Get<RenderData>();
                //Image image = renderables[gameObject.Get<RenderData>()].Data;
                Image image = renderables[i].ImgData;
                Transform transform = transforms[gameObject.Get<Transform>()];
                Buffer.Graphics.DrawImage(image, transform.X - camX, transform.Y - camY);
            }
        }
        internal static void Update(long ticks)
        {
            ClearScreen();
            // Draw images to internal buffer
            ScreenBuffer(ticks);
            // Draw images to the screen
            Buffer.Render();
        }
    }
}
