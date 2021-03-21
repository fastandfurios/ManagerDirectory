using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.Actions
{
    public class Informer
    {
	    private string _fullPathFile;
	    public string FullPathFile
	    {
		    get => _fullPathFile;
		    set => _fullPathFile = value;
	    }

	    private string _fullPathDirectory;
	    public string FullPathDirectory
	    {
		    get => _fullPathDirectory;
		    set => _fullPathDirectory = value;
	    }

	    public override string ToString()
	    {
		    var fileInfo = new FileInfo(_fullPathFile);

		    if (!string.IsNullOrEmpty(_fullPathDirectory))
		    {
			    return $"";
		    }
		    else
		    {
			    return $"Имя: {Path.GetFileNameWithoutExtension(_fullPathFile)}\n" +
			           $"Расширение: {fileInfo.Extension}\n" +
			           $"Объем занимаемой памяти: {fileInfo.Length} байт";
		    }
	    }
    }
}
