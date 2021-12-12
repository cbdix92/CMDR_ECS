using System;
using OpenGL;

namespace CMDR.Components
{
    public struct Transform : IComponent<Transform>
    {
        #region IComponent
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        private int _static;
        public bool Static
        {
            get => _static == 1;
            set
            {
                Receive();
                _static = value == true ? 0 : 1;
                Send();
            }
        }

        #region POSITION_PROPERTIES
		private Vector3 _pos;
		
        public float X
        {
            get => _pos.X;
            set
            {
                Receive();
                _pos.X = value;
                Send();
            }
        }
        public float Y
        {
            get => _pos.Y;
            internal set
            {
                Receive();
                _pos.Y = value;
                Send();
            }
        }
        public float Z
        {
            get => _pos.Z;
            internal set
            {
                Receive();
                _pos.Z = value;
                Send();
            }
        }
        #endregion

        #region VELOCITY_PROPERTIES
        private Vector2 _vel;
        public float Xvel
        {
            get => _vel.X;
            set
            {
                Receive();
                _vel.X = value * _static;
                Send();
            }
        }
        public float Yvel
        {
            get => _vel.Y;
            set
            {
                Receive();
                _vel.Y = value * _static;
                Send();
            }
        }
        #endregion

        #region ROTATION_PROPERTIES
        private float _rotD;
        public float RotDeg
        {
            get => _rotD;
            set
            {
                Receive();
                _rotD = value;
                Send();
            }
        }
        public float RotRad
        {
			get => _rotD * 0.01745329f;
        }

        #endregion

        #region SCALE_PROPERTIES
		
		private Vector3 _scale;
		
		public float Xscale
		{
			get => _scale.X;
			set
			{
				Receive();
				_scale.X = value;
				Send();
			}
		}
		
		public float Yscale
		{
			get => _scale.Y;
			set
			{
				Receive();
				_scale.Y = value;
				Send();
			}
		}
		
		public float Zscale
		{
			get => _scale.Z;
			set
			{
				Receive();
				_scale.Z = value;
				Send();
			}
		}
        #endregion


        public void Init()
        {
            Xscale = 1;
            Yscale = 1;
            Zscale = 1;
        }
        public void Move(float x, float y)
        {
            X += x;
            Y += y;   
        }
        public void Teleport(float x, float y)
        {
            X = x;
            Y = y;
        }
        public void Scale(float n)
        {
            (Xscale, Yscale, Zscale) = (n, n, n);
        }
		
		public Matrix4 GenerateModelMatrix(Texture texture)
		{
			// Generate the model matrix with appropiate Scale, Rotate, Translate. In that order.
			Matrix4 result = Matrix4.CreateScale(Xscale*texture.Width, Yscale*texture.Height, 1f) * Matrix4.CreateTranslation(_pos);
            return result * Matrix4.CreateRotationZ(RotRad);	
		}

        public void Receive()
        {
            this = Scene.Get<Transform>(ID);
        }
        public void Send()
        {
            Scene.Update<Transform>(this);
        }
    }
    
}
