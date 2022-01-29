namespace StackMachine.Symbol;

public record SplitArg : ISymbol
{
    public Range Range { get; set; }
}
