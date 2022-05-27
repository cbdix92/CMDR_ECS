using System;
using System.IO;
using OpenGL;

namespace CMDR.Components
{
    public struct RenderData : IComponent<RenderData>
    {
        #region IComponent
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion


        // Specifies if the this RenderData is to return a static image or animation
        public bool Static { get; set; }

        public Mesh Mesh;

        public Shader Shader;

        public Color Color;

        public Texture ImgData { get; set; }

        internal Animation2D Animator;

        public string currentAnimation;

        public void Init()
        {
            Shader = ShaderManager.DefaultShader();
            Color = Color.Gray;
            Send();
        }


        public void LoadMesh(string path)
        {
            Receive();
            Mesh = MeshManager.Load(path);
            Send();
        }

        public void Draw()
        {
            GL.BindVertexArray(Mesh.VAO);
            GL.DrawArrays(GL.TRIANGLES, 0, Mesh.NumVertices);
            GL.BindVertexArray(0);
        }

        #region DEPRECATED 2D CODE
        public void FromFile(string src)
        {
            Receive();
            ImgData = Decoder.LoadFromFile(src);
            Send();
        }

        public unsafe void CreateAnimation(string name, string[] paths, float stepSize)
        {
            Receive();
			
			// Is this needed with a struct?
            if (Animator.Equals(default(Animation2D)))
                Animator = new Animation2D();
            
            currentAnimation = name;

            Texture[] _ = new Texture[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                try
                {
                    _[i] = Decoder.LoadFromFile(paths[i]);
                }
                catch (FileNotFoundException)
                {
                    throw new FileNotFoundException($"'{paths[i]}', Animation");
                }
            }

            Animator.InsertAnimation(name, _, stepSize);

            Send();
        }


        #endregion
        public Texture GetRender(long ticks)
        {
            return Static ? ImgData : Animator.Get(ticks, currentAnimation);
        }
        public void Receive()
        {
            this = Scene.Get<RenderData>(ID);
        }
        public void Send()
        {
            Scene.Update<RenderData>(this);
        }
    }
}