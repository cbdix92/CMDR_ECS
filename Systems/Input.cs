using System;
using System.Windows.Input;

namspace CMDR
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
			if(Count == _keyBinds.Length)
				Array.Resize(ref _keyBinds, _keyBinds.Length + Data.StorageScale)
			
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
			foreach(KeyBind keybind in _keyBinds)
			{
				if (Keyboard.IsKeyDown(keybind.Key) && !keybind.IsKeyDownTriggered)
				{
					keybind.OnKeyDown();
					keybind.IsKeyDownTriggered = true;
				}
				else if (Keyboard.IsKeyUp(keybind.Key) && keybind.IsKeyDownTriggered)
				{
					if (OnKeyUp != null)
						OnKeyUp();
					
					keybind.IsKeyDownTriggered = false;
				}
			}
		}
	}
}