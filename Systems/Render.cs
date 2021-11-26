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
		
        internal static void ClearScreen()
        {
            GL.Clear(BUFFER_MASK.COLOR_BUFFER_BIT | BUFFER_MASK.DEPTH_BUFFER_BIT);
        }
		
        internal static void ScreenBuffer(long ticks)
        {

            Debugger.Draw(ticks);

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();
            SGameObject[] gameObjects = Camera.GetRenderable(Scene.GameObjects.Get(), transforms);

            foreach(SGameObject gameObject in gameObjects)
            {

                int tex_ID = gameObject.Get<RenderData>();
                Texture texture = renderables[tex_ID].GetRender(ticks);
                Transform transform = transforms[gameObject.Get<Transform>()];



                Debugger.DrawBoundingBox(gameObject);
            }
        }
        internal static void Update(long ticks)
        {
            ClearScreen();
            // Draw images to internal buffer
            ScreenBuffer(ticks);
            // Draw images to the screen
            Glfw.SwapBuffers(Display.Window);
        }
    }
}
