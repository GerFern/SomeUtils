namespace StackMachine.Symbol
{
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
}