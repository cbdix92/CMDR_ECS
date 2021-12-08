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

        private static readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
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
            GL.VertexAttribPointer(0, 3, GL.FLOAT, false, (void*)0);
            GL.EnableVertexAttribArray(0);


            // Shaders
            Shader shader = ShaderManager.Load(@"Shaders\Vert.vert", @"Shaders\Frag.frag");
            GL.ClearColor(Color.BabyBlue);
            while (!Glfw.WindowShouldClose(Window))
            {
                GL.Clear(GL.COLOR_BUFFER_BIT | GL.DEPTH_BUFFER_BIT);

                shader.Use();
                shader.SetUniformVec4("Color", Color.Caramel);
                GL.BindVertexArray(VAO);
                GL.DrawArrays(GL.TRIANGLES, 0, 3);
                Glfw.SwapBuffers(Window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }
    }
}
