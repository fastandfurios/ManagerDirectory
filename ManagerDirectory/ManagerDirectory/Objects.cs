using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Actions;
using ManagerDirectory.IO;
using ManagerDirectory.Models;
using ManagerDirectory.Repository;
using ManagerDirectory.Validation;

namespace ManagerDirectory
{
    public abstract class Objects
    {
	    protected InputData _input;
	    protected Output _output;
	    protected Copying _copying;
	    protected Deletion _deletion;
	    protected ManagerRepository _managerRepository;
	    protected CurrentPath _currentPath;
	    protected Informer _informer;
	    protected Checker _checker;


		protected Objects()
	    {
		    _managerRepository = new ManagerRepository();
		    _input = new InputData();
		    _output = new Output();
		    _copying = new Copying();
		    _deletion = new Deletion();
			_currentPath = new CurrentPath();
			_informer = new Informer();
			_checker = new Checker();
		}
    }
}
