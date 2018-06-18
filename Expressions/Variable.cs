namespace NetCoreLog.Expressions
{
    public class Variable : Expression
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"var:{Name}";
        }
    }
}