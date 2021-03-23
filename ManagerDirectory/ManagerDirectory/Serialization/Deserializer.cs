using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
