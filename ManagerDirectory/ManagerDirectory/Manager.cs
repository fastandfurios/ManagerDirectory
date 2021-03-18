using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory
{
    public class Manager : Objects
    {
	    private string _empty;

	    public void Run()
	    {
		    _empty = _input.Input();

			ToDistribute();
	    }

	    private void ToDistribute()
	    {
		    try
		    {
			    string command = _empty.Split(" ")[0];
			    string path = _empty.Split(" ")[1];
			    string newPath = _empty.Split(" ")[2];

			    switch (command)
			    {
				    case "ls":
					    CallOutput(path);
					    break;
				    case "cp":
					    CallCopying(path, newPath);
					    break;
					case "clear":
						Console.Clear();
						Run();
						break;
					case "cd":
						Move();
						break;
			    }
		    }
		    catch
		    {
				Console.Clear();
			    Run();
		    }
	    }

		private void CallOutput(string path)
		{
			_output.OutputTree(path);
		}

		private void CallCopying(string path, string newPath)
		{
			_copying.Copy(path, newPath);
		}

		private void Move()
		{

		}
    }
}
