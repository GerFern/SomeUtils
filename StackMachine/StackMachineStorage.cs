using StackMachine.Symbol;

namespace StackMachine
{
    public class StackMachineStorage
    {
        public StackMachineEngine Owner { get; init; }
        public Stack<ISymbol> VariableStack { get; } = new();

        public Variable PopVariable()
        {
            var symbol = VariableStack.Pop();
            return GetVariable(symbol);
        }

        public Variable PeekVariable()
        {
            var symbol = VariableStack.Peek();
            return GetVariable(symbol);
        }

        public void PushVariable(ISymbol symbol)
        {
            VariableStack.Push(symbol);
        }

        public void SaveTempVariable(string name, ISymbol symbol)
        {
            Owner.TempVariables[name] = symbol;
        }

        public bool RemoveTempVariable(string name)
        {
            return Owner.TempVariables.Remove(name);
        }

        public bool RemoveTempVariable(string name, out ISymbol? symbol)
        {
            return Owner.TempVariables.Remove(name, out symbol);
        }

        Variable GetVariable(ISymbol symbol)
        {
            if (symbol is Variable variable) return variable;
            else if (symbol is NameVariable nameVariable)
            {
                ISymbol varSymbol;
                if (Owner.TempVariables.TryGetValue(nameVariable.Name, out varSymbol!)) return GetVariable(varSymbol);
                else if (Owner.Variables.TryGetValue(nameVariable.Name, out varSymbol!)) return GetVariable(varSymbol);
                else throw new Exception();
            }
            throw new Exception();
        }
    }
}