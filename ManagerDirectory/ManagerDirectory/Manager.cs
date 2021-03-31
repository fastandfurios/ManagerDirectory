using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManagerDirectory.Models;

namespace ManagerDirectory
{
	public class Manager : Objects
	{
		private string _entry;
	    private string _defaultPath = "C:\\";
	    private string _fileName = "CurrentPath.json";
	    private string _fileLogErrors = "LogErrors.txt";

		public void Start()
		{
			GetDisk();
			Run();
		}

		/// <summary>
		/// Получает диск
		/// </summary>
		private void GetDisk()
		{
			_currentPath = _deserializer.Deserialize(_fileName, _currentPath, _defaultPath);

			foreach (var drive in DriveInfo.GetDrives())
			{
				if (_currentPath.Path.Length > drive.Name.Length)
				{
					if (drive.Name == _currentPath.Path.Substring(0, 3))
						return;
				}
				else
				{
					if (drive.Name == _currentPath.Path.Substring(0, _currentPath.Path.Length))
						return;
				}
			}

			_currentPath.Path = _defaultPath;
		} 

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

				if(command.Contains(':'))
					_defaultPath = command + "\\";
				
			    string path = string.Empty;
			    string newPath = string.Empty;

			    switch (command)
			    {
					case "disk":
						CallOutput();
						break;
					case "ls":
						path = _entry.Length == command.Length ? _defaultPath : _entry.Remove(0, command.Length + 1);
						path = _checker.CheckPath(path, _defaultPath);
						CallOutput(path, 10);
						break;
					case "lsAll":
						path = _entry.Length == command.Length ? _defaultPath : _entry.Remove(0, command.Length + 1);
						path = _checker.CheckPath(path, _defaultPath);
						CallOutput(path, Directory.GetDirectories(path).Length + Directory.GetFiles(path).Length);
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
						path = _entry.Remove(0, command.Length + 1) + "\\";
						_defaultPath = _checker.CheckPath(path, _defaultPath);
						break;
					case "cd..":
						path = _defaultPath.Remove(_defaultPath.Length - 1, 1);
						_defaultPath = Directory.GetParent(path)?.FullName;
						break;
					case "cd\\":
						_defaultPath = Directory.GetDirectoryRoot(_defaultPath);
						break;
					case "info":
						CallInformer(command);
						_output.OutputInfoFilesAndDirectory(_informer);
						break;
					case "rm":
						CallDeletion(command);
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
				Console.WriteLine(e.Message);
				File.AppendAllText(_fileLogErrors, $"{DateTime.Now.ToString("G")} {e.Message} {e.TargetSite}" );
				File.AppendAllText(_fileLogErrors, Environment.NewLine);
				Run();
			}
		}

		/// <summary>
		/// Вызывает вывод дисков в системе
		/// </summary>
		private void CallOutput()
			=> _output.GetDrives();

		/// <summary>
		/// Вызывает вывод деревьев
		/// </summary>
		/// <param name="path">Путь</param>
		private void CallOutput(string path, int maxObjects)
			=> _output.OutputTree(path, maxObjects);

		/// <summary>
		/// Вызывает копирование
		/// </summary>
		/// <param name="name">Имя удаляемого файла или папки</param>
		/// <param name="newPath">Путь, по которому производится копирование</param>
		private void CallCopying(string name, string newPath)
			=> _copying.Copy(_defaultPath, name, newPath);

		/// <summary>
		/// Вызывает удаление
		/// </summary>
		private void CallDeletion(string command)
		{
			string entry = _checker.CheckPath(_entry.Remove(0, command.Length + 1), _defaultPath);

			if (Path.GetExtension(entry) != string.Empty)
				_deletion.FullPathFile = entry;
			else
				_deletion.FullPathDirectory = entry;
		}

		/// <summary>
		/// Вызывает информатор объектов
		/// </summary>
		private void CallInformer(string command)
		{
			string entry = string.Empty;

			if (_entry.Length == command.Length)
				entry = _defaultPath;
			else
				entry = _checker.CheckPath(_entry.Remove(0, command.Length + 1), _defaultPath);


			if (Path.GetExtension(entry) != string.Empty)
			{
				_informer.FullPathFile = entry;
				_informer.FullPathDirectory = string.Empty;
			}
			else
			{
				_informer.FullPathDirectory = entry;
				_informer.FullPathFile = string.Empty;
			}
		}

		/// <summary>
		/// Преобразует введенную строку пользователя в имя файла или папки при операции копирования
		/// </summary>
		/// <param name="str">Строка</param>
		/// <returns>Имя файла или папки</returns>
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
