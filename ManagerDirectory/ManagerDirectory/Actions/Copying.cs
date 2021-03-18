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
	    public void Copy(string oldPath, string newPath)
	    {
		    try
		    {
			    if (oldPath.Contains('.'))
			    {
					CopyFiles(oldPath, newPath);

					Console.WriteLine($"Копирование прошло успешно!");
			    }
			    else
			    {
				    foreach (var directory in Directory.GetDirectories(oldPath,"*", SearchOption.TopDirectoryOnly))
					    Directory.CreateDirectory(directory.Replace(oldPath, newPath));

				    CopyFiles(oldPath, newPath);
			    }
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
		    }
	    }

	    private void CopyFiles(string oldPath, string newPath)
	    {
		    foreach (var file in Directory.GetFiles(oldPath, "*.*", SearchOption.AllDirectories))
			    File.Copy(file, newPath.Replace(oldPath, newPath), true);
		}
    }
}
