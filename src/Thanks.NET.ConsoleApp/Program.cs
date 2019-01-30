using System;
using System.Threading.Tasks;
using ThanksNET.Core;

namespace ThanksNET.ConsoleApp
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			var e = new Executor("");
			await e.RunAsync(@"C:\Data\WORK\FC\Source\");

			Console.ReadKey();
		}
	}
}