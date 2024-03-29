﻿using StackMachine.Symbol;

namespace StackMachine
{
    public class SortStation : ICloneable
    {
        public virtual object Clone()
        {
            return new SortStation();
        }

        public virtual IEnumerable<ISymbol> Sort(IEnumerable<ISymbol> symbols)
        {
#if DEBUG
            List<ISymbol> debug = new();
#endif
            Stack<ISymbol> stack = new();
            Stack<NameVariable> stackFuncs = new();
            NameVariable? tmpNameVariable = null;
            List<ISymbol> current = new();
            ISymbol? prev = null;
            foreach (ISymbol symbol in symbols)
            {
                if (symbol is Variable) current.Add(symbol);
                else if (symbol is NameVariable nameVariable)
                {
                    if (!nameVariable.IsFunc)
                    {
                        current.Add(nameVariable);
                    }
                    else
                    {
                        if (nameVariable.EmptyArg) current.Add(nameVariable);
                    }
                }
                else if (symbol is Operation operation)
                {
                    while (stack.TryPeek(out ISymbol? symbolStack) && symbolStack is Operation operationStack && (operationStack.Priority > operation.Priority || (operationStack.Priority == operation.Priority && operationStack.BinaryType == BinaryType.BinaryLeft)))
                    {
                        current.Add(stack.Pop());
                    }
                    stack.Push(operation);
                }
                else if (symbol is Scobe scobe)
                {
                    if (scobe.Type == Scobe.ScobeType.Start)
                    {
                        if(scobe.Func != null)
                        {
                            stackFuncs.Push(scobe.Func);
                        }
                        stack.Push(scobe);
                    }
                    else
                    {
                        while (stack.TryPop(out ISymbol symbolStack))
                        {
                            if (symbolStack is Scobe scobeStack && scobeStack.Type == Scobe.ScobeType.Start)
                            {
                                if(scobeStack.Func != null)
                                {
                                    current.Add(scobeStack.Func);
                                }
                                break;
                            }
                            else
                            {
                                current.Add(symbolStack);
                            }
                        }
                    }
                }
                else if (symbol is SplitArg arg)
                {
                    stackFuncs.Peek().ArgCount++;
                    while(stack.TryPeek(out var symbolStack))
                    {
                        if (symbolStack is Scobe) break;
                        else current.Add(stack.Pop());
                    }
                }
                if (current.Count > 0)
                {
                    prev = current.Last();
                    foreach (var item in current)
                    {
                        if (item != null)
                            yield return item;
                    }
#if DEBUG
                    debug.AddRange(current);
#endif
                    current.Clear();
                }
            }

            if (tmpNameVariable != null)
            {
                yield return tmpNameVariable;
#if DEBUG
                debug.Add(tmpNameVariable);
#endif
            }
            while (stack.TryPop(out ISymbol symbol)!)
            {
                yield return symbol;
#if DEBUG
                debug.Add(symbol);
#endif
            }

        }
    }    
  
}