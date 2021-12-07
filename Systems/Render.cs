using System;
using GLFW;
using OpenGL;

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
			
			GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
			GL.BufferData(GL.ARRAY_BUFFER, sizeof(float)*Vertices.Length, Vertices, GL.STATIC_DRAW);
			
			GL.BindVertexArray(VAO);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 4, typeof(float), false, (void*)0);
			
			// Unbind VAO and VBO
			GL.BindBuffer(GL.ARRAY_BUFFER, 0);
			GL.BindVertexArray(0);
		}
		
        internal static void ClearScreen()
        {
			GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);
        }
		
        internal static void ScreenBuffer(long ticks)
        {

            Debugger.Draw(ticks);

			Matrix4 projection = Camera.Projection;

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();
            SGameObject[] gameObjects = Camera.GetRenderable(Scene.GameObjects.Get(), transforms);

            foreach(SGameObject gameObject in gameObjects)
            {
                
				Transform transform = transforms[gameObject.Get<Transform>()];
				RenderData renderData = renderables[gameObject.Get<RenderData>()];
                
				Texture texture = renderData.GetRender(ticks);
                
				
				Matrix4 model = transform.GenerateModelMatrix();
				
				renderData.Shader.Use();
				renderData.Shader.SetUniformMatrix4("model", model);
				renderData.Shader.SetUniformMatrix4("projection", projection);
				renderData.Shader.SetUniformVec4("color", renderData.Color);
				
				GL.ActiveTexture(GL.TEXTURE0);
				texture.Bind();
				
				GL.BindVertexArray(VAO);
				GL.DrawArrays(GL.TRIANGLES, 0, 6);
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
