using System;
using System.Linq;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    public static class Camera
    {


        private static Vector2 _size;
        private static Vector3 _velocity;
        private static Vector3 _pos;
        private static float _zoom = 1f;
        private static Matrix4 _view;

        #region SIZE
        public static float Width
        {
            get => _size.X;
            set
            {
                _size.X = value * 1;
                ChangeState = true;
            }
        }

        public static float Height
        {
            get => _size.Y;
            set
            {
                _size.Y = value * 1;
                ChangeState = true;
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
                ChangeState = true; ;
            }
        }

        public static float Y
        {
            get => _pos.Y;
            set
            {
                _pos.Y = value;
                ChangeState = true;
            }
        }

        public static float Z
        {
            get => _pos.Z;
            set
            {
                _pos.Z = value;
                ChangeState = true;
            }
        }
        #endregion


        public static float Zoom
        {
            get => _zoom;
            set
            {
                if(value != _zoom & value != 0)
                {
                    _zoom = value;
                    ChangeState = true;
                }
            }
        }

        public static Matrix4 View
        {
            get
            {
                if (ChangeState)
                    CreateView();
                return _view;
            }
        }

        /// <summary>
        /// Determine if the View matrix needs to be recalculated
        /// </summary>
        public static bool ChangeState { get; private set; }

        /// <summary>
        /// Called by the renderer if ChangeSate is set true to calculate the new View Matrix.
        /// </summary>
		internal static void CreateView()
		{
            Matrix4 identity = Matrix4.Identity;

            _view = identity * Matrix4.CreateScale(new Vector3(Zoom)) * Matrix4.CreateTranslation(_pos);
            ChangeState = false;
        }
		
        /// <summary>
        /// Check if a Transform is within view of the camera by performing bounding box collision test.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static bool CameraRectCheck(Transform transform)
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
    }
}
