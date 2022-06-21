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

		private uint _classStyleMaster;

		private uint _windowStyleMaster;

		private uint _windowStyleEXMaster;

		#endregion

		#region CONSTRUCTORS

		public Window(uint width, uint height, int startingX, int startingY, string title)
		{
            (Width, Height, startingX, startingY, Title) = (width, height, StartingPosX, StartingPosY, title);
			GenerateMasterBitMask();
		}

        #endregion

        #region PUBLIC_METHODS

        public void SetFlag(WindowTarget target, uint flag)
		{
			switch(target)
			{
				case WindowTarget.ClassStyle:
					ClassStyle |= (flag & ~(_classStyleMaster));
					break;
				case WindowTarget.WindowStyle:
					WindowStyle |= (flag & ~(_windowStyleMaster));
					break;
				case WindowTarget.WindowStyleExtended:
					WindowStyleEX |= (flag & ~(_windowStyleEXMaster));
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
