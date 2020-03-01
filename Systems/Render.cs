using System;
using System.Drawing;
using System.Windows.Forms;

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

        internal static void ScreenBuffer()
        {
            Scene scene = Scene;
            Transform[] transforms = scene.Components.Get<Transform>();
            RenderData[] renderables = scene.Components.Get<RenderData>();

            foreach(SGameObject gameObject in Camera.GetRenderable())
            {
                Image image = renderables[gameObject.Get<RenderData>()].Data;
                Transform transform = transforms[gameObject.Get<Transform>()];
                Buffer.Graphics.DrawImage(image, transform.X - Camera.X, transform.Y - Camera.Y);
            }
        }
        internal static void SetDisplay(Display display)
        {
            Display = display;
            Buffer_Context = BufferedGraphicsManager.Current;
            Buffer = Buffer_Context.Allocate(Display.CreateGraphics(), new Rectangle(0, 0, Display.Width, Display.Height));
        }
        internal static void Draw()
        {
            Buffer.Render();
        }
    }
}
