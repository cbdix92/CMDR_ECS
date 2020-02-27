using System;
using System.Windows.Input;

namspace CMDR
{
	internal struct KeyBind
	{
		public Key Key;
		public Action OnKeyUp;
		public Action OnKeyDown;
		public bool IsKeyDownTriggered { get; private set; }
		
		internal void Detect()
		{
			if (Keyboard.IsKeyDown(Key) && !IsKeyDownTriggered)
			{
				OnKeyDown();
				IsKeyDownTriggered = true;
			}
			else if (Keyboard.IsKeyUp(Key) && IsKeyDownTriggered)
			{
				if (OnKeyUp != null)
					OnKeyUp();
				
				IsKeyDownTriggered = false;
			}
		}
		
		public static class Input
		{
			private static KeyBind[] _keyBinds = new KeyBind[Data.StorageScale];
			public static int Count { get; private set; }
			public static void AddKeyBind(Key key, Action onKeyDown, Action onKeyUp = null)
			{
				if(Count == _keyBinds.Length)
					Array.Resize(ref _keyBinds, _keyBinds.Length + Data.StorageScale)
			}
		}
	}
}