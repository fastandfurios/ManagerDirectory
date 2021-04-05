using System;
using System.IO;
using System.Text.Json;
using ManagerDirectory.Models;

namespace ManagerDirectory.Serialization
{
    public class Serializer
    {
	    private string _currentPath;

	    public void Serialize(CurrentPath currentPath, string fileName)
	    {
		    try
		    {
			    _currentPath = JsonSerializer.Serialize(currentPath);
			    File.WriteAllText(fileName, _currentPath);
			}
		    finally{}
	    }
    }
}
