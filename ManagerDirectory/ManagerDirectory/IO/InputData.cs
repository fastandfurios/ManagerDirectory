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

	    public string Input(string defaultPath, Checker checker)
	    {
		    do
		    {
				Console.Write($"{defaultPath} > ");
				_entry = Console.ReadLine();
			} while (!checker.CheckInputCommand(_entry));

		    return _entry;
	    }
    }
}
