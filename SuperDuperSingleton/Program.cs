using System.Threading.Tasks;

namespace SuperDuperSingleton
{
	internal static class Program
	{
		private static readonly object lockObj = new object();

		private static void Main(string[] args)
		{
			using var streamWriter = System.IO.File.AppendText("test.txt");
			Parallel.For(0, 10000L, (l, state) =>
			{
				var singleton = Singleton.GetInstance("instance" + l);
				lock (lockObj)
				{
					streamWriter.WriteLine(l + " " + singleton.InstanceName);
				}
			});
		}
	}
}