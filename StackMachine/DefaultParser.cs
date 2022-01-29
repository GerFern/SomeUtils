using StackMachine.Symbol;

namespace StackMachine
{
    public static class DefaultParser
    {
        public static IEnumerable<KeyValuePair<string, string>> GetDefaultDefs()
        {
            yield return new KeyValuePair<string, string>("ts", "\\\\\\\"");
            yield return new KeyValuePair<string, string>("word", @"(?=[^\d])\w+");
            yield return new KeyValuePair<string, string>("op", @"[+\-*\/]|!=|==|>=|<=|>|<|=");
            yield return new KeyValuePair<string, string>("s_start", @"\(");
            yield return new KeyValuePair<string, string>("s_end", @"\)");
            yield return new KeyValuePair<string, string>("digit", @"[\d.]+");
            yield return new KeyValuePair<string, string>("char", @"\'.\'");
            yield return new KeyValuePair<string, string>("string", "\\\"((?&def_ts)|[^\\\"])*\\\"");
            yield return new KeyValuePair<string, string>("var_f", @"(?&def_word)\(\s*\)?");
            yield return new KeyValuePair<string, string>("var", @"(?&def_word)");
            yield return new KeyValuePair<string, string>("s_arg", ",");
        }

        public static IEnumerable<KeyValuePair<string, Func<string, ISymbol[]?>>> GetDefaultSymbolCreators()
        {
            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "op",
                a => a switch
                {
                    "+" => new[] { new Operation(OperationType.BinaryAdd) },
                    "-" => new[] { new Operation(OperationType.BinarySub) },
                    "*" => new[] { new Operation(OperationType.BinaryMul) },
                    "/" => new[] { new Operation(OperationType.BinaryDiv) },
                    "!=" => new[] { new Operation(OperationType.BinaryNotEqual) },
                    "==" => new[] { new Operation(OperationType.BinaryEqual) },
                    ">=" => new[] { new Operation(OperationType.BinaryGreaterOrEqual) },
                    "<=" => new[] { new Operation(OperationType.BinaryLowerOrEqual) },
                    ">" => new[] { new Operation(OperationType.BinaryGreater) },
                    "<" => new[] { new Operation(OperationType.BinaryLower) },
                    "=" => new[] { new Operation(OperationType.Assign) },
                    _ => throw new Exception()
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "s_start",
                a => new[] { new Scobe { Type = Scobe.ScobeType.Start } }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "s_end",
                a => new[] { new Scobe { Type = Scobe.ScobeType.End } }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "digit",
                a => new[]
                {
                    a.Contains('.') ?
                    new Variable
                    {
                        VariableType = VariableType.Double,
                        Value = double.Parse(a)
                    } :
                    new Variable
                    {
                        VariableType = VariableType.Int64,
                        Value = long.Parse(a)
                    }
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
               "char",
                a => new[]
                {
                    new Variable
                    {
                        VariableType = VariableType.Char,
                        Value = a[1]
                    }
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
               "string",
                a => new[]
                {
                    new Variable
                    {
                        VariableType = VariableType.String,
                        Value = a[1..^1].Replace("\\\"","\"")
                    }
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
               "var_f",
                a =>
                {
                    ISymbol[] vs;
                    if (a.EndsWith(')'))
                    {
                        vs = new ISymbol[]
                        {
                        new NameVariable
                        {
                            Name = a[..a.IndexOf('(')],
                            IsFunc = true,
                            EmptyArg = true
                        }
                        };
                    }
                    else
                    {
                        var f = new NameVariable
                        {
                            Name = a[..a.IndexOf('(')],
                            IsFunc = true,
                            ArgCount = 1
                        };
                        vs = new ISymbol[]
                        {
                        f,
                        new Scobe
                        {
                            Type = Scobe.ScobeType.Start,
                            Func = f
                        }
                        };
                    }
                    return vs;
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "var",
                a => new[]
                {
                    new NameVariable
                    {
                        Name = a
                    }
                }
            );

            yield return new KeyValuePair<string, Func<string, ISymbol[]?>>(
                "s_arg",
                a => new[] { new SplitArg() }
            );
        }
    }
}