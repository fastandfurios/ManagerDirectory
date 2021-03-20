using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManagerDirectory.Serialization
{
    public class Serializer
    {
	    private string _currentPath;

	    public void Serialize(string currentPath, string fileName)
	    {
		    try
		    {
			    _currentPath = JsonSerializer.Serialize(currentPath);
				File.WriteAllText(fileName, _currentPath);
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
		    }
	    }
    }
}
