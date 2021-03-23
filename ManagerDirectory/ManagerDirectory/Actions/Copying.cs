using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.Actions
{
    public class Copying
    {
	    public void Copy(string oldPath, string name, string newPath)
	    {
		    if (Path.GetExtension(name) != string.Empty)
		    {
			    foreach (var file in Directory.GetFiles(oldPath, name, SearchOption.TopDirectoryOnly))
				    File.Copy(file, file.Replace(oldPath, newPath), true);

			    Console.WriteLine($"Копирование прошло успешно!");
		    }
		    else
		    {
			    foreach (var directory in Directory.GetDirectories(oldPath, name, SearchOption.TopDirectoryOnly))
				    Directory.CreateDirectory(directory.Replace(oldPath, newPath));

			    foreach (var file in Directory.GetFiles(oldPath + name, "*.*", SearchOption.TopDirectoryOnly))
				    File.Copy(file, file.Replace(oldPath, newPath), true);

			    Console.WriteLine($"Копирование прошло успешно!");
		    }
	    }
    }
}
