namespace NetCoreLog.Expressions
{
    public class Atom : Expression
    {
        public Atom(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"atom:{Name}";
        }
    }
}