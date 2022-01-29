namespace StackMachine.Symbol;

public record VariableGet : Variable
{
    public Func<object> Func { get; init; }
    public override object Value { get => Func.Invoke(); init { } }
}
