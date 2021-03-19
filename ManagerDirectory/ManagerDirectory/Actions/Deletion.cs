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

	    private string _path;
	    public string Path
	    {
		    get => _path; 
		    set
		    {
			    _path = value;
				Delete();
				Directory.Delete(_path);
				Console.WriteLine("Удаление прошло успешно!");
			} 
	    }

        private void Delete()
		{
			try
			{
				_countFiles = Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories).Length;
				_countDirectory = Directory.GetDirectories(_path, "*", SearchOption.AllDirectories).Length;

				if (_countFiles != 0)
				{
					File.Delete(Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories)[_countFiles - 1]);
					Delete();
				}

				if (_countDirectory != 0)
				{
					Directory.Delete(Directory.GetDirectories(_path, "*", SearchOption.AllDirectories)[_countDirectory - 1]);
					Delete();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
    }
}
