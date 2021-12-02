using System;

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
            get
            {
                return _static == 1 ? true : false;
            }
            set
            {
                Receive();
                _static = value == true ? 0 : 1;
                Send();
            }
        }

        #region POSITION_PROPERTIES
		public Vector3 Pos;
		
        public float X
        {
            get => Pos.X;
            internal set
            {
                Receive();
                Pos.X = value;
                Send();
            }
        }
        public float Y
        {
            get => Pos.Y;
            internal set
            {
                Receive();
                Pos.Y = value;
                Send();
            }
        }
        public float Z
        {
            get => Pos.Z;
            internal set
            {
                Receive();
                Pos.Z = value;
                Send();
            }
        }
        #endregion

        #region VELOCITY_PROPERTIES
        private float _xvel, _yvel;
        public float Xvel
        {
            get => _xvel;
            set
            {
                Receive();
                _xvel = value * _static;
                Send();
            }
        }
        public float Yvel
        {
            get => _yvel;
            set
            {
                Receive();
                _yvel = value * _static;
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
            get => _rotD / 180.0F * (float)Math.PI;
        }

        #endregion

        #region SCALE_PROPERTIES
		
		public Vector3 Scale;
		
		public float Xscale
		{
			get => Scale.X;
			set
			{
				Receive();
				Scale.X = value;
				Send();
			}
		}
		
		public float Yscale
		{
			get => Scale.Y;
			set
			{
				Receive();
				Scale.Y = value;
				Send();
			}
		}
		
		public float Zscale
		{
			get => Scale.Z;
			set
			{
				Receive();
				Scale.Z = value;
				Send();
			}
		}
        #endregion


        public void Move(float x, float y)
        {
            Receive();
            X += x;
            Y += y;
            Send();   
        }
        public void Teleport(float x, float y)
        {
            Receive();
            X = x;
            Y = y;
            Send();
        }
		
		public Matrix4 GenerateModel()
		{
			// Generate the model matrix with appropiate Scale, Rotate, Translate. In that order.
			throw new NotImplementedException("Transform.GenerateMatrix");	
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
