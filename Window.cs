using System;
using CMDR.Native;

namespace CMDR
{
	
    public class Window
    {
        #region PUBLIC_MEMBERS

        public string Title;

		/// <summary> WNDCLASSEXW.style bitfield settings </summary>
		public uint EXClassStyle { get; private set; }
		
		public uint ClassStyle { get; private set; }
		
		public IntPtr HWND { get; internal set; }
		
		public int StartingPosX;
		
		public int StartingPosY;

		public uint Width
		{
			get => _size.X;
			set
			{
				(Camera.Width, _size.X) = (value, value);
			}
		}

		public uint Height
		{
			get => _size.Y;
			set
			{
				(Camera.Height, _size.Y) = (value, value);
			}
		}

		#endregion

		#region INTERNAL_MEMBERS


		#endregion

		#region PRIVATE_MEMBERS

		private Vector2UI _size;

		#endregion

		#region CONTRUCTORS

		public Window(uint width, uint height, string title)
		{
            (Width, Height, Title) = (width, height, title);
		}

        #endregion

        #region PUBLIC_METHODS
        public void SetFlag(uint flag)
		{
			// TODO ... 
            // Set window bit fields here.
		}

		public void Create()
		{
			Win.CreateWindow(this);
		}

		public void Close()
        {
			Win.PostMessageW(HWND, (uint)WM.CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

		#region VIRTUAL_METHODS
		
		public virtual void OnMove()
        {

        }

		public virtual void OnSize()
        {

        }

		public virtual void OnClose()
        {

        }

        #endregion

        #endregion


    }
}
