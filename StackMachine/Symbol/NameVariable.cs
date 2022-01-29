namespace StackMachine.Symbol;

public record NameVariable : ISymbol
{
    public string Name { get; init; }
    public bool IsNotDefined { get; init; }
    public bool IsFunc { get; init; }
    public bool EmptyArg { get; init; }
    public int ArgCount { get; set; }
    public Range Range { get; set; }
}
