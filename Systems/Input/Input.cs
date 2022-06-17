using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CMDR.Native;

namespace CMDR.Systems
{
		
	public static class Input
	{
		
		private static readonly byte _shiftMask = 0x01;
		private static readonly byte _ctrlMask = 0x02;
		private static readonly byte _altMask = 0x04;
		private static readonly byte _superMask = 0x08;
		private static readonly byte _capsLockMask = 0x10;
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
			long Lparam = lParam.ToInt64();

			Console.WriteLine(Convert.ToString(Lparam, 2));
            // Key pressed = 0, Key released = 1
            uint keyState = (uint)(Lparam & 0xff000000) & 0X80000000;

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
                    keyState >>= 31;
                    _modKeys &= (byte)(~_shiftMask);
					_modKeys |= (byte)(~keyState & _shiftMask);
					break;
				case (Key.Control):
                    GenerateModKeys(30, keyState, _ctrlMask);
					break;
				case (Key.Alt):
                    GenerateModKeys(29, keyState, _altMask);
                    break;
                case (Key.LeftWindows):
                    GenerateModKeys(28, keyState, _superMask);
                    break;
				case (Key.CapsLock):
                    // TODO .. Toggle logic for CapsLock
                    GenerateModKeys(27, keyState, _capsLockMask);
					break;
				case (Key.Numlock):
                    // TODO .. Toggle logic for NumLock
                    GenerateModKeys(26, keyState, _numsLockMask);
                    break;
			}
			
			if(_keyBinds.ContainsKey(key))
			{
				byte modCode = _modKeys;
				modCode |= (byte)(keyState >> 25);
				
				KeyEventArgs args = new KeyEventArgs(key, modCode, GameLoop.GameTime);
				
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

		public static void Update(long ticks)
        {
			if(UseMouse)
            {
				//Glfw.GetCursorPosition(Display.Window, out _mouseX, out _mouseY);

				if (KeepCenterMouse)
					return;
					//Glfw.SetCursorPosition(Display.Window, Display.Center.X, Display.Center.Y);

			}
        }

        private static void GenerateModKeys(int shiftRight, uint keyState, byte keyMask)
        {
            keyState >>= shiftRight;
            _modKeys &= (byte)(~keyMask);
            _modKeys |= (byte)(~keyState & keyMask);
        }
	}
}