using BulkLove.Core.IO;
using BulkLove.Core.Nuget;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BulkLove.ConsoleApp
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			var a = PackageCrawler.Crawl(@"C:\Data\WORK\FC\Source\");
			var nw = new NugetWrapper();

			var res = await Task.WhenAll(a.Select(p => nw.GetAsync(p.Id, p.Version.ToString())));
			foreach (var r in res)
			{
				Console.WriteLine(r?.ProjectUrl);
			}
		}
	}
}