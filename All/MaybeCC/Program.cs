using System;

namespace MaybeCC
{
	class Program
	{
		public static void Main(string[] args)
		{
			var resultGood = TestMaybeGood();
			PrintResult(resultGood);

			var resultBad = TestMaybeBad();
			PrintResult(resultBad);
		}

		static async Option<int> TestMaybeGood()
		{
			var val1 = await TryDivide(120, 2);
			var val2 = await TryDivide(val1, 2);
			var val3 = await TryDivide(val2, 2);

			return val3;
		}

		static async Option<int> TestMaybeBad()
		{
			var val1 = await TryDivide(120, 2);
			var val2 = await TryDivide(val1, 0); 
			var val3 = await TryDivide(val2, 2);

			return val3;
		}

		static Option<int> TryDivide(int top, int bottom)
		{
			Console.WriteLine($"Trying to execute division {top}/{bottom}");
			if (bottom == 0)
				return None<int>.Value;

			return Some.Of(top / bottom);
		}

		static void PrintResult<T>(Option<T> maybe)
		{
			switch (maybe)
			{
				case None<T> n:
					Console.WriteLine("None");
					break;
				case Some<T> s:
					Console.WriteLine($"Some {(T) s}");
					break;
			}
		}
	}
}