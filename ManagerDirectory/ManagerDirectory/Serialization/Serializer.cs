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
    public class Serializer
    {
	    private string _currentPath;

	    public void Serialize(CurrentPath currentPath, string fileName)
	    {
		    _currentPath = JsonSerializer.Serialize(currentPath);
		    File.WriteAllText(fileName, _currentPath);
	    }
    }
}
