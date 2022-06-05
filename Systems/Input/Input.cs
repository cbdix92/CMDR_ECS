using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GLFW;

namespace CMDR.Systems
{
		
	public static class Input
	{
		
		private static readonly byte _altMask = 0x08;
		private static readonly byte _shiftMask = 0x04;
		private static readonly byte _ctrlMask = 0x02;
		private static readonly byte _keyMask =	0x01;


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

		internal static IntPtr KeyboardCallback(int code, IntPtr wParam, IntPtr lParam)
        {
			if (code < 0)
				return Native.Win.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
			
			Key key = (Key)Marshal.ReadInt32(wParam);
			int data = Marshal.ReadInt32(lParam);
			
			byte keyState = (byte)((data << 8) & 0xff);
			
			// Update Mod key states
			switch(key)
			{
				case (Key.Shift):
					keyState = (byte)(keyState >> 8);
					break;
				case (Key.Control):
					keyState = (byte)(keyState >> 7);
					break;
				case (Key.Alt):
					keyState = (byte)(keyState >> 6);
					break;
                case (Key.LeftWindows):
					keyState = (byte)(keyState >> 5);
					break;
				case (Key.CapsLock):
					// TODO .. Toggle logic for CapsLock
					keyState = (byte)(keyState >> 4);
					break;
				case (Key.Numlock):
					// TODO .. Toggle logic for NumLock
					keyState = (byte)(keyState >> 3);
					break;
			}
			_modKeys = (byte)(_modKeys & (~keyState));
			
			
			if(_keyBinds.ContainsKey(key))
			{
				byte modCode = _modKeys;
				modCode = (byte)((code << 6) | modCode);
				short repeatCount = (short)((code >> 16) &0xffff);
				
				KeyEventArgs args = new KeyEventArgs(key, modCode, repeatCount, GameLoop.GameTime);
				
				foreach(KeyPressCallback callBack in _keyBinds[key])
					callBack(args);
			}
			
			
			Console.WriteLine("test");
			Console.WriteLine(_modKeys);
			return IntPtr.Zero;
        }
		
		internal static IntPtr MouseCallback(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code < 0)
				return Native.Win.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            
			MouseHookStruct mouseInfo = PtrToStructure<MouseHookStruct>(lParam);
			
			(_mouseX, _mouseY) = (mouseInfo.Pos.X, mouseInfo.Pos.Y);
			
			if (KeepCenterMouse)
			{
				// CenterMouse on screen
			}
			
			Console.WriteLine("MouseTest");
			
			
			
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