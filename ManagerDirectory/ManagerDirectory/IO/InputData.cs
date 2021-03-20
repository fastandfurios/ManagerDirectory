using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Repository;
using ManagerDirectory.Validation;

namespace ManagerDirectory.IO
{
    public class InputData
    {
	    private string _entry;
	    private Checker _checker = new Checker();

	    public string Input(string defaultPath)
	    {
		    do
		    {
				Console.Write($"{defaultPath} > ");
				_entry = Console.ReadLine();
			} while (!_checker.CheckInputCommand(_entry));

		    return _entry;
	    }
    }
}
