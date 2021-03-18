using System;

namespace ManagerDirectory
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "ManagerDirectory";

			var managerDirectory = new Manager();
			managerDirectory.Run();
		}
	}
}
