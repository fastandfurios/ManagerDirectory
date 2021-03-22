using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Actions;

namespace ManagerDirectory.IO
{
    public class Output
    {
	    private DirectoryInfo _directory;
	    private const int MAX_OBJECTS = 10;
	    private int _countFiles, _countDirectory;

	    /// <summary>
		/// Выводит список директорий и файлов
		/// </summary>
		/// <param name="path">Путь</param>
	    public void OutputTree(string path)
	    {
		    _directory = new DirectoryInfo(path);
		    Console.ForegroundColor = ConsoleColor.Yellow;
		    Console.WriteLine(" " + path);
		    Console.ResetColor();
		    foreach (var directory in _directory.GetDirectories())
		    {
			    if (_countDirectory < MAX_OBJECTS)
			    {
				    Console.WriteLine(
					    $"{new string(' ', path.Length / 2)}|{new string('-', path.Length - path.Length / 2)}{directory.Name}");
				    _countDirectory++;
			    }
			    else
			    {
				    Console.WriteLine(
					    $"{new string(' ', path.Length / 2)}|{new string('-', path.Length - path.Length / 2)}...");
				    break;
			    }
		    }

		    _countDirectory = 0;

			foreach (var file in _directory.GetFiles())
		    {
			    if (_countFiles < MAX_OBJECTS)
			    {
				    Console.Write(
					    $"{new string(' ', path.Length / 2)}|{new string('-', path.Length + 1 - path.Length / 2)}");
				    Console.ForegroundColor = ConsoleColor.DarkGreen;
				    Console.Write($"{file.Name}\n");
				    Console.ResetColor();
				    _countFiles++;
			    }
			    else
			    {
				    Console.Write(
					    $"{new string(' ', path.Length / 2)}|{new string('-', path.Length + 1 - path.Length / 2)}...\n");
				    break;
			    }
		    }

			_countFiles = 0;
		}

	    public void OutputInfoFilesAndDirectory(Informer informer) => Console.WriteLine(informer);
    }
}
