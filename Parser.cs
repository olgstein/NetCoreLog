using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCoreLog.Expressions;

namespace NetCoreLog
{
    public class Parser
    {
        public Expression Parse(string code)
        {
            var variables = new Dictionary<string, Variable>();
            var tokens = Tokenize(code).GetEnumerator();

            var fact = ParseFact(tokens, variables);

            if (!tokens.MoveNext()) return fact;

            Expression left = fact;
            
            while (tokens.Current.Type == TokenType.And)
            {
                left = new BinaryExpression(left, ParseFact(tokens, variables));

                if (!tokens.MoveNext()) throw new NetCoreLogException("1- Unexpected end of code");
            }

            if (tokens.Current.Type != TokenType.Arrow) throw new NetCoreLogException($"2- Arrow expected, was {tokens.Current.Type}");

            return new Rule(left, ParseFact(tokens, variables));
        }

        private Fact ParseFact(IEnumerator<(TokenType Type, string Value)> tokens, Dictionary<string, Variable> variables)
        {
            if (!tokens.MoveNext()) throw new NetCoreLogException("3- Unexpected end of code");
            if (tokens.Current.Type != TokenType.Identifier) throw new NetCoreLogException($"4- Identifier expected, was {tokens.Current.Type}");

            var fact = new Fact(tokens.Current.Value);

            if (!tokens.MoveNext()) throw new NetCoreLogException("4- Unexpected end of code");
            if (tokens.Current.Type != TokenType.LeftBraket) throw new NetCoreLogException($"5- LeftBraket expected, was {tokens.Current.Type}");

            while (tokens.MoveNext() && tokens.Current.Type != TokenType.RightBraket)
            {
                if (tokens.Current.Type != TokenType.Identifier) throw new NetCoreLogException($"6- Identifier expected, was {tokens.Current.Type}");

                if (tokens.Current.Value.Any(c => !char.IsUpper(c)))
                {
                    fact.AddParameter(new Atom(tokens.Current.Value));
                }
                else
                {
                    fact.AddParameter(
                        variables.ContainsKey(tokens.Current.Value) ? 
                        variables[tokens.Current.Value] : 
                        variables[tokens.Current.Value] = new Variable(tokens.Current.Value));
                }
            }

            return fact;
        }

        private IEnumerable<(TokenType Type, string Value)> Tokenize(IEnumerable<char> code)
        {
            var currentIdentifier = new StringBuilder();
            foreach(var c in code)
            {
                if (c == '(' || c == ')' || c == '&')
                {
                    if (currentIdentifier.Length > 0)
                    {
                        yield return IdentifierOrArrow(currentIdentifier.ToString());
                        currentIdentifier.Clear();
                    }
                    yield return ((TokenType)c, null);
                }
                else if (char.IsWhiteSpace(c) || c == ',')
                {
                    if (currentIdentifier.Length > 0)
                    {
                        yield return IdentifierOrArrow(currentIdentifier.ToString());
                        currentIdentifier.Clear();
                    }
                }
                else
                    currentIdentifier.Append(c);
            }

            if (currentIdentifier.Length > 0) yield return IdentifierOrArrow(currentIdentifier.ToString());
        }

        private (TokenType, string) IdentifierOrArrow(string identifier)
        {
            if (identifier == "->") return (TokenType.Arrow, null);
            return (TokenType.Identifier, identifier);
        }

        private enum TokenType
        {
            Identifier = 0,
            Arrow = 1,
            And = 38,
            LeftBraket = 40,
            RightBraket = 41
        }
    }
}