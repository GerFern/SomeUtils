using MathOperations;
using MathOperations.Extensions;
using System.Numerics;

namespace StackMachine
{
    public class EngineExecutor
    {
        public Dictionary<string, ISymbol> Variables { get; } = new();
        public Parser Parser { get; set; }
        public SortStation SortStation { get; set; }
        public EngineExecutor() : this(new Parser(), new SortStation())
        {
        }

        public EngineExecutor(Parser parser, SortStation sortStation)
        {
            Parser = parser;
            SortStation = sortStation;

            foreach (var item in DefaultVariables.GetDefaultVariables())
            {
                Variables[item.Key] = item.Value;
            }
        }


        public Variable Execute(IEnumerable<ISymbol> symbols)
        {
            ExecutorStorage storage = new();
            foreach (ISymbol symbol in symbols)
            {
                ExecuteSymbol(storage, symbol);
            }
            return storage.VariableStack.Pop();
        }

        public Variable Execute(string input)
        {
            return Execute(input, Parser, SortStation);
        }

        public Variable Execute(string input, Parser parser, SortStation sortStation)
        {
#if DEBUG
            var p = parser.Parse(input).ToArray();
            var s = sortStation.Sort(p).ToArray();
            return Execute(s);
#else
            return Execute(sortStation.Sort(parser.Parse(input)));
#endif
        }

        void ExecuteSymbol(ExecutorStorage storage, ISymbol symbol)
        {
            try
            {
                if (symbol is NameVariable nameVariable)
                {
                    var symbol2 = Variables[nameVariable.Name];
                    if(symbol2 is VariableFunc func)
                    {
                        ExecuteFunc(storage, func with { Range = symbol.Range }, nameVariable.ArgCount);
                    }
                    else ExecuteSymbol(storage, Variables[nameVariable.Name]);
                }
                if (symbol is Variable variable)
                {
                    storage.VariableStack.Push(variable);
                }
                else if (symbol is Operation operation)
                {
                    switch (operation.OperationType)
                    {
                        case OperationType.BinaryAdd:
                            ExecuteAdd(storage);
                            break;
                        case OperationType.BinarySub:
                            ExecuteSub(storage);
                            break;
                        case OperationType.BinaryMul:
                            ExecuteMul(storage);
                            break;
                        case OperationType.BinaryDiv:
                            ExecuteDiv(storage);
                            break;
                        case OperationType.Call:
                            break;
                        default:
                            break;
                    }
                }
                else if(symbol is VariableFunc func)
                {
                    ExecuteFunc(storage, func, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + Environment.NewLine + symbol.ToString());
            }
        }

        void ExecuteFunc(ExecutorStorage storage, VariableFunc func, int? variableCount)
        {
            int length = (variableCount ?? func.ArgCount);
            Variable[] vs = new Variable[length];
            for (int i = length - 1; i >= 0; i--)
            {
                vs[i] = storage.VariableStack.Pop();
            }

            storage.VariableStack.Push(func.Func(vs));
        }


        void ExecuteAdd(ExecutorStorage storage)
        {
            Variable v2 = storage.VariableStack.Pop();
            Variable v1 = storage.VariableStack.Pop();
            VariableType variableType = v1.VariableType;
            object ret = null;
            if(v1.VariableType == VariableType.String || v2.VariableType == VariableType.String)
            {
                ret = v1.Value.ToString() + v2.Value.ToString();
                variableType = VariableType.String;
            }
            else if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        ret = ((bool)v1.Value).Add((bool)v2.Value);
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value).Add((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value).Add((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value).Add((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value).Add((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value).Add((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value).Add((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value).Add((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value).Add((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value).Add((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value).Add((double)v2.Value);
                        break;
                    case VariableType.Char:
                        if (v2.VariableType == VariableType.Char)
                            ret = ((char)v1.Value) + ((char)v2.Value);
                        else if (v2.VariableType == VariableType.String)
                            ret = ((char)v1.Value) + ((string)v2.Value);
                        else
                            ret = ((char)v1.Value) + v2.Value.ToString();
                        variableType = VariableType.String;
                        break;
                    case VariableType.String:
                        if (v2.VariableType == VariableType.Char)
                            ret = ((string)v1.Value) + ((char)v2.Value);
                        else if (v2.VariableType == VariableType.String)
                            ret = ((string)v1.Value) + ((string)v2.Value);
                        else
                            ret = ((string)v1.Value) + v2.Value.ToString();
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }

            

            Variable variable = new Variable
            {
                VariableType = variableType,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteSub(ExecutorStorage storage)
        {
            Variable v2 = storage.VariableStack.Pop();
            Variable v1 = storage.VariableStack.Pop();
            VariableType variableType = v1.VariableType;
            object ret = null;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            if (variableType == VariableType.Object || variableType == VariableType.RangeIndex || variableType == VariableType.Char || variableType == VariableType.String) throw new Exception();

            switch (variableType)
            {
                case VariableType.Boolean:
                    ret = ((bool)v1.Value).Sub((bool)v2.Value);
                    break;
                case VariableType.Byte:
                    ret = ((byte)v1.Value).Sub((byte)v2.Value);
                    break;
                case VariableType.SByte:
                    ret = ((sbyte)v1.Value).Sub((sbyte)v2.Value);
                    break;
                case VariableType.Int16:
                    ret = ((short)v1.Value).Sub((short)v2.Value);
                    break;
                case VariableType.UInt16:
                    ret = ((ushort)v1.Value).Sub((ushort)v2.Value);
                    break;
                case VariableType.Int32:
                    ret = ((int)v1.Value).Sub((int)v2.Value);
                    break;
                case VariableType.UInt32:
                    ret = ((uint)v1.Value).Sub((uint)v2.Value);
                    break;
                case VariableType.Int64:
                    ret = ((long)v1.Value).Sub((long)v2.Value);
                    break;
                case VariableType.UInt64:
                    ret = ((ulong)v1.Value).Sub((ulong)v2.Value);
                    break;
                case VariableType.Single:
                    ret = ((float)v1.Value).Sub((float)v2.Value);
                    break;
                case VariableType.Double:
                    ret = ((double)v1.Value).Sub((double)v2.Value);
                    break;
                default:
                    throw new Exception();
                    break;
            }

            Variable variable = new Variable
            {
                VariableType = variableType,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteMul(ExecutorStorage storage)
        {
            Variable v1 = storage.VariableStack.Pop();
            Variable v2 = storage.VariableStack.Pop();
            VariableType variableType = v1.VariableType;
            object ret = null;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            if (variableType == VariableType.Object || variableType == VariableType.RangeIndex || variableType == VariableType.Char || variableType == VariableType.String) throw new Exception();

            switch (variableType)
            {
                case VariableType.Boolean:
                    ret = ((bool)v1.Value).Mul((bool)v2.Value);
                    break;
                case VariableType.Byte:
                    ret = ((byte)v1.Value).Mul((byte)v2.Value);
                    break;
                case VariableType.SByte:
                    ret = ((sbyte)v1.Value).Mul((sbyte)v2.Value);
                    break;
                case VariableType.Int16:
                    ret = ((short)v1.Value).Mul((short)v2.Value);
                    break;
                case VariableType.UInt16:
                    ret = ((ushort)v1.Value).Mul((ushort)v2.Value);
                    break;
                case VariableType.Int32:
                    ret = ((int)v1.Value).Mul((int)v2.Value);
                    break;
                case VariableType.UInt32:
                    ret = ((uint)v1.Value).Mul((uint)v2.Value);
                    break;
                case VariableType.Int64:
                    ret = ((long)v1.Value).Mul((long)v2.Value);
                    break;
                case VariableType.UInt64:
                    ret = ((ulong)v1.Value).Mul((ulong)v2.Value);
                    break;
                case VariableType.Single:
                    ret = ((float)v1.Value).Mul((float)v2.Value);
                    break;
                case VariableType.Double:
                    ret = ((double)v1.Value).Mul((double)v2.Value);
                    break;
                default:
                    throw new Exception();
                    break;
            }

            Variable variable = new Variable
            {
                VariableType = variableType,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteDiv(ExecutorStorage storage)
        {
            Variable v2 = storage.VariableStack.Pop();
            Variable v1 = storage.VariableStack.Pop();
            VariableType variableType = v1.VariableType;
            object ret = null;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            if (variableType == VariableType.Object || variableType == VariableType.RangeIndex || variableType == VariableType.Char || variableType == VariableType.String) throw new Exception();

            switch (variableType)
            {
                case VariableType.Boolean:
                    ret = ((bool)v1.Value).Div((bool)v2.Value);
                    break;
                case VariableType.Byte:
                    ret = ((byte)v1.Value).Div((byte)v2.Value);
                    break;
                case VariableType.SByte:
                    ret = ((sbyte)v1.Value).Div((sbyte)v2.Value);
                    break;
                case VariableType.Int16:
                    ret = ((short)v1.Value).Div((short)v2.Value);
                    break;
                case VariableType.UInt16:
                    ret = ((ushort)v1.Value).Div((ushort)v2.Value);
                    break;
                case VariableType.Int32:
                    ret = ((int)v1.Value).Div((int)v2.Value);
                    break;
                case VariableType.UInt32:
                    ret = ((uint)v1.Value).Div((uint)v2.Value);
                    break;
                case VariableType.Int64:
                    ret = ((long)v1.Value).Div((long)v2.Value);
                    break;
                case VariableType.UInt64:
                    ret = ((ulong)v1.Value).Div((ulong)v2.Value);
                    break;
                case VariableType.Single:
                    ret = ((float)v1.Value).Div((float)v2.Value);
                    break;
                case VariableType.Double:
                    ret = ((double)v1.Value).Div((double)v2.Value);
                    break;
                default:
                    throw new Exception();
                    break;
            }

            Variable variable = new Variable
            {
                VariableType = variableType,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        class ExecutorStorage
        {
            public Stack<Variable> VariableStack { get; } = new();
        }
    }

    public interface ISymbol
    {
        public Range Range { get; set; }
    }

    public record Scobe : ISymbol
    {
        public ScobeType Type { get; init; }
        public NameVariable? Func { get; set; }
        public Range Range { get; set; }

        public enum ScobeType
        {
            Start,
            End
        }
    }


    public record Variable : ISymbol
    {
        public VariableType VariableType { get; init; }
        public Type? Type { get; init; }
        public virtual object Value { get; init; }
        public Range Range { get; set; }
    }

    public record VariableGet : Variable
    {
        public Func<object> Func { get; init; }
        public override object Value { get => Func.Invoke(); init { } }
    }

    public record VariableFunc : ISymbol
    {
        public Func<Variable[], Variable> Func { get; init; }
        public int ArgCount { get; set; }
        public Range Range { get; set; }
    }

    public record SplitArg : ISymbol
    {
        public Range Range { get; set; }
    }

    public record NameVariable : ISymbol
    {
        public string Name { get; init; }
        public bool IsFunc { get; init; }
        public bool EmptyArg { get; init; }
        public int ArgCount { get; set; }
        public Range Range { get; set; }
    }

    public enum VariableType
    {
        Object,
        Boolean,
        Byte,
        SByte,
        Int16,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Single,
        Double,
        Char,
        String,
        RangeIndex
    }

    public record Operation : ISymbol
    {
        public int Priority { get; init; }
        public OperationType OperationType { get; init; }
        public Range Range { get; set; }
    }

    public enum OperationType
    {
        BinaryAdd,
        BinarySub,
        BinaryMul,
        BinaryDiv,
        Call
    }
}