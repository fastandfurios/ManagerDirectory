using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.IO
{
    public class Output
    {
	    private DirectoryInfo _directory;

	    public void OutputTree(string path)
	    {
		    try
		    {
			    string newPath = path.Split(" ")[1];
			    _directory = new DirectoryInfo(newPath);
			    Console.WriteLine(" " + newPath);
			    foreach (var directory in _directory.GetDirectories())
				    Console.WriteLine($" |{new string('-', newPath.Length)}{directory.Name.Replace($"{newPath}", "")}");
			}
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
		    }
	    }
    }
}
