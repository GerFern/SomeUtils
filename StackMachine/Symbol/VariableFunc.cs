using System.Collections.Immutable;

namespace StackMachine.Symbol;

public record VariableFunc : ISymbol
{
    public Func<FuncInput, Variable> Func { get; init; }
    public int ArgCount { get; set; }
    public Range Range { get; set; }
}

public record FuncInput
{
    public Variable this[int index]
    {
        get => Args[index];
    }

    public ImmutableArray<Variable> Args { get; init; }
    public StackMachineStorage Storage { get; init; }
}
