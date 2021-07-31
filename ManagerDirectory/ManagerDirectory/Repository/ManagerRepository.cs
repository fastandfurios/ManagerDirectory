using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ManagerDirectory.Models;

namespace ManagerDirectory.Repository
{
    public class ManagerRepository
    {
	    private string _currentPath;

		public CurrentPath GetSavePath(string fileName, CurrentPath currentPath, string defaultPath)
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

        public void SaveCurrentPath(CurrentPath currentPath, string fileName)
        {
            _currentPath = JsonSerializer.Serialize(currentPath);
            File.WriteAllText(fileName, _currentPath);
        }
	}
}
