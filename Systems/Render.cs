using System;
using GLFW;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

using CMDR.Components;

namespace CMDR.Systems
{
    internal static class Render
    {
        internal static Scene Scene { get => SceneManager.ActiveScene; }

        public static byte ZDepth;


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
			GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            
			
			//GL.BindVertexArray(VAO);

            foreach(SGameObject gameObject in Camera.GetRenderable(transforms))
            {
                int tex_ID = gameObject.Get<RenderData>();
                Texture texture = renderables[tex_ID].GetRender(ticks);
                Transform transform = transforms[gameObject.Get<Transform>()];

                // Get width and height of rotated texture
                float rad = transform.RotRad;
                double w = Math.Abs((Math.Cos(rad) * texture.Width) + Math.Abs((Math.Sin(rad) * texture.Height)));
                double h = Math.Abs((Math.Sin(rad) * texture.Width) + Math.Abs((Math.Cos(rad) * texture.Height)));

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
