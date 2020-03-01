using System;
using System.Windows.Input;

namespace CMDR.Systems
{
	internal struct KeyBind
	{
		public Key Key;
		public Action OnKeyUp;
		public Action OnKeyDown;
		public bool IsKeyDownTriggered;
	}
		
	public static class Input
	{
		private static KeyBind[] _keyBinds = new KeyBind[Data.StorageScale];
		public static int Count { get; private set; }
		public static void AddKeyBind(Key key, Action onKeyDown, Action onKeyUp = null)
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
		
		public static void DetectKeys()
		{
			for(int i = 0; i < _keyBinds.Length; i++)
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
	}
}