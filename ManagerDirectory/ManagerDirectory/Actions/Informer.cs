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
		    if (!string.IsNullOrEmpty(_fullPathDirectory))
		    {
			    var directoryInfo = new DirectoryInfo(_fullPathDirectory);
				int countDirectory = directoryInfo.GetDirectories("*", SearchOption.AllDirectories).Length;
			    int countFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Length;
			    long size = 0;
				foreach (var file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
					size += file.Length;
				//TODO размер поправить
			    return $"Количество папок: {countDirectory}\n" +
			           $"Количество файлов: {countFiles}\n" +
			           $"Размер: {size} байт";
		    }
		    else
		    {
			    var fileInfo = new FileInfo(_fullPathFile);
				return $"Имя: {Path.GetFileNameWithoutExtension(_fullPathFile)}\n" +
			           $"Расширение: {fileInfo.Extension}\n" +
			           $"Размер: {fileInfo.Length} байт";
		    }
	    }
    }
}
