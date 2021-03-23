using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Models;

namespace ManagerDirectory
{
    public class Manager : Objects
    {
	    private string _entry;
	    private string _defaultPath = "c:\\";
	    private string _fileName = "CurrentPath.json";
	    private string _fileLogErrors = "LogErrors.txt";

		public Manager()
		{
			Start();
			Run();
		}

		/// <summary>
		/// Начинает программу
		/// </summary>
		private void Start() => _currentPath = _deserializer.Deserialize(_fileName);

	    private void Run()
	    {
			if (File.Exists(_fileName) && !string.IsNullOrEmpty(_currentPath.Path))
				_defaultPath = _currentPath.Path;

			_entry = _input.Input(_defaultPath, _checker);

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
					    path = _entry.Length == 2 ? _defaultPath : _entry.Remove(0, command.Length + 1);
						CallOutput(path);
					    break;
				    case "cp":
						path = Transform(_entry.Remove(0, command.Length + 1)).TrimEnd();
						newPath = _entry.Remove(0, command.Length + path.Length + 2) + "\\";
						CallCopying(path, newPath);
					    break;
					case "clear":
						Console.Clear();
						break;
					case "cd":
						_defaultPath = _checker.CheckPath( _entry.Split(" ")[1] + "\\", _defaultPath);
						break;
					case "cd..":
						_defaultPath = Directory.GetParent(_defaultPath.Remove(_defaultPath.Length - 1, 1))?.FullName;
						break;
					case "cd\\":
						_defaultPath = Directory.GetDirectoryRoot(_defaultPath);
						break;
					case "info":
						CallInformer();
						_output.OutputInfoFilesAndDirectory(_informer);
						break;
					case "rm":
						CallDeletion();
						break;
			    }

			    if (command != "exit")
			    {
				    _currentPath.Path = string.Empty;
				    Run();
				}
			    else
			    {
				    _currentPath.Path = _defaultPath;
				    _serializer.Serialize(_currentPath, _fileName);
				}
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine("Bad request!");
				File.AppendAllText(_fileLogErrors, $"{DateTime.Now.ToString("G")} {e.Message} {e.TargetSite}" );
				File.AppendAllText(_fileLogErrors, Environment.NewLine);
			    Run();
		    }
	    }

		/// <summary>
		/// Вызывает вывод
		/// </summary>
		/// <param name="path">Путь</param>
		private void CallOutput(string path)
		{
			_output.OutputTree(_checker.CheckPath(path, _defaultPath));
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
			string entry = GetPath(_entry.Split(" ")[1]);

			if (Path.GetExtension(entry) != string.Empty)
				_deletion.FullPathFile = entry;
			else
				_deletion.FullPathDirectory = entry;
		}

		/// <summary>
		/// Вызывает информатор объектов
		/// </summary>
		private void CallInformer()
		{
			string entry = string.Empty;

			if (_entry.Length == 4)
				entry = _defaultPath;
			else
				entry = GetPath(_entry.Split(" ")[1]);
			
			
			if (Path.GetExtension(entry) != string.Empty)
				_informer.FullPathFile = entry;
			else
				_informer.FullPathDirectory = entry;
		}

		/// <summary>
		/// Преобразует путь
		/// </summary>
		/// <returns>Абсолютный путь</returns>
		private string GetPath(string entry)
		{
			if (!entry.Contains("\\"))
				return entry = _defaultPath + entry;

			return _defaultPath;
		}

		private string Transform(string str)
		{
			for (int i = str.Length - 1; i > 0; i--)
			{
				if (str[i] != ':')
					str = str.Remove(i, 1);
				else
				{
					for (int j = str.Length - 1; j > 0; j--)
					{
						if (str[j] != ' ')
							str = str.Remove(j, 1);
						else
							break;
					}

					break;
				}
			}

			return str;
		}
    }
}
