using System;
using CMDR.Native;

namespace CMDR
{
	
    public class Window
    {
        #region PUBLIC_MEMBERS

        public string Title;

		public uint ClassStyle { get; private set; }

		public uint WindowStyle { get; private set; }
		
		public uint WindowStyleEX { get; private set; }
		
		public IntPtr HWND { get; internal set; }

		public IntPtr HGLRC { get; internal set; }
		
		public int StartingPosX;
		
		public int StartingPosY;

		public int PixelFormatNumber
        {
			get => _pixelFormatNumber;

			internal set => _pixelFormatNumber = value;
        }

		public IntPtr DC
        {
			get => _dc;

			set => _dc = value;
        }

		public int Width
		{
			get => _size.X;

			set => (Camera.Width, _size.X) = (value, value);
		}

		public int Height
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

		private Vector2I _size;

		private uint _classStyleMaster;

		private uint _windowStyleMaster;

		private uint _windowStyleEXMaster;

		private int _pixelFormatNumber;

		private IntPtr _dc;

		#endregion

		#region CONSTRUCTORS
		public Window() : this(800, 600, 0, 0, "CMDR") { }
		public Window(int width, int height, int startingX, int startingY, string title)
		{
            (Width, Height, StartingPosX, StartingPosY, Title) = (width, height, startingX, startingY, title);
			GenerateMasterBitMask();
		}

        #endregion

        #region PUBLIC_METHODS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"> The target bitfield.
		/// Possible values include <see cref="WindowTarget.ClassStyle"/>,
		/// <see cref="WindowTarget.WindowStyle"/> and 
		/// <see cref="WindowTarget.WindowStyleExtended"/>. </param>
		/// <param name="flags"> The flags to set. </param>
        public void SetFlag(WindowTarget target, uint flags)
		{
			switch(target)
			{
				case WindowTarget.ClassStyle:
					ClassStyle |= (flags & ~(_classStyleMaster));
					break;
				case WindowTarget.WindowStyle:
					WindowStyle |= (flags & ~(_windowStyleMaster));
					break;
				case WindowTarget.WindowStyleExtended:
					WindowStyleEX |= (flags & ~(_windowStyleEXMaster));
					break;
			}
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

		#region PRIVATE_METHODS
		
		/// <summary>
		/// Generates the master bit mask for ClassStyle, WindowStyle and WindowStyleEX.
		/// The master bit mask are used to filter out any unused bits that could occur when combining multiple mask.
		/// <see cref="SetFlag(WindowTarget, uint)"/>
		/// </summary>
		private void GenerateMasterBitMask()
		{
			Type[] types = new Type[]{typeof(ClassStyle), typeof(WindowStyle), typeof(WindowStyleEX) };

			foreach(Type type in types)
			{
				uint result = 0;
				foreach(uint i in Enum.GetValues(type))
				{
					result |= i;
				}
				if(type == typeof(ClassStyle))
					_classStyleMaster = result;
				else if(type == typeof(WindowStyle))
					_windowStyleMaster = result;
				else if(type == typeof(WindowStyleEX))
					_windowStyleEXMaster = result;
			}
		}

		#endregion

    }
}
