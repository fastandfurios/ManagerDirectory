using System;
using System.IO;
using System.Text.Json;
using ManagerDirectory.Models;

namespace ManagerDirectory.Serialization
{
    public class Deserializer
    {
	    private string _currentPath;

		public CurrentPath Deserialize(string fileName, CurrentPath currentPath, string defaultPath)
		{
			try
			{
				_currentPath = File.ReadAllText(fileName);
				return JsonSerializer.Deserialize<CurrentPath>(_currentPath);
			}
			catch { }

			currentPath.Path = defaultPath;
			return currentPath;
		}
	}
}
