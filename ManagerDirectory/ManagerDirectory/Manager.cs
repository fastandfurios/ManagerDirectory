using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManagerDirectory.Actions;
using ManagerDirectory.Models;

namespace ManagerDirectory
{
	public class Manager : Objects
	{
		#region Fields
		private string _entry;
		private string _defaultPath = "C:\\";
		private string _fileName = "CurrentPath.json";
		private string _fileLogErrors = "LogErrors.txt";
		#endregion

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
			CurrentPath = Deserializer.Deserialize(_fileName, CurrentPath, _defaultPath);

			foreach (var drive in DriveInfo.GetDrives())
			{
				if (CurrentPath.Path.Length > drive.Name.Length)
				{
					if (drive.Name == CurrentPath.Path.Substring(0, 3))
						return;
				}
				else
				{
					if (drive.Name == CurrentPath.Path.Substring(0, CurrentPath.Path.Length))
						return;
				}
			}

			CurrentPath.Path = _defaultPath;
		} 

	    private void Run()
	    {
			if (File.Exists(_fileName) && !string.IsNullOrEmpty(CurrentPath.Path))
				if(Directory.Exists(CurrentPath.Path))
					_defaultPath = CurrentPath.Path;

			_entry = Input.Input(_defaultPath, Checker);

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
						Output.GetDrives();
						JumpInRun();
						break;
					case "ls":
						CallOutput(path, command, 10);
						JumpInRun();
						break;
					case "lsAll":
						CallOutput(path, command, Directory.GetDirectories(path).Length + Directory.GetFiles(path).Length);
						JumpInRun();
						break;
					case "cp":
						CallCopying(path, newPath, command);
						JumpInRun();
						break;
					case "clear":
						Console.Clear();
						JumpInRun();
						break;
					case "cd":
						path = _entry.Remove(0, command.Length + 1);
						ChangePath(ref path);
						_defaultPath = Checker.CheckPath(path, _defaultPath);
						JumpInRun();
						break;
					case "cd..":
						path = _defaultPath.Remove(_defaultPath.Length - 1, 1);
						_defaultPath = Directory.GetParent(path)?.FullName;
						JumpInRun();
						break;
					case "cd\\":
						_defaultPath = Directory.GetDirectoryRoot(_defaultPath);
						JumpInRun();
						break;
					case "info":
						CallInformer(command);
						Output.OutputInfoFilesAndDirectory(Informer);
						JumpInRun();
						break;
					case "rm":
						CallDeletion(command);
						JumpInRun();
						break;
					case "exit":
						CurrentPath.Path = _defaultPath;
						Serializer.Serialize(CurrentPath, _fileName);
						break;
			    }
		    }
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				File.AppendAllText(_fileLogErrors, $"{DateTime.Now:G} {e.Message} {e.TargetSite}");
				File.AppendAllText(_fileLogErrors, Environment.NewLine);
				Run();
			}
		}

		/// <summary>
		/// Вызывает вывод деревьев
		/// </summary>
		/// <param name="path">Путь</param>
		private void CallOutput(string path, string command, int maxObjects)
		{
			path = _entry.Length == command.Length ? _defaultPath : _entry.Remove(0, command.Length + 1);
			path = Checker.CheckPath(path, _defaultPath);
			Output.OutputTree(path, maxObjects);
		}

		/// <summary>
		/// Вызывает копирование
		/// </summary>
		/// <param name="name">Имя удаляемого файла или папки</param>
		/// <param name="newPath">Путь, по которому производится копирование</param>
		private void CallCopying(string name, string newPath, string command)
		{
			name = Transform(_entry.Remove(0, command.Length + 1)).TrimEnd();
			newPath = _entry.Remove(0, command.Length + name.Length + 2) + "\\";
			Copying.Copy(_defaultPath, name, newPath);
		}
		
		/// <summary>
		/// Вызывает удаление
		/// </summary>
		private void CallDeletion(string command)
		{
			string entry = Checker.CheckPath(_entry.Remove(0, command.Length + 1), _defaultPath);

			if (Input.Input(Checker).Equals("y"))
			{
				if (Path.GetExtension(entry) != string.Empty)
					Deletion.FullPathFile = entry;
				else
					Deletion.FullPathDirectory = entry;
			}
		}

		/// <summary>
		/// Вызывает информатор объектов
		/// </summary>
		private void CallInformer(string command)
		{
			string entry = _entry.Length == command.Length ? _defaultPath : 
				Checker.CheckPath(_entry.Remove(0, command.Length + 1), _defaultPath);
			
			if (Path.GetExtension(entry) != string.Empty)
			{
				Informer.FullPathFile = entry;
				Informer.FullPathDirectory = string.Empty;
			}
			else
			{
				Informer.FullPathDirectory = entry;
				Informer.FullPathFile = string.Empty;
			}
		}

		/// <summary>
		/// Преобразует введенную строку пользователя в имя файла или папки при операции копирования
		/// </summary>
		/// <param name="entry">Строка</param>
		/// <returns>Имя файла или папки</returns>
		private string Transform(string entry)
		{
			for (int i = entry.Length - 1; i > 0; i--)
			{
				if (entry[i] != ':')
					entry = entry.Remove(i, 1);
				else
				{
					for (int j = entry.Length - 1; j > 0; j--)
					{
						if (entry[j] != ' ')
							entry = entry.Remove(j, 1);
						else
							break;
					}

					break;
				}
			}

			return entry;
		}

		private void ChangePath(ref string entry)
		{
			if (!entry.Contains("\\"))
			{
				if (_defaultPath.EndsWith("\\")) { return; }
				else
					entry = "\\" + entry;
			}
		}

		private void JumpInRun()
		{
			CurrentPath.Path = string.Empty;
			Run();
		}
	}
}
