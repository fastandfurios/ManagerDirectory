using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.IO;
using ManagerDirectory.Serialization;

namespace ManagerDirectory
{
    public abstract class Objects
    {
	    protected InputData _input;
	    protected Output _output;
	    protected Serializer _serializer;
	    protected Deserializer _deserializer;
		

			public Objects()
	    {
		    _serializer = new Serializer();
		    _deserializer = new Deserializer();
		    _input = new InputData();
		    _output = new Output();
	    }
    }
}
