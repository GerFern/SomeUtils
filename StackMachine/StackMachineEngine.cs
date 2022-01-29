using MathOperations;
using MathOperations.Extensions;
using StackMachine.Symbol;
using System.Collections.Immutable;
using System.Numerics;

namespace StackMachine
{
    public class StackMachineEngine : ICloneable
    {
        public Dictionary<string, ISymbol> Variables { get; } = new();
        public Dictionary<string, ISymbol> TempVariables { get; } = new();
        public Parser Parser { get; set; }
        public SortStation SortStation { get; set; }
        public StackMachineEngine() : this(new Parser(), new SortStation())
        {
        }

        public StackMachineEngine(Parser parser, SortStation sortStation)
        {
            Parser = parser;
            SortStation = sortStation;

            foreach (var item in DefaultVariables.GetDefaultVariables())
            {
                Variables[item.Key] = item.Value;
            }
        }

        public void ClearTempVariables() => TempVariables.Clear();


        public Variable? Execute(IEnumerable<ISymbol> symbols)
        {
            StackMachineStorage storage = new()
            {
                Owner = this
            };
            foreach (ISymbol symbol in symbols)
            {
                ExecuteSymbol(storage, symbol);
            }
            if (storage.VariableStack.Count > 0)
                return storage.PopVariable();
            else return null;
        }

        public Variable? Execute(string input)
        {
            return Execute(input, Parser, SortStation);
        }

        public Variable? Execute(string input, Parser parser, SortStation sortStation)
        {
#if DEBUG
            var p = parser.Parse(input).ToArray();
            var s = sortStation.Sort(p).ToArray();
            return Execute(s);
#else
            return Execute(sortStation.Sort(parser.Parse(input)));
#endif
        }

        void ExecuteSymbol(StackMachineStorage storage, ISymbol symbol)
        {
            try
            {
                if (symbol is NameVariable nameVariable)
                {
                    if(TempVariables.TryGetValue(nameVariable.Name, out var symbol2) || Variables.TryGetValue(nameVariable.Name, out symbol2))
                    {
                        if (symbol2 is VariableFunc func)
                        {
                            ExecuteFunc(storage, func with { Range = symbol.Range }, nameVariable.ArgCount);
                        }
                        else if (symbol2 is Variable) storage.PushVariable(symbol);
                        else ExecuteSymbol(storage, symbol2);
                    }
                    else storage.PushVariable(symbol);
                    //var symbol2 = Variables[nameVariable.Name];
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
                        case OperationType.BinaryGreater:
                            ExecuteGreater(storage);
                            break;
                        case OperationType.BinaryGreaterOrEqual:
                            ExecuteGreaterOrEquals(storage);
                            break;
                        case OperationType.BinaryLower:
                            ExecuteLower(storage);
                            break;
                        case OperationType.BinaryLowerOrEqual:
                            ExecuteLowerOrEquals(storage);
                            break;
                        case OperationType.BinaryEqual:
                            ExecuteEquals(storage);
                            break;
                        case OperationType.BinaryNotEqual:
                            ExecuteNotEquals(storage);
                            break;
                        case OperationType.Assign:
                            ExecuteAssign(storage);
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

        void ExecuteFunc(StackMachineStorage storage, VariableFunc func, int? variableCount)
        {
            int length = (variableCount ?? func.ArgCount);
            Variable[] vs = new Variable[length];
            for (int i = length - 1; i >= 0; i--)
            {
                vs[i] = storage.PopVariable();
            }

            FuncInput funcInput = new()
            {
                Args = vs.ToImmutableArray(),
                Storage = storage
            };
            storage.VariableStack.Push(func.Func(funcInput));
        }


        void ExecuteAdd(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
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

        void ExecuteSub(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
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

        void ExecuteMul(StackMachineStorage storage)
        {
            Variable v1 = storage.PopVariable();
            Variable v2 = storage.PopVariable();
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

        void ExecuteDiv(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
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

        void ExecuteEquals(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        ret = ((bool)v1.Value) == ((bool)v2.Value);
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) == ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) == ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) == ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) == ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) == ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) == ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) == ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) == ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) == ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) == ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) == ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value) == ((string)v2.Value);
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteNotEquals(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        ret = ((bool)v1.Value) != ((bool)v2.Value);
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) != ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) != ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) != ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) != ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) != ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) != ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) != ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) != ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) != ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) != ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) != ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value) != ((string)v2.Value);
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteGreaterOrEquals(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        bool leftBool = (bool)v1.Value, rightBool = (bool)v2.Value;
                        ret = leftBool || !rightBool;
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) >= ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) >= ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) >= ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) >= ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) >= ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) >= ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) >= ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) >= ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) >= ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) >= ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) >= ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value).CompareTo((string)v2.Value) >= 0;
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteGreater(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        bool leftBool = (bool)v1.Value, rightBool = (bool)v2.Value;
                        ret = leftBool && !rightBool;
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) > ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) > ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) > ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) > ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) > ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) > ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) > ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) > ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) > ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) > ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) > ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value).CompareTo((string)v2.Value) > 0;
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteLowerOrEquals(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        bool leftBool = (bool)v1.Value, rightBool = (bool)v2.Value;
                        ret = !leftBool || rightBool;
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) <= ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) <= ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) <= ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) <= ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) <= ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) <= ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) <= ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) <= ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) <= ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) <= ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) <= ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value).CompareTo((string)v2.Value) <= 0;
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteLower(StackMachineStorage storage)
        {
            Variable v2 = storage.PopVariable();
            Variable v1 = storage.PopVariable();
            VariableType variableType = v1.VariableType;
            bool ret = false;
            if (v1.VariableType != v2.VariableType) throw new Exception();
            else if (v1.VariableType == VariableType.Object || v1.VariableType == VariableType.RangeIndex) throw new Exception();
            else
            {
                switch (variableType)
                {
                    case VariableType.Boolean:
                        bool leftBool = (bool)v1.Value, rightBool = (bool)v2.Value;
                        ret = !leftBool && rightBool;
                        break;
                    case VariableType.Byte:
                        ret = ((byte)v1.Value) < ((byte)v2.Value);
                        break;
                    case VariableType.SByte:
                        ret = ((sbyte)v1.Value) < ((sbyte)v2.Value);
                        break;
                    case VariableType.Int16:
                        ret = ((short)v1.Value) < ((short)v2.Value);
                        break;
                    case VariableType.UInt16:
                        ret = ((ushort)v1.Value) < ((ushort)v2.Value);
                        break;
                    case VariableType.Int32:
                        ret = ((int)v1.Value) < ((int)v2.Value);
                        break;
                    case VariableType.UInt32:
                        ret = ((uint)v1.Value) < ((uint)v2.Value);
                        break;
                    case VariableType.Int64:
                        ret = ((long)v1.Value) < ((long)v2.Value);
                        break;
                    case VariableType.UInt64:
                        ret = ((ulong)v1.Value) < ((ulong)v2.Value);
                        break;
                    case VariableType.Single:
                        ret = ((float)v1.Value) < ((float)v2.Value);
                        break;
                    case VariableType.Double:
                        ret = ((double)v1.Value) < ((double)v2.Value);
                        break;
                    case VariableType.Char:
                        ret = ((char)v1.Value) < ((char)v2.Value);
                        break;
                    case VariableType.String:
                        ret = ((string)v1.Value).CompareTo((string)v2.Value) < 0;
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            Variable variable = new Variable
            {
                VariableType = VariableType.Boolean,
                Value = ret
            };
            storage.VariableStack.Push(variable);
        }

        void ExecuteAssign(StackMachineStorage storage)
        {
            Variable right = storage.PopVariable();
            NameVariable? left = storage.VariableStack.Pop() as NameVariable;
            storage.SaveTempVariable(left!.Name, right);
            storage.PushVariable(right);
        }

        public virtual object Clone()
        {
            StackMachineEngine stackMachine = new StackMachineEngine((Parser)Parser.Clone(), (SortStation)SortStation.Clone());
            foreach (var item in Variables)
            {
                stackMachine.Variables[item.Key] = item.Value;
            }
            return stackMachine;
        }
    }
}