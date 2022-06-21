using OpenGL;

using CMDR.Components;

namespace CMDR.Systems
{
    internal static class Render
    {
        internal static Scene Scene { get => SceneManager.ActiveScene; }
		
        internal static void ClearScreen()
        {
			GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);
        }
		
        internal static void ScreenBuffer(long ticks)
        {

            Debugger.Draw(ticks);

			Matrix4 projection = Camera.Projection;
			Matrix4 view = Camera.View;
            Vector3 cameraPos = Camera.Pos;

            Transform[] transforms = Scene.Components.Get<Transform>();
            RenderData[] renderables = Scene.Components.Get<RenderData>();
			// Todo: frustum culling
			SGameObject[] gameObjects = Scene.GameObjects.Get();// Camera.GetRenderable(Scene.GameObjects.Get(), transforms);

            foreach(SGameObject gameObject in gameObjects)
            {
                
				Transform transform = transforms[gameObject.Get<Transform>()];
				RenderData renderData = renderables[gameObject.Get<RenderData>()];
                
				//Texture texture = renderData.GetRender(ticks);

                Shader shader = renderData.Shader;

                shader.Use();
                shader.SetUniformMatrix4("model", false, transform.Model);
                shader.SetUniformMatrix4("view", false, view);
                shader.SetUniformMatrix4("projection", false, projection);
                shader.SetUniformVec4("color", renderData.Color);

                shader.SetUniformVec3("lightPos", new Vector3(3f));
                shader.SetUniformVec4("lightColor", Color.White);

                shader.SetUniformVec3("viewPos", cameraPos);


                //GL.ActiveTexture(GL.TEXTURE0);
                //texture.Bind();


                renderData.Draw();
                GL.BindVertexArray(0);
				//GL.BindVertexArray(VAO);
				//GL.DrawArrays(GL.TRIANGLES, 0, 6);
				//GL.BindVertexArray(0);
				
				Debugger.DrawBoundingBox(gameObject);
            }
			
        }
        internal static void Update(long ticks)
        {
            ClearScreen();
            // Draw images to internal buffer
            ScreenBuffer(ticks);
            // Draw images to the screen
            //Glfw.SwapBuffers(Display.Window);
        }
    }
}
