using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Actions;
using ManagerDirectory.IO;
using ManagerDirectory.Models;
using ManagerDirectory.Repository;
using ManagerDirectory.Serialization;
using ManagerDirectory.Validation;

namespace ManagerDirectory
{
    public abstract class Objects
    {
		#region Fields
		protected readonly InputData Input;
		protected readonly Output Output;
		protected readonly Copying Copying;
		protected readonly Deletion Deletion;
		protected readonly Serializer Serializer;
		protected readonly Deserializer Deserializer;
		protected CurrentPath CurrentPath;
		protected readonly Informer Informer;
		protected readonly Checker Checker;
		#endregion

		protected Objects()
	    {
		    Serializer = new Serializer();
		    Deserializer = new Deserializer();
		    Input = new InputData();
		    Output = new Output();
		    Copying = new Copying();
		    Deletion = new Deletion();
			CurrentPath = new CurrentPath();
			Informer = new Informer();
			Checker = new Checker();
		}
    }
}
