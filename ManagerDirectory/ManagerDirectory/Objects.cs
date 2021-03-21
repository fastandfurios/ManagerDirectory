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

namespace ManagerDirectory
{
    public abstract class Objects
    {
	    protected InputData _input;
	    protected Output _output;
	    protected Copying _copying;
	    protected Deletion _deletion;
	    protected Serializer _serializer;
	    protected Deserializer _deserializer;
	    protected CurrentPath _currentPath;


	    protected Objects()
	    {
		    _serializer = new Serializer();
		    _deserializer = new Deserializer();
		    _input = new InputData();
		    _output = new Output();
		    _copying = new Copying();
		    _deletion = new Deletion();
		    _currentPath = new CurrentPath();
	    }
    }
}
