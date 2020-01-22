using System;

namespace HelloApp
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Введите свое имя");
			string name = Console.ReadLine();
			Console.WriteLine($"Hello, {name}");
		}
	}
}