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
		    set => _fullPathFile = value;
	    }

	    private string _fullPathDirectory;
	    public string FullPathDirectory
	    {
		    set => _fullPathDirectory = value;
	    }

	    public override string ToString()
	    {
		    try
		    {
			    if (!string.IsNullOrEmpty(_fullPathDirectory) && Path.GetExtension(_fullPathDirectory) == string.Empty)
			    {
				    var directoryInfo = new DirectoryInfo(_fullPathDirectory);

				    int countDirectory =
					    Task.Run(() => directoryInfo.GetDirectories("*", SearchOption.AllDirectories).Length).Result;
				    int countFiles = Task.Run(() => directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Length)
					    .Result;
				    long size = 0;

				    foreach (var file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
					    size += file.Length;

				    return $"Количество папок: {countDirectory}\n" +
				           $"Количество файлов: {countFiles}\n" +
				           $"Размер: {Converter(size)}";
			    }
			    else
			    {
				    var fileInfo = new FileInfo(_fullPathFile);

				    return $"Имя: {Path.GetFileNameWithoutExtension(_fullPathFile)}\n" +
				           $"Расширение: {fileInfo.Extension}\n" +
				           $"Размер: {Converter(fileInfo.Length)}";
			    }
		    }
		    finally{}
	    }

		/// <summary>
		/// Конвертирует размер файлов и папок в читаемый вид
		/// </summary>
		/// <param name="size">Размерность</param>
		/// <returns>Готовая строка</returns>
		private string Converter(long size)
		{
			if (size < 1024)
				return $"{size.ToString()} B";
			else if (1024 < size && size < 1_048_576)
				return $"{((double)size / 1024).ToString("F")} KB";
			else if (1_048_576 < size && size < 1_073_741_824)
				return $"{((double)size / 1_048_576).ToString("F")} MB";
			else if (size > 1_073_741_824)
				return $"{((double)size / 1_073_741_824).ToString("F")} GB";

			return default;
		}
    }
}
