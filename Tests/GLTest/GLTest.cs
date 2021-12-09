using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMDR;
using OpenGL;
using GLFW;
using CMDR.Components;
using CMDR.Systems;

namespace GLTest
{
    class GLtest
    {
        public static Window Window;

        private static float[] _vertices = new float[] { 
			// pos      // tex
			0.0f, 1.0f, 0.0f, 1.0f,
			1.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 0.0f, 
		
			0.0f, 1.0f, 0.0f, 1.0f,
			1.0f, 1.0f, 1.0f, 1.0f,
			1.0f, 0.0f, 1.0f, 0.0f
			};

    public static uint VAO;
        public static uint VBO;
        static unsafe void Main(string[] args)
        {

            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);

            Window = Glfw.CreateWindow(800, 600, "Test Title", Monitor.None, Window.None);

            Glfw.MakeContextCurrent(Window);

            GL.Init();

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
            GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertices.Length, _vertices, GL.STATIC_DRAW);

            GL.BindVertexArray(VAO);
            GL.VertexAttribPointer(0, 4, GL.FLOAT, false, (void*)0);
            GL.EnableVertexAttribArray(0);


            // Shaders
            //Shader shader = ShaderManager.Load(@"Shaders\Vert.vert", @"Shaders\Frag.frag");
            ShaderManager.Init();
            Shader shader = ShaderManager.DefaultShader();
            GL.ClearColor(Color.BabyBlue);

            float[] pixels = GenPixels(50, 50);
            Texture texture = new Texture(pixels, 50, 50, 4);
            Camera.Width = 800;
            Camera.Height = 600;
            Matrix4 projection = Camera.Projection;

            Scene scene = new Scene();
            SGameObject gameObject = scene.GenerateGameObject();
            Transform transform = scene.Generate<Transform>();
            RenderData renderData = scene.Generate<RenderData>();
            renderData.ImgData = texture;
            transform.Teleport(50, 50);
            gameObject.Use(transform);
            gameObject.Use(renderData);

            Matrix4 model = transform.GenerateModelMatrix();

            while (!Glfw.WindowShouldClose(Window))
            {
                GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);

                shader.Use();
                shader.SetUniformMatrix4("model", false, model);
                shader.SetUniformMatrix4("projection", false, projection);
                shader.SetUniformVec4("color", Color.Caramel);
                
                GL.ActiveTexture(GL.TEXTURE0);
                texture.Bind();

                GL.BindVertexArray(VAO);
                GL.DrawArrays(GL.TRIANGLES, 0, 3);
                Glfw.SwapBuffers(Window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }

        public static float[] GenPixels(int width, int height)
        {
            Color color = Color.Red;
            int size = width * height * 4;
            float[] pixels = new float[size];
            for(int i = 0; i < size; i+=4)
            {
                pixels[i] = color.R;
                pixels[i+1] = color.G;
                pixels[i+2] = color.B;
                pixels[i+3] = color.A;
            }
            return pixels;
        }
    }
}
