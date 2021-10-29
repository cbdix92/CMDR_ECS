using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;

namespace OpenGL
{
	internal class Builder
	{
		private static Builder instance = new Builder();

		public AppDomain domain = Thread.GetDomain();
		AppDomain.CurrentDomain.

		AssemblyBuilder assemblyBuilder = Thread.GetDomain()
		
		private unsafe Builder()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			MethodInfo[] methods = assembly.GetTypes().SelectMany(x => x.GetMethods()).Where(y => y.GetCustomAttributes(typeof(BuildInfo), false).Length > 0).ToArray();
			
			foreach(MethodInfo method in methods)
			{
				BuildInfo buildInfo = method.GetCustomAttribute<BuildInfo>();
				string name = buildInfo.Name;
				IntPtr ptr = GetProc(name);
				IntPtr outout = method.MethodHandle.GetFunctionPointer();
				Marshal.GetDelegateForFunctionPointer(GetProc(name), method.GetType());
			}
			
		}
		
		private IntPtr GetProc(string name)
		{
			
		}		
	}
	
	public class BuildInfo : Attribute
	{
		public string Name;
		public string Lib;
	}
	
	
	
}