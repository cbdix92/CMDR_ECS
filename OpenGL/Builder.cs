using System;
using System.Linq;
using System.Reflection;

namespace OpenGL
{
	private class Builder
	{
		private static Builder instance = new Builder();
		
		private Builder()
		{
			MethodInfo[] methods = Assembly.GetTypes().SelectMany(x => x.GetMethods()).Where(y => y.GetCustomAttributes(typeof(BuildInfo), false).length > 0).ToArray();
			
			foreach(MethodInfo method in methods)
			{
				Delegate delegate = method.CreatDelegate(method.GetType());
			}
			
		}
		
		private IntPtr GetProc(string name)
		{
			
		}		
	}
	
	public class BuildInfo : Attribute
	{
		string Name;
		string Lib;
	}
	
	
	
}