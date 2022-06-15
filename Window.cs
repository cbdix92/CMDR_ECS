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
		
		private int _width;
		
		private int _height;

		public int Width
		{
			get => _width;
			set
			{
				(Camera.Width, _width) = (value, value);
			}
		}

		public int Height
		{
			get => _height;
			set
			{
				(Camera.Height, _height) = (value, value);
			}
		}
		
        public Window(int width, int height, string title)
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
