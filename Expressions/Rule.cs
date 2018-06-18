namespace NetCoreLog.Expressions
{
    public class Rule : Expression
    {
        public Rule(Expression condition, Fact result)
        {
            Condition = condition;
            Result = result;
        }

        public Expression Condition { get; }

        public Fact Result { get; }

        public override string ToString()
        {
            return $"rule {Condition} -> {Result}";
        }
    }
}