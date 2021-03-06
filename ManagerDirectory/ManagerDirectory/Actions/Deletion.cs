using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.Actions
{
    public class Deletion
    {
	    private int _countFiles;
	    private int _countDirectory;

	    private string _fullPathDirectory;
	    public string FullPathDirectory
	    {
		    get => _fullPathDirectory; 
		    set
		    {
			    _fullPathDirectory = value;

				Delete();

				Directory.Delete(_fullPathDirectory);
				Console.WriteLine("Удаление прошло успешно!");
		    } 
	    }

	    private string _fullPathFile;
	    public string FullPathFile
	    {
		    get => _fullPathFile;
		    set
		    {
			    _fullPathFile = value;

			    File.Delete(_fullPathFile);
			    Console.WriteLine("Удаление прошло успешно!");
			}
	    }

		/// <summary>
		/// Удаляет все директории и файлы в указанной директории
		/// </summary>
        private void Delete()
		{
			_countFiles = Directory.GetFiles(_fullPathDirectory, "*.*", SearchOption.AllDirectories).Length;
			_countDirectory = Directory.GetDirectories(_fullPathDirectory, "*", SearchOption.AllDirectories).Length;

			if (_countFiles != 0)
			{
				File.Delete(
					Directory.GetFiles(_fullPathDirectory, "*.*", SearchOption.AllDirectories)[_countFiles - 1]);
				Delete();
			}

			if (_countDirectory != 0)
			{
				Directory.Delete(
					Directory.GetDirectories(_fullPathDirectory, "*",
						SearchOption.AllDirectories)[_countDirectory - 1]);
				Delete();
			}
			
		}
    }
}
