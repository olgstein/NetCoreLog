using System.Collections.Generic;
using System.Text;

namespace NetCoreLog.Expressions
{
    public class Fact : Expression
    {
        private List<Expression> _parameters = new List<Expression>();

        public Fact(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public IEnumerable<Expression> Parameters { get => _parameters; }

        public void AddParameter(Atom atom)
        {
            _parameters.Add(atom);
        }

        public void AddParameter(Variable variable)
        {
            _parameters.Add(variable);
        }

        public override string ToString()
        {
            var sb = new StringBuilder("(Fact ").Append(Name);

            foreach (var parameter in Parameters)
            {
                sb.Append(' ').Append(parameter.ToString());
            }

            sb.Append(')');

            return sb.ToString();
        }
    }
}