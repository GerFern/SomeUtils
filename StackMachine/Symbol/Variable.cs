namespace StackMachine.Symbol;

public record Variable : ISymbol
{
    public VariableType VariableType { get; init; }
    public Type? Type { get; init; }
    public virtual object Value { get; init; }
    public Range Range { get; set; }
}