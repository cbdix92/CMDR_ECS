using System;

namespace CMDR
{
	public static class MathHelper
	{
		public static float Cos(float num)
		{
			return (float)Math.Cos((double)num);
		}
		
		public static float Sin(float num)
		{
			return (float)Math.Sin((double)num);
		}
		
		public static float Tan(float num)
		{
			return (float)Math.Tan((double)num);
		}
		
		public static float Abs(float num)
		{
			return (float)Math.Abs((double)num);
		}
		
		public static float Min(float num1, float num2)
		{
			return (float)Math.Min((double)num1, (double)num2);
		}
		
		public static float Max(float num1, float num2)
		{
			return (float)Math.Max((double)num1, (double)num2);
		}
		
		public static float Sqrt(float num)
		{
			return (float)Math.Sqrt((double)num);
		}
	}
}