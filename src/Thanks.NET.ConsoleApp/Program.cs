using System;
using System.Threading.Tasks;
using ThanksNET.Core;

namespace ThanksNET.ConsoleApp
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			var e = new PackageHandler("7c17a0f228a57292912acc525da38c4201d1bd98");
			await e.StarAllAsync(@"C:\Data\WORK\FC\Source\");

			Console.ReadKey();
		}
	}
}