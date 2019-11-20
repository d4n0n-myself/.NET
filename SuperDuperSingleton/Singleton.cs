using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SuperDuperSingleton
{
	/// <summary>
	/// Потокобезопастный Singleton.
	/// </summary>
	public class Singleton
	{
		private Singleton()
		{
		}

		private static Singleton _instance;

		/// <summary>
		/// Объект для синхронизации создания экземпляра <see cref="Singleton"/>
		/// </summary>
		private static readonly object Lock = new object();

		/// <summary>
		/// Получить экземпляр <see cref="Singleton"/>
		/// </summary>
		/// <remarks>
		/// Реализовано через double-check locking.
		/// 1 проверка исключает доступ к созданию экземпляра - если экземлпяр уже создан, просто возвращаем его.
		/// Блокировка предназначена для гарантии того,
		/// чтобы при запуске программы несколько потоков не смогут одновременно попасть в блок создания экземпляра.
		/// 2 проверка предназначена для тех потоков, которые обратились к получению экземпляра, и попали в блокировку, когда он еще не был создан,
		/// а теперь могут получить созданный экземпляр, который создал поток, первый попавший в блок создания экземпляра.
		/// </remarks>
		/// <param name="instanceName"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Копия метода GetInstance для расширенного тестирования.
		/// </summary>
		/// <param name="instanceName"></param>
		/// <returns></returns>
		public static Singleton TestGetInstance(string instanceName, StreamWriter sw)
		{
			sw.WriteLine($"{instanceName}: Entering {nameof(TestGetInstance)} method...");
			if (_instance == null)
			{
				sw.WriteLine($"{instanceName}: Passed first check...");
				lock (Lock)
				{
					sw.WriteLine($"{instanceName}: Entered lock statement...");
					if (_instance == null)
					{
						sw.WriteLine($"{instanceName}: Creating Singleton...");
						_instance = new Singleton {InstanceName = instanceName};
					}
				}
			}

			return _instance;
		}

		/// <summary>
		/// Свойство для идентификации объекта.
		/// </summary>
		public string InstanceName { get; set; }
	}
}