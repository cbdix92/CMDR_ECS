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

		private Matrix4 _model;
        private int _static;
		private Vector3 _pos;
		private Vector3 _vel;
		private Vector3 _scale;
		private Vector3 _rot;
		
		/// <summary>
        /// Determine if the Model matrix needs to be recalculated
        /// </summary>
        public bool ChangeState { get; private set; }
		
		public Matrix4 Model
		{
			get
			{
				if (ChangeState)
					GenerateModelMatrix();
				return _model;
			}
		}
		
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
		
        public float X
        {
            get => _pos.X;
            set
            {
                Receive();
                _pos.X = value;
				ChangeState = true;
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
				ChangeState = true;
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
				ChangeState = true;
                Send();
            }
        }
        #endregion

        #region ROTATION_PROPERTIES
		// Rotation properties get and receive rotation expressed in degrees.
		// These values are stored in the backig field in radians.
		// When calculating the model matrix, rotation values should be retrived from the backing field or readonly radian properties.
		public float Xrot
		{
			get => _rot.X / 0.01745329f;
			set
			{
				Receive();
				_rot.X = value * 0.01745329f;
				ChangeState = true;
				Send();
			}
		}
		public float Yrot
		{
			get => _rot.Y / 0.01745329f;
			set
			{
				Receive();
				_rot.Y = value * 0.01745329f;
				ChangeState = true;
				Send();
			}
		}
		public float Zrot
		{
			get => _rot.Z / 0.01745329f;
			set
			{
				Receive();
				_rot.Z = value * 0.01745329f;
				ChangeState = true;
				Send();
			}
		}
		public float Xradians { get => _rot.X; }
		public float Yradians { get => _rot.Y; }
		public float Zradians { get => _rot.Z; }
		
        #endregion

        #region SCALE_PROPERTIES
		
		public float Xscale
		{
			get => _scale.X;
			set
			{
				Receive();
				_scale.X = value;
				ChangeState = true;
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
				ChangeState = true;
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
				ChangeState = true;
				Send();
			}
		}
        #endregion
		
		#region VELOCITY_PROPERTIES
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
		public float Zvel
		{
			get => _vel.Z;
			set
			{
				Receive();
				_vel.Z = value * _static;
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
        public void Move(float x, float y, float z)
        {
            X += x;
            Y += y;
			Z += z;
        }
        public void Teleport(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public void Scale(float n)
        {
            (Xscale, Yscale, Zscale) = (n, n, n);
        }
		
		private void GenerateModelMatrix()
		{
			// Generate the model matrix with appropiate Scale, Rotate, Translate. In that order.
			//Matrix4 result = Matrix4.CreateScale(Xscale*texture.Width, Yscale*texture.Height, 1f) * Matrix4.CreateTranslation(_pos);
			
            // For testing cube render
            _model = Matrix4.CreateScale(Xscale, Yscale, 1f) * Matrix4.CreateTranslation(_pos) * Matrix4.CreateRotationZ(Zradians) * Matrix4.CreateRotationX(Xradians) * Matrix4.CreateRotationY(Yradians);
			ChangeState = false;
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
