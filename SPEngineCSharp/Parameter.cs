using System;
using System.Data;

namespace SPEngineCSharp
{
    public class Parameter
    {
        public String Name { get; set; }
        public Object Value { get; set; }
        public SqlDbType DataType { get; set; }
        public Int32 Size { get; set; }
        public ParameterDirection Direction { get; set; }

        //Input Parameter
        public Parameter(String name, Object value)
        {
            this.Name = name;
            this.Value = value;
            this.Direction = ParameterDirection.Input;
        }
        //OuputParameter
        public Parameter(String name, SqlDbType DataType, Int32 Size)
        {
            this.Name = name;
            this.DataType = DataType;
            this.Size = Size;
            this.Direction = ParameterDirection.Output;
        }
    }
}
