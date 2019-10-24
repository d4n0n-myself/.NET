﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClient
{
	class Program
	{
		static void Main(string[] args)
		{
			GetByWebClient();
			Thread.Sleep(1000);
		}

		static void GetByWebClient()
		{
			var webClient = new WebClient();
			var requestUri = new Uri("http://google.com");
			var responseString = webClient.DownloadString(requestUri);
			Console.WriteLine(responseString);
		}

		static async Task GetByHttpClient()
		{
			var httpClient = new System.Net.Http.HttpClient();
			var requestUri = new Uri("http://google.com");
			var message = await httpClient.GetAsync(requestUri);
			
			Console.WriteLine("Headers");
			
			foreach (var header in message.Headers)
			{
				Console.WriteLine($"{header.Key} : {string.Join(",", header.Value)}");
			}

			Console.WriteLine();
			
			Console.WriteLine($"Status Code: {(int) message.StatusCode} {message.StatusCode}");
			
			
			Console.WriteLine();

			var response = await message.Content.ReadAsStringAsync();
			Console.WriteLine(response);
		}
	}
}