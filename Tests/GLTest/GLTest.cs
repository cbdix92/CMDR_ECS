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

        public static Display Display;

        #region VERTS

        private static float[] cube = new float[]
        {
            -1.0f,-1.0f,-1.0f,
            -1.0f,-1.0f, 1.0f,
            -1.0f, 1.0f, 1.0f,
            1.0f, 1.0f,-1.0f, 
            -1.0f,-1.0f,-1.0f,
            -1.0f, 1.0f,-1.0f,
            1.0f,-1.0f, 1.0f,
            -1.0f,-1.0f,-1.0f,
            1.0f,-1.0f,-1.0f,
            1.0f, 1.0f,-1.0f,
            1.0f,-1.0f,-1.0f,
            -1.0f,-1.0f,-1.0f,
            -1.0f,-1.0f,-1.0f,
            -1.0f, 1.0f, 1.0f,
            -1.0f, 1.0f,-1.0f,
            1.0f,-1.0f, 1.0f,
            -1.0f,-1.0f, 1.0f,
            -1.0f,-1.0f,-1.0f,
            -1.0f, 1.0f, 1.0f,
            -1.0f,-1.0f, 1.0f,
            1.0f,-1.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            1.0f,-1.0f,-1.0f,
            1.0f, 1.0f,-1.0f,
            1.0f,-1.0f,-1.0f,
            1.0f, 1.0f, 1.0f,
            1.0f,-1.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            1.0f, 1.0f,-1.0f,
            -1.0f, 1.0f,-1.0f,
            1.0f, 1.0f, 1.0f,
            -1.0f, 1.0f,-1.0f,
            -1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            -1.0f, 1.0f, 1.0f,
            1.0f,-1.0f, 1.0f
        };
        #endregion
		
        public static uint VAO;
        public static uint VBO;
		
        static unsafe void Main(string[] args)
        {
            Display = new Display(800, 600, "Test title");

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertFull.Length, _vertFull, GL.STATIC_DRAW);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertTopLeft.Length, _vertTopLeft, GL.STATIC_DRAW);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertTopRight.Length, _vertTopRight, GL.STATIC_DRAW);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertBottomLeft.Length, _vertBottomLeft, GL.STATIC_DRAW);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertBottomRight.Length, _vertBottomRight, GL.STATIC_DRAW);
            //GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _testVertices.Length, _testVertices, GL.STATIC_DRAW);
            GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * cube.Length, cube, GL.STATIC_DRAW);

            GL.BindVertexArray(VAO);
            GL.VertexAttribPointer(0, 3, GL.FLOAT, false, 0, (void*)0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);


            // Shaders
            //Shader shader = ShaderManager.Load(@"Shaders\Vert.vert", @"Shaders\Frag.frag");
            Shader shader = ShaderManager.DefaultShader();

            int texWidth = 50;
            int texHeight = 50;

            float[] pixels = GenPixels(texWidth, texHeight);
            Texture texture = new Texture(pixels, texWidth, texHeight, 4);


            Scene scene = new Scene();
            SGameObject gameObject = scene.GenerateGameObject();
            Transform transform = scene.Generate<Transform>();
            RenderData renderData = scene.Generate<RenderData>();
            renderData.ImgData = texture;
            transform.Teleport(0, 0, 0);
            transform.Scale(3f);
            gameObject.Use(transform);
            gameObject.Use(renderData);
            //Camera.Z = -100f;
			
            GameLoop.Time.Start();
            float counter = 0;
            Glfw.SetKeyCallback(Display.Window, keyinput);

            //Mesh mesh = MeshManager.Load(@"Assets\FinalBaseMesh.OBJ");
            Mesh mesh = MeshManager.Load(@"Assets\tree_obj.OBJ");
            GL.BindVertexArray(mesh.VAO);

            GL.Enable(GL.DEPTH_TEST);
            while (!Glfw.WindowShouldClose(Display.Window))
            {
                GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);
                
                if (counter > 10)
                {
                    transform.Zrot += 2f;
                    transform.Xrot += 1f;
                    transform.Yrot += 0.5f;
                    //Console.WriteLine(transform.Zradians);
                    //Camera.Z;
                    //transform.X = transform.X == 1f ? 0 : 1f;
                    //renderData.Color = new Color(MathHelper.Cos(GameLoop.GameTime), MathHelper.Tan(GameLoop.GameTime), MathHelper.Sin(GameLoop.GameTime), 1f);
                    //transform.Teleport(MathHelper.Cos(GameLoop.GameTime), -MathHelper.Sin(GameLoop.GameTime));
                    //transform.RotDeg = MathHelper.Sin(GameLoop.GameTime);
                    //Camera.Zoom = MathHelper.Sin(GameLoop.GameTime);
                    counter = 0;
                }
                //counter++;
                
                shader.Use();
                shader.SetUniformMatrix4("model", false, transform.Model);
                shader.SetUniformMatrix4("view", false, Camera.View);
                shader.SetUniformMatrix4("projection", false, Display.Projection);
                shader.SetUniformVec4("color", renderData.Color);

                //GL.ActiveTexture(GL.TEXTURE0);
                //texture.Bind();

                // Cube Draw
                GL.BindVertexArray(VAO);
                GL.DrawArrays(GL.TRIANGLES, 0, 36);
                GL.BindVertexArray(0);
                GL.BindVertexArray(mesh.VAO);
                
                // mesh Draw
                //GL.DrawArrays(GL.TRIANGLES, 0, mesh.NumVertices);
                GL.DrawElements(GL.TRIANGLES, mesh.NumVertices, (void*)0);
                Glfw.SwapBuffers(Display.Window);
                GL.BindVertexArray(0);
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
        public static void keyinput(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            float speed = 1f;

            switch(key)
            {
                case Keys.W:
                    Camera.Z += speed;
                    break;
                case Keys.S:
                    Camera.Z += -speed;
                    break;
                case Keys.A:
                    Camera.X += speed;
                    break;
                case Keys.D:
                    Camera.X += -speed;
                    break;
                case Keys.Q:
                    Camera.Y += -speed;
                    break;
                case Keys.E:
                    Camera.Y += speed;
                    break;
                case Keys.Z:
                    Camera.Yrot += speed/2;
                    break;
                case Keys.C:
                    Camera.Yrot += -speed/2;
                    break;
                case Keys.T:
                    Camera.Zoom += speed;
                    break;
                case Keys.Y:
                    Camera.Zoom += -speed;
                    break;
            }
            //Console.WriteLine(Camera.Z);
        }
    }
}
