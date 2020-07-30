using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEngineCSharp
{
    public class StringConnectionNotDefinedException : Exception
    {
        public StringConnectionNotDefinedException() { }

        public StringConnectionNotDefinedException(string message)
            : base(message) { }

        public StringConnectionNotDefinedException(string message, Exception inner)
            : base(message, inner) { }
    }
}
