using System;
using GLFW;
using OpenGL;
using System.Collections.Generic;

using CMDR.Components;

namespace CMDR.Systems
{
    internal static class Render
    {
        internal static Scene Scene { get => SceneManager.ActiveScene; }

        public static byte ZDepth;

        public static Matrix4 ProjectionMatrix = Matrix4.CreatePerspectiveFOV((float)Math.PI / 3, Camera.Width / Camera.Height, 0.1f, 2000);
        public static Matrix4 CameraMatrix = Matrix4.CreateTranslation(Camera.X, Camera.Y, Camera.Z);


        internal static uint VAO;
        internal static uint VBO;
        internal static void ClearScreen()
        {
            //Buffer.Graphics.Clear(Color.Black);
        }
        internal static void ScreenBuffer(long ticks)
        {

            GL.Clear(GL.BUFFER_MASK.COLOR_BUFFER_BIT | GL.BUFFER_MASK.DEPTH_BUFFER_BIT);


            Debugger.Draw(ticks);

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();
            IEnumerable<SGameObject> gameObjects = Camera.GetRenderable(transforms);

            (float camX,float camY) = (Camera.X, Camera.Y);

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
			GL.BindBuffer(GL.BUFFER_BINDING_TARGET.ARRAY_BUFFER, VBO);
            
			
			GL.BindVertexArray(VAO);

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
