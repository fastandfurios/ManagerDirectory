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

		public string Deserialize(string fileName, string defaultPath)
		{
			try
			{
				_currentPath = File.ReadAllText(fileName);
				return JsonSerializer.Deserialize<CurrentPath>(_currentPath).Path;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return defaultPath;
		}
	}
}
