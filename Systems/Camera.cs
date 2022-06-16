using System;
using CMDR.Components;

namespace CMDR
{
    public static class Camera
    {
        #region PUBLIC_MEMBERS

        public static Vector3 Pos { get => _pos; }

        public static Vector3 Velocity { get => _velocity; }

        public static Vector3 Rotation { get => _rot; }

        public static Vector2UI Size { get => _size; }

        public static uint FOV
        {
            get => _fov;
            set
            {
                _fov = value;
                if (ProjectionType != ProjectionTypes.Orthographic)
                    ChangeProjectionState = true;
            }
        }

        /// <summary>
        /// Determine if the Camera View Matrix needs to be recalculated.
        /// </summary>
        public static bool ChangeViewState { get; private set; }

        /// <summary>
        /// Determine if the Camera Projection Matrix needs to be recalculated.
        /// </summary>
        public static bool ChangeProjectionState { get; private set; }

        public static ProjectionTypes ProjectionType
        {
            get => _projectionType;
            set
            {
                _projectionType = value;
                ChangeProjectionState = true;
            }
        }

        public static Matrix4 View
        {
            get
            {
                if (ChangeViewState)
                    CreateView();
                return _view;
            }
        }

        public static Matrix4 Projection
        {
            get
            {
                if (ChangeProjectionState)
                    CreateProjection();
                return _projection;
            }
        }

        public static float Left { get => -Width / 2; }
        
        public static float Right { get => Width / 2; }
        
        public static float Top { get => -Height / 2; }
        
        public static float Bottom { get => Height / 2; }

        public static readonly float Near = 1f;

        public static readonly float Far = 1000f;

        #region ROTATION

        public static float Xrot
        {
            get => _rot.X / 0.01745329f;
            set
            {
                float rads = value * 0.01745329f;
                // Check if camera is beyond 90 degrees
                if (MathHelper.Abs(rads) < 1.5708f)
                    _rot.X = rads;

                ChangeViewState = true;
            }
        }
        public static float Yrot
        {
            get => _rot.Y / 0.01745329f;
            set
            {
                _rot.Y = value * 0.01745329f;
                ChangeViewState = true;
            }
        }
        public static float Zrot
        {
            get => _rot.Z / 0.01745329f;
            set
            {
                _rot.Z = value * 0.01745329f;
                ChangeViewState = true;
            }
        }

        #endregion

        #region SIZE
        public static uint Width
        {
            get => _size.X;
            set
            {
                _size.X = value;
                ChangeViewState = true;
            }
        }

        public static uint Height
        {
            get => _size.Y;
            set
            {
                _size.Y = value;
                ChangeViewState = true;
            }
        }
        #endregion

        #region VELOCITY
        public static float Xvel
        {
            get => _velocity.X;
            set
            {
                _velocity.X = value;
            }
        }

        public static float Yvel
        {
            get => _velocity.Y;
            set
            {
                _velocity.Y = value;
            }
        }

        public static float Zvel
        {
            get => _velocity.Z;
            set
            {
                _velocity.Z = value;
            }
        }
        #endregion

        #region POSITION
        public static float X
        {
            get => _pos.X;
            set
            {
                _pos.X = value;
                ChangeViewState = true; ;
            }
        }

        public static float Y
        {
            get => _pos.Y;
            set
            {
                _pos.Y = value;
                ChangeViewState = true;
            }
        }

        public static float Z
        {
            get => _pos.Z;
            set
            {
                _pos.Z = value;
                ChangeViewState = true;
            }
        }
        #endregion

        #endregion

        #region PRIVATE_MEMBERS

        private static Vector2UI _size;

        private static Vector3 _velocity;

        private static Vector3 _pos;

        private static Vector3 _rot;

        private static float _zoom = 1f;

        private static uint _fov;

        private static Matrix4 _view;

        private static Matrix4 _projection;

        private static ProjectionTypes _projectionType;

        #endregion

        #region PUBLIC_METHODS

        public static Vector3 Forward
		{
			get
			{
                Vector3 q = Quaternion.RotatePoint(_rot, Vector3.Forward);
                q.X = -q.X;
                q.Y = -q.Y;
                return q;
			}
		}


        public static float Zoom
        {
            get => _zoom;
            set
            {
                if(value != _zoom & value != 0)
                {
                    _zoom = value;
                    ChangeViewState = true;
                }
            }
        }

        
		
        public static void ResetRotation()
        {
            _rot = new Vector3();
            ChangeViewState = true;
        }
		public static void MoveCamera(float distance)
		{
            Vector3 direction = Forward * distance;
            
            _pos += direction;

            ChangeViewState = true;
		}

        public static void StrafeCamera(float distance)
        {
            Vector3 direction = Vector3.Normalize(Vector3.Cross(Forward, Vector3.Up)) * distance;

            _pos += direction;

            ChangeViewState = true;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// Called by the renderer if ChangeSate is set true to calculate the new View Matrix.
        /// </summary>
        private static void CreateView()
		{
            Matrix4 identity = Matrix4.Identity;

            Matrix4 rotX = Matrix4.CreateRotationX(_rot.X);
            Matrix4 rotY = Matrix4.CreateRotationY(_rot.Y);
            Matrix4 rotation = rotX * rotY;


            Matrix4 translation = Matrix4.CreateTranslation(_pos);
            _view = rotation * translation * identity;


            ChangeViewState = false;
        }

        private static void CreateProjection()
        {
            switch(ProjectionType)
            {
                case ProjectionTypes.Orthographic:
                    CreateOrthographicProjection();
                    break;
                case ProjectionTypes.Perspective:
                    CreatePerspectiveProjection();
                    break;
                case ProjectionTypes.PersoectiveFOV:
                    CreatePerspectiveFovProjection();
                    break;
            }
            ChangeProjectionState = false;
        }

        private static void CreateOrthographicProjection()
        {
            _projection = Matrix4.CreateOrthographic(Top, Bottom, Left, Right, Far, Near);
        }

        private static void CreatePerspectiveProjection()
        {
            _projection = Matrix4.CreatePerspective(Top, Bottom, Left, Right, Far, Near);
        }

        private static void CreatePerspectiveFovProjection()
        {
            if (Height == 0)
                _projection = default(Matrix4);
            _projection = Matrix4.CreatePerspectiveFOV(_fov, Width / Height, Far, Near);
        }

        #endregion

        #region INTERNAL_METHODS

        /// <summary>
        /// Check if a Transform is within view of the camera by performing bounding box collision test.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        internal static bool CameraRectCheck(Transform transform)
        {
            return transform.X - X < Width && transform.Y - Y < Height;
        }

        /// <summary>
        /// Returns renderable SGameObjects in view of the camera.
        /// </summary>
        /// <param name="transforms"> Array of transforms that are checked.</param>
        /// <returns></returns>
        internal static SGameObject[] GetRenderable(SGameObject[] gameObjects, Transform[] transforms)
        {
            // TODO ... LINQ is very slow. Do something better.
            //IEnumerable<SGameObject> _ = SceneManager.ActiveScene.GameObjects.Get().AsQueryable().Where(gameObject => gameObject.Contains<RenderData>()
            //&&
            //CameraRectCheck(transforms[gameObject.Get<Transform>()]));
			
			SGameObject[] result = new SGameObject[gameObjects.Length];
			int count = 0;
			for(int i = 0; i < gameObjects.Length; i++)
			{
				SGameObject g = gameObjects[i];
				if(g.Contains<RenderData>() && CameraRectCheck(transforms[g.Get<Transform>()]))
				{
					result[count++] = gameObjects[i];
				}
			}
			Array.Resize<SGameObject>(ref result, count);

            return result;

        }

        #endregion
    }
}
