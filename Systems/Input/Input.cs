using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CMDR.Native;
using GLFW;

namespace CMDR.Systems
{
		
	public static class Input
	{
		
		private static readonly byte _shiftMask = 0x01;
		private static readonly byte _ctrlMask = 0x02;
		private static readonly byte _altMask = 0x04;
		private static readonly byte _superMask = 0x08;
		private static readonly byte _capslockMask = 0x10;
		private static readonly byte _numsLockMask = 0x20;


		private static Dictionary<Key, List<KeyPressCallback>> _keyBinds = new Dictionary<Key, List<KeyPressCallback>>();

		public static bool UseMouse;
		public static bool KeepCenterMouse;

		private static int _mouseX;
		private static int _mouseY;
		
		
		private static byte _modKeys;
		public static byte ModKeys => _modKeys;

		public static void AddKeyBind(Key key, KeyPressCallback keyPressCallback)
		{
			if (!_keyBinds.ContainsKey(key))
				_keyBinds.Add(key, new List<KeyPressCallback>());
			
			_keyBinds[key].Add(keyPressCallback);

		}
		public static void RemoveKeyBind(Key key)
		{
			throw new NotImplementedException("Systems.Input.RemoveKeyBind");
		}

		internal static unsafe IntPtr KeyboardCallback(int code, IntPtr wParam, IntPtr lParam)
        {
			if (code < 0)
				return Win.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
			
			int keybyte = wParam.ToInt32();
			long data = lParam.ToInt64();

			// Key pressed = 0, Key released = 1
			long keyState = data & 1;
			Console.Clear();
			Console.WriteLine(keyState);

			Key key;
            try
            {
				key = (Key)keybyte;
            }
			catch(InvalidCastException) // Unsupported key pressed
			{
				return IntPtr.Zero;
            }

			// Update Mod key states
			switch(key)
			{
				case (Key.Shift):
					_modKeys |= (byte)((~keyState) & _shiftMask);
					break;
				case (Key.Control):
					keyState <<= 1;
					break;
				case (Key.Alt):
					keyState <<= 2;
					break;
                case (Key.LeftWindows):
					keyState <<= 3;
					break;
				case (Key.CapsLock):
					// TODO .. Toggle logic for CapsLock
					keyState <<= 4;
					break;
				case (Key.Numlock):
					// TODO .. Toggle logic for NumLock
					keyState <<= 5;
					break;
			}
			//_modKeys = (byte)(_modKeys | (~keyState));
			
			
			if(_keyBinds.ContainsKey(key))
			{
				byte modCode = _modKeys;
				modCode = (byte)((code << 6) | modCode);
				short repeatCount = (short)((code >> 16) & 0xffff);
				
				KeyEventArgs args = new KeyEventArgs(key, modCode, repeatCount, GameLoop.GameTime);
				
				foreach(KeyPressCallback callBack in _keyBinds[key])
					callBack(args);
			}
			
			Console.WriteLine($"ModKeys: {Convert.ToString(_modKeys, 2)}");
			return IntPtr.Zero;
        }
		
		internal static IntPtr MouseCallback(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code < 0)
				return Win.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

			MouseHookStruct mouseInfo = Marshal.PtrToStructure<MouseHookStruct>(lParam);
			
			(_mouseX, _mouseY) = (mouseInfo.Pos.X, mouseInfo.Pos.Y);
			
			if (KeepCenterMouse)
			{
				// CenterMouse on screen
			}
			
			//Console.WriteLine($"X:{mouseInfo.Pos.X}\nY:{mouseInfo.Pos.Y}");
			
			
			
			return IntPtr.Zero;
		}

		internal static void KeyRecorder(Window window, Key key, int scanCode, InputState state, ModifierKeys mods)
        {
			/// Store a que of key presses that will be processed the next time Input.Update is called.
			/// Combine action and mods into keyData byte using key mask.
        }

		public static void Update(long ticks)
        {
			if(UseMouse)
            {
				//Glfw.GetCursorPosition(Display.Window, out _mouseX, out _mouseY);
				
				if(KeepCenterMouse)
					Glfw.SetCursorPosition(Display.Window, Display.Center.X, Display.Center.Y);

			}
        }
	}
}