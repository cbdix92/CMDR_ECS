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
		
		internal static float[] Vertices;
		
		internal static uint VBO;
		internal static uint VAO;
		
		
		internal unsafe static void Init()
		{
			Vertices = new float[] { 
			// pos      // tex
			0.0f, 1.0f, 0.0f, 1.0f,
			1.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 0.0f, 
		
			0.0f, 1.0f, 0.0f, 1.0f,
			1.0f, 1.0f, 1.0f, 1.0f,
			1.0f, 0.0f, 1.0f, 0.0f
			};
			
			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();
			
			GL.BindBuffer(GLenum.ARRAY_BUFFER, VBO);
			GL.BufferData(GLenum.ARRAY_BUFFER, sizeof(float)*Vertices.Length, Vertices, GLenum.STATIC_DRAW);
			
			GL.BindVertexArray(VAO);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 4, typeof(float), false, (void*)0);
			
			// Unbind VAO and VBO
			GL.BindBuffer(GLenum.ARRAY_BUFFER, 0);
			GL.BindVertexArray(0);
		}
		
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

				int renderDataID = gameObject.Get<RenderData>();
				int transformID = gameObject.Get<Transform>();
                
				RenderData renderData = renderables[renderDataID];
                
				Texture texture = renderData.GetRender(ticks);
                
				Transform transform = transforms[transformID];
				
				Matrix4 model = transform.GenerateModel();
				
				renderData.Shader.SetUniformMatrix4("model", model);
				renderData.Shader.SetUniformVec4("color", renderData.Color);
				
				GL.ActiveTexture(GLenum.TEXTURE0);
				texture.Bind();
				
				GL.BindVertexArray(VAO);
				GL.DrawArrays(GLenum.TRIANGLES, 0, 6);
				GL.BindVertexArray(0);
				
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
