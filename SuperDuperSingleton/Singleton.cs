namespace SuperDuperSingleton
{
	internal class Singleton
	{
		private Singleton()
		{
		}

		private static Singleton _instance;

		private static readonly object Lock = new object();

		public static Singleton GetInstance(string instanceName)
		{
			if (_instance == null)
			{
				lock (Lock)
				{
					if (_instance == null)
					{
						_instance = new Singleton {InstanceName = instanceName};
					}
				}
			}

			return _instance;
		}

		public string InstanceName { get; set; }
	}
}