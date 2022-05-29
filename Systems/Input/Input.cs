using System;
using System.Collections.Generic;
using GLFW;

namespace CMDR.Systems
{
		
	public static class Input
	{

		private static readonly byte _altMask = 0x08;
		private static readonly byte _shiftMask = 0x04;
		private static readonly byte _ctrlMask = 0x02;
		private static readonly byte _keyMask =	0x01;


		private static Dictionary<Keys, List<KeyPressCallback>> _keyBinds = new Dictionary<Keys, List<KeyPressCallback>>();

		public static bool UseMouse;
		public static bool KeepCenterMouse;

		private static double _mouseX;
		private static double _mouseY;

		public static void AddKeyBind(Keys key, KeyPressCallback keyPressCallback)
		{
			if (!_keyBinds.ContainsKey(key))
				_keyBinds.Add(key, new List<KeyPressCallback>());
			
			_keyBinds[key].Add(keyPressCallback);

		}
		public static void RemoveKeyBind(Keys key)
		{
			throw new NotImplementedException("Systems.Input.RemoveKeyBind");
		}

		internal static IntPtr KeyboardCallback(int code, IntPtr wParam, IntPtr lParam)
        {
			if (code < 0)
				return Native.Win.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
			Console.WriteLine("test");
			return IntPtr.Zero;
        }

		internal static void KeyRecorder(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
			/// Store a que of key presses that will be processed the next time Input.Update is called.
			/// Combine action and mods into keyData byte using key mask.
        }

		public static void Update(long ticks)
        {
			if(UseMouse)
            {
				Glfw.GetCursorPosition(Display.Window, out _mouseX, out _mouseY);
				
				if(KeepCenterMouse)
					Glfw.SetCursorPosition(Display.Window, Display.Center.X, Display.Center.Y);

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