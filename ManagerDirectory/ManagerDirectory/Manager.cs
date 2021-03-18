using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory
{
    public class Manager : Objects
    {
	    public void Run()
	    {
		    Console.Title = "ManagerDirectory";

			string path = _input.InputCommand();
			CallOutput(path);
	    }

		private void CallOutput(string path)
		{
			_output.OutputTree(path);
		}
    }
}
