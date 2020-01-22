using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Singleton.Tests
{
	public class Tests
	{
		private static readonly object lockObj = new object();

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void TryCreate_10000Instances()
		{
			System.IO.File.Delete("../../../../inst10000.txt");
			using var streamWriter = System.IO.File.AppendText("../../../../inst10000.txt");
			Parallel.For(0, 10000L, (l, state) =>
			{
				var singleton = SuperDuperSingleton.Singleton.GetInstance("instance" + l);
				lock (lockObj)
				{
					streamWriter.WriteLine(l + " " + singleton.InstanceName);
				}
			});
		}

		[Test]
		public void TryCreate_10Instances_WithDebugData()
		{
			System.IO.File.Delete("../../../../inst10.txt");
			using var streamWriter = System.IO.File.AppendText("../../../../inst10.txt");
			Parallel.For(0, 10, (l, state) =>
			{
				var singleton = SuperDuperSingleton.Singleton.TestGetInstance("instance" + l, streamWriter);
				lock (lockObj)
				{
					streamWriter.WriteLine(l + " " + singleton.InstanceName);
				}
			});
		}
	}
}