namespace SPEngineCSharp
{
    public abstract class StoreProcedure
    {
        public string Name { get; set; }
        public System.Collections.Generic.List<Parameter> Parameters { get; set; }
        public void Execute()
        {

        }
    }
}