namespace StackMachine.Symbol;

public interface ISymbol
{
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

public enum BinaryType
{
    BinaryLeft,
    BinaryRight,
    PostFunc // Example: factorial (!)
}

public enum OperationType
{
    BinaryAdd,
    BinarySub,
    BinaryMul,
    BinaryDiv,
    BinaryEqual,
    BinaryNotEqual,
    BinaryGreater,
    BinaryLower,
    BinaryGreaterOrEqual,
    BinaryLowerOrEqual,
    Assign
}