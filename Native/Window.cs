using System;

namespace CMDR.Native
{
	
    public sealed class Window
    {

		public string Title;

		/// <summary> WNDCLASSEXW.style bitfield settings </summary>
		public uint EXClassStyle { get; private set; }

		public uint ClassStyle { get; private set; }
		public IntPtr HWND { get; private set; }
		public int StartingPosX;
		public int StartingPosY;
		
        private WNDCLASSEXW WNDCLASS;
		private int _width;
		private int height;




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
			(Width, Height, Title) = (width, height, title)
		}

		public void SetFlag(uint flag)
		{
			
		}

		public void Create(this window)
		{
			Win.CreateWindow(window);
		}
		
		
    }
}
