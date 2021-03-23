using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.IO
{
    public class Output
    {
	    private DirectoryInfo _directory;

	    /// <summary>
		/// Выводит список директорий и файлов
		/// </summary>
		/// <param name="path">Путь</param>
	    public void OutputTree(string path)
	    {
		    try
		    {
				_directory = new DirectoryInfo(path);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(" " + path);
				Console.ResetColor();
				foreach (var directory in _directory.GetDirectories())
					Console.WriteLine($"{new string(' ', path.Length / 2)}|{new string('-', path.Length - path.Length / 2)}{directory.Name}");

				foreach (var file in _directory.GetFiles())
				{
					Console.Write($"{new string(' ', path.Length / 2)}|{new string('-', path.Length + 1 - path.Length / 2)}");
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write($"{file.Name}\n");
					Console.ResetColor();
				}
			}
		    catch (Exception e)
		    {
				Console.WriteLine(e);
		    }
	    }
    }
}
