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
	    private string _empty;
	    private Checker _checker = new Checker();

	    public string Input(string defaultPath)
	    {
		    do
		    {
				Console.Write($"{defaultPath} > ");
				_empty = Console.ReadLine();
			} while (!_checker.CheckInputCommand(_empty));

		    return _empty;
	    }
    }
}
