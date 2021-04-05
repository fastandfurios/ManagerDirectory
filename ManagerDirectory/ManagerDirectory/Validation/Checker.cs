using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Repository;

namespace ManagerDirectory.Validation
{
    public class Checker
    {
	    private readonly Commands _commands = new Commands();

		/// <summary>
		/// Проверяет введенную команду
		/// </summary>
		/// <param name="nameCommand">Имя команды</param>
		/// <returns>Либо истина, либо ложь</returns>
		public bool CheckInputCommand(string nameCommand)
		{
			foreach (var command in _commands.ArrayCommands)
			{
				if ((command == nameCommand.Split(" ")[0] && nameCommand != string.Empty))
					return true;

				if (nameCommand.Contains(':') && nameCommand.Length == 2)
				{
					foreach (var drive in DriveInfo.GetDrives())
					{
						if (nameCommand == drive.Name.Replace("\\", ""))
							return true;
					}
				}
			}

			Console.WriteLine("Unknown command!");
			return false;
		}

		/// <summary>
		/// Проверяет существование файла или каталога
		/// </summary>
		/// <param name="path">Путь, заданный пользователем</param>
		/// <param name="defaultPath">Путь по умолчанию</param>
		/// <returns>Либо пользовательский существующий путь, либо стандартный путь</returns>
		public string CheckPath(string path, string defaultPath)
		{
			if (Directory.Exists(defaultPath + path))
				return defaultPath + path;
			else if ((Directory.Exists(path) || File.Exists(path)) && path.Contains('\\'))
				return path;

			Console.WriteLine(Path.GetExtension(path) == string.Empty ? "Directory not found!" : "File not found!");

			return defaultPath;
		}
	}
}
