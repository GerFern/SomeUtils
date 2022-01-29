using StackMachine.Symbol;

namespace StackMachine
{
    public record Operation : ISymbol
    {
        public int Priority { get; init; }
        public OperationType OperationType { get; init; }
        public BinaryType BinaryType { get; init; }
        public Range Range { get; set; }

        public Operation(OperationType operationType)
        {
            OperationType = operationType;
            switch (operationType)
            {
                case OperationType.BinaryAdd:
                    Priority = 300;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinarySub:
                    Priority = 300;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryMul:
                    Priority = 400;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryDiv:
                    Priority = 400;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryEqual:
                    Priority = 200;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryNotEqual:
                    Priority = 200;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryGreater:
                    Priority = 100;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryLower:
                    Priority = 100;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryGreaterOrEqual:
                    Priority = 100;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.BinaryLowerOrEqual:
                    Priority = 100;
                    BinaryType = BinaryType.BinaryLeft;
                    break;
                case OperationType.Assign:
                    Priority = 50;
                    BinaryType = BinaryType.BinaryRight;
                    break;
                default:
                    break;
            }
        }
    }
}