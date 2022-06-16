using System;
using CMDR.Native;

namespace CMDR
{
	
    public class Window
    {

		public string Title;

		/// <summary> WNDCLASSEXW.style bitfield settings </summary>
		public uint EXClassStyle { get; private set; }
		
		public uint ClassStyle { get; private set; }
		
		public IntPtr HWND { get; internal set; }
		
		public int StartingPosX;
		
		public int StartingPosY;

		private Vector2UI _size;

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
		
        public Window(uint width, uint height, string title)
		{
            (Width, Height, Title) = (width, height, title);
		}

		public void SetFlag(uint flag)
		{
			// TODO ... 
            // Set window bit fields here.
		}

		public void Create()
		{
			Win.CreateWindow(this);
		}

		public virtual void OnMove()
        {

        }
		public virtual void OnSize()
        {

        }
		
		
    }
}
