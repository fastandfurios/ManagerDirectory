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
			    string newPath = path.Split(" ")[1];
			    _directory = new DirectoryInfo(newPath);
			    Console.WriteLine(" " + newPath);
			    foreach (var directory in _directory.GetDirectories())
				    Console.WriteLine($" |{new string('-', newPath.Length)}{directory.Name}");

			    foreach (var file in _directory.GetFiles())
			    {
					Console.Write($" |{new string('-', newPath.Length + 1)}");
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
