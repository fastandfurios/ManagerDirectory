using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Repository;

namespace ManagerDirectory.Validation
{
    public class Checker
    {
	    private Commands _commands = new Commands();

		public bool CheckInputCommand(string nameCommand)
		{
			foreach (var command in _commands.ArrayCommands)
			{
				if (command == nameCommand.Split(" ")[0] && nameCommand != string.Empty)
					return true;
			}

			Console.Clear();
			return false;
		}
	}
}
