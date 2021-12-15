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
        public static float Width = 35;
        public static float Height = 35;

        public static Display Display;

        private static float[] _vertices = new float[] 
        { 
			// X     Y
			-1.0f,  1.0f, 
             1.0f,  1.0f,
            -1.0f, -1.0f,

             1.0f,  1.0f,
             1.0f, -1.0f, 
            -1.0f, -1.0f
        };

        private static float[] _verts = new float[]
        {
            -1.0f,  1.0f,
            1.0f,  1.0f,
            -1.0f, -1.0f,

            -1.0f,  -1.0f,
            1.0f, 1.0f,
            1.0f, -1.0f
        };

        public static uint VAO;
        public static uint VBO;
        static unsafe void Main(string[] args)
        {
            Display = new Display(800, 600, "Test title");

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertices.Length, _vertices, GL.STATIC_DRAW);
            GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _verts.Length, _verts, GL.STATIC_DRAW);

            GL.BindVertexArray(VAO);
            GL.VertexAttribPointer(0, 2, GL.FLOAT, false, (void*)0);
            GL.EnableVertexAttribArray(0);


            // Shaders
            Shader shader = ShaderManager.Load(@"Shaders\Vert.vert", @"Shaders\Frag.frag");
            ShaderManager.Init();
            //Shader shader = ShaderManager.DefaultShader();
            GL.ClearColor(Color.BabyBlue);

            int texWidth = 50;
            int texHeight = 50;

            float[] pixels = GenPixels(texWidth, texHeight);
            Texture texture = new Texture(pixels, texWidth, texHeight, 4);
            
            Camera.Width = 800;
            Camera.Height = 600;


            Scene scene = new Scene();
            SGameObject gameObject = scene.GenerateGameObject();
            Transform transform = scene.Generate<Transform>();
            RenderData renderData = scene.Generate<RenderData>();
            renderData.ImgData = texture;
            transform.Teleport(2, 2);
            transform.Scale(1f);
            gameObject.Use(transform);
            gameObject.Use(renderData);

            Matrix4 model = transform.GenerateModelMatrix(texture);
            GameLoop.Time.Start();
            float counter = 0;
            while (!Glfw.WindowShouldClose(Display.Window))
            {
                GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);
                
                if (counter > 60)
                {
                    //transform.RotDeg++;
                    //transform.X = transform.X == 1f ? 0 : 1f;
                    //Console.WriteLine(transform.RotDeg);
                    //renderData.Color = new Color(MathHelper.Cos(GameLoop.GameTime), MathHelper.Tan(GameLoop.GameTime), MathHelper.Sin(GameLoop.GameTime), 1f);
                    //transform.Teleport(MathHelper.Cos(GameLoop.GameTime), -MathHelper.Sin(GameLoop.GameTime));
                    //transform.RotDeg = MathHelper.Sin(GameLoop.GameTime);
                    //Camera.Zoom = MathHelper.Sin(GameLoop.GameTime);
                    model = transform.GenerateModelMatrix(texture);
                    counter = 0;
                }
                counter++;
                shader.Use();
                shader.SetUniformMatrix4("model", false, model);
                shader.SetUniformMatrix4("projection", false, Display.Projection);
                shader.SetUniformVec4("color", renderData.Color);
                shader.SetUniformVec4("pos", new Vector4(transform.X, transform.Y, Camera.Width, Camera.Height));

                //GL.ActiveTexture(GL.TEXTURE0);
                //texture.Bind();

                GL.BindVertexArray(VAO);
                GL.DrawArrays(GL.TRIANGLES, 0, 6);
                GL.BindVertexArray(0);
                Glfw.SwapBuffers(Display.Window);
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
