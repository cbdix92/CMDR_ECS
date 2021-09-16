using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private float _x, _y, _z;
        public float X
        {
            get => _x;
            internal set
            {
                Receive();
                _x = value;
                Send();
            }
        }
        public float Y
        {
            get => _y;
            internal set
            {
                Receive();
                _y = value;
                Send();
            }
        }
        public float Z
        {
            get => _z;
            internal set
            {
                Receive();
                _z = value;
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
        private float _scale;
        public float Scale
        {
            get => _scale+1;
            set
            {
                Receive();
                _scale = value;
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
