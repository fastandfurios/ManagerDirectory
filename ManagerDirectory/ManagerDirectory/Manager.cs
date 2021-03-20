using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory
{
    public class Manager : Objects
    {
	    private string _entry;
	    private string _defaultPath = "c:\\";

		/// <summary>
		/// Запускает программу
		/// </summary>
	    public void Run()
	    {
		    _entry = _input.Input(_defaultPath);

			ToDistribute();
	    }

		/// <summary>
		/// Управляет всей программой
		/// </summary>
	    private void ToDistribute()
	    {
		    try
		    {
			    string command = _entry.Split(" ")[0];
			    string path = string.Empty;
			    string newPath = string.Empty;

			    switch (command)
			    {
				    case "ls":
					    path = _entry.Length == 2 ? _defaultPath : _entry.Split(" ")[1];
						CallOutput(path);
					    break;
				    case "cp":
						path = _entry.Split(" ")[1];
						newPath = _entry.Split(" ")[2];
						CallCopying(path, newPath);
					    break;
					case "clear":
						Console.Clear();
						break;
					case "cd":
						_defaultPath = _defaultPath + _entry.Split(" ")[1] + "\\";
						break;
					case "cd..":
						_defaultPath = Directory.GetParent(_defaultPath.Remove(_defaultPath.Length-1, 1)).FullName;
						break;
					case "cd\\":
						_defaultPath = Directory.GetDirectoryRoot(_defaultPath);
						break;
					case "info":
						break;
					case "rm":
						CallDeletion();
						break;
			    }

				if(command != "exit")
					Run();
		    }
		    catch
		    {
			    Run();
		    }
	    }

		/// <summary>
		/// Вызывает вывод
		/// </summary>
		/// <param name="path">Путь</param>
		private void CallOutput(string path)
		{
			_output.OutputTree(path);
		}

		/// <summary>
		/// Вызывает копирование
		/// </summary>
		/// <param name="name">Имя удаляемого файла или папки</param>
		/// <param name="newPath">Путь, по которому производится копирование</param>
		private void CallCopying(string name, string newPath)
		{
			_copying.Copy(_defaultPath, name, newPath);
		}

		/// <summary>
		/// Вызывает удаление
		/// </summary>
		private void CallDeletion()
		{
			string entry = _entry.Split(" ")[1];

			if (!entry.Contains("\\"))
				entry = _defaultPath + entry;


			if (Path.GetExtension(entry) != string.Empty)
				_deletion.FullPathFile = entry;
			else
				_deletion.FullPathDirectory = entry;
		}
    }
}
