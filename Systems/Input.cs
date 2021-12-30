using System;
using GLFW;

namespace CMDR.Systems
{
	internal struct KeyBind
	{
		public Keys Key;
		public Action OnKeyUp;
		public Action OnKeyDown;
		public bool IsKeyDownTriggered;
	}
		
	public static class Input
	{
		private static KeyBind[] _keyBinds = new KeyBind[Data.StorageScale];
		public static int Count { get; private set; }

		public static bool UseMouse;
		public static bool KeepCenterMouse;

		private static double _mouseX;
		private static double _mouseY;

		public static void AddKeyBind(Keys key, Action onKeyDown, Action onKeyUp = null)
		{
			if (Count == _keyBinds.Length)
				Array.Resize(ref _keyBinds, _keyBinds.Length + Data.StorageScale);


			_keyBinds[Count] = new KeyBind
			{
				Key = key,
				OnKeyDown = onKeyDown,
				OnKeyUp = onKeyUp
			};
			Count++;
		}
		public static void RemoveKeyBind(Keys key)
		{
			throw new NotImplementedException("Systems.Input.RemoveKeyBind");
		}

		public static void Update(long ticks)
        {
			if(UseMouse)
            {
				Glfw.GetCursorPosition(Display.Window, out _mouseX, out _mouseY);
				if(KeepCenterMouse)
					Glfw.SetCursorPosition(Display.Window, Display.Width / 2, Display.Height / 2);

			}
        }

		/*
		public static void Update(long ticks)
		{
			for(int i = 0; i < Count; i++)
			{
				if (Keyboard.IsKeyDown(_keyBinds[i].Key) && !_keyBinds[i].IsKeyDownTriggered)
				{
					_keyBinds[i].OnKeyDown();
					_keyBinds[i].IsKeyDownTriggered = true;
				}
				else if (Keyboard.IsKeyUp(_keyBinds[i].Key) && _keyBinds[i].IsKeyDownTriggered)
				{
					if (_keyBinds[i].OnKeyUp != null)
						_keyBinds[i].OnKeyUp();
					
					_keyBinds[i].IsKeyDownTriggered = false;
				}
			}
		}
		*/
	}
}