using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDirectory.Repository
{
	public class Commands
	{
		public string[] ArrayCommands => new string[]
		{
			"ls",
			"cp",
			"rm",
			"info",
			"clear",
			"cd"
		};
}
}
