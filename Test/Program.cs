// See https://aka.ms/new-console-template for more information
using MathOperations.Extensions;
using StackMachine;


EngineExecutor engine = new EngineExecutor();
engine.Variables["time"] = new VariableGet
{
    Func = () => DateTime.Now.Ticks,
    VariableType = VariableType.Int64
};
engine.Variables["add"] = new VariableFunc
{
    Func = vs => new Variable() { Value = ((long)vs[0].Value) + ((long)vs[1].Value), VariableType = VariableType.Int64 }
};
engine.Variables["plus"] = new Operation
{
    OperationType = OperationType.BinaryAdd,
    Priority = 999,
};
engine.Variables["to"] = new VariableFunc
{
    Func = vs =>
    {
        var v1 = vs[0];
        var v2 = vs[1];
        string type = v2.Value.ToString().ToLowerInvariant()!;

        VariableType variableType = type switch
        {
            "bool" or "boolean" => VariableType.Boolean,
            "byte" => VariableType.Byte,
            "sbyte" => VariableType.SByte,
            "short" or "int16" => VariableType.Int16,
            "ushort" or "uint16" => VariableType.UInt16,
            "int" or "int32" => VariableType.Int32,
            "uint" or "uint32" => VariableType.UInt32,
            "long" or "int64" => VariableType.Int64,
            "ulong" or "ulong64" => VariableType.UInt64,
            "float" or "single" => VariableType.Single,
            "double" => VariableType.Double,
            "char" => VariableType.Char,
            "string" => VariableType.String,
            _ => VariableType.Object
        };

        if (variableType == v1.VariableType) return v1;
        object ret = null;
        switch (v1.VariableType)
        {
            case global::StackMachine.VariableType.Boolean:
                bool boolVal = (bool)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => boolVal.Convert<bool, bool>(),
                    VariableType.Byte => boolVal.Convert<bool, byte>(),
                    VariableType.SByte => boolVal.Convert<bool, sbyte>(),
                    VariableType.Int16 => boolVal.Convert<bool, short>(),
                    VariableType.UInt16 => boolVal.Convert<bool, ushort>(),
                    VariableType.Int32 => boolVal.Convert<bool, int>(),
                    VariableType.UInt32 => boolVal.Convert<bool, uint>(),
                    VariableType.Int64 => boolVal.Convert<bool, long>(),
                    VariableType.UInt64 => boolVal.Convert<bool, ulong>(),
                    VariableType.Single => boolVal.Convert<bool, float>(),
                    VariableType.Double => boolVal.Convert<bool, double>(),
                    VariableType.Char => (char)boolVal.Convert<bool, ushort>(),
                    VariableType.String => boolVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Byte:
                byte byteVal = (byte)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => byteVal.Convert<byte, bool>(),
                    VariableType.Byte => byteVal.Convert<byte, byte>(),
                    VariableType.SByte => byteVal.Convert<byte, sbyte>(),
                    VariableType.Int16 => byteVal.Convert<byte, short>(),
                    VariableType.UInt16 => byteVal.Convert<byte, ushort>(),
                    VariableType.Int32 => byteVal.Convert<byte, int>(),
                    VariableType.UInt32 => byteVal.Convert<byte, uint>(),
                    VariableType.Int64 => byteVal.Convert<byte, long>(),
                    VariableType.UInt64 => byteVal.Convert<byte, ulong>(),
                    VariableType.Single => byteVal.Convert<byte, float>(),
                    VariableType.Double => byteVal.Convert<byte, double>(),
                    VariableType.Char => (char)byteVal.Convert<byte, ushort>(),
                    VariableType.String => byteVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.SByte:
                sbyte sbyteVal = (sbyte)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => sbyteVal.Convert<sbyte, bool>(),
                    VariableType.Byte => sbyteVal.Convert<sbyte, byte>(),
                    VariableType.SByte => sbyteVal.Convert<sbyte, sbyte>(),
                    VariableType.Int16 => sbyteVal.Convert<sbyte, short>(),
                    VariableType.UInt16 => sbyteVal.Convert<sbyte, ushort>(),
                    VariableType.Int32 => sbyteVal.Convert<sbyte, int>(),
                    VariableType.UInt32 => sbyteVal.Convert<sbyte, uint>(),
                    VariableType.Int64 => sbyteVal.Convert<sbyte, long>(),
                    VariableType.UInt64 => sbyteVal.Convert<sbyte, ulong>(),
                    VariableType.Single => sbyteVal.Convert<sbyte, float>(),
                    VariableType.Double => sbyteVal.Convert<sbyte, double>(),
                    VariableType.Char => (char)sbyteVal.Convert<sbyte, ushort>(),
                    VariableType.String => sbyteVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Int16:
                short shortVal = (short)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => shortVal.Convert<short, bool>(),
                    VariableType.Byte => shortVal.Convert<short, byte>(),
                    VariableType.SByte => shortVal.Convert<short, sbyte>(),
                    VariableType.Int16 => shortVal.Convert<short, short>(),
                    VariableType.UInt16 => shortVal.Convert<short, ushort>(),
                    VariableType.Int32 => shortVal.Convert<short, int>(),
                    VariableType.UInt32 => shortVal.Convert<short, uint>(),
                    VariableType.Int64 => shortVal.Convert<short, long>(),
                    VariableType.UInt64 => shortVal.Convert<short, ulong>(),
                    VariableType.Single => shortVal.Convert<short, float>(),
                    VariableType.Double => shortVal.Convert<short, double>(),
                    VariableType.Char => (char)shortVal.Convert<short, ushort>(),
                    VariableType.String => shortVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.UInt16:
                ushort ushortVal = (ushort)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => ushortVal.Convert<ushort, bool>(),
                    VariableType.Byte => ushortVal.Convert<ushort, byte>(),
                    VariableType.SByte => ushortVal.Convert<ushort, sbyte>(),
                    VariableType.Int16 => ushortVal.Convert<ushort, short>(),
                    VariableType.UInt16 => ushortVal.Convert<ushort, ushort>(),
                    VariableType.Int32 => ushortVal.Convert<ushort, int>(),
                    VariableType.UInt32 => ushortVal.Convert<ushort, uint>(),
                    VariableType.Int64 => ushortVal.Convert<ushort, long>(),
                    VariableType.UInt64 => ushortVal.Convert<ushort, ulong>(),
                    VariableType.Single => ushortVal.Convert<ushort, float>(),
                    VariableType.Double => ushortVal.Convert<ushort, double>(),
                    VariableType.Char => (char)ushortVal.Convert<ushort, ushort>(),
                    VariableType.String => ushortVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Int32:
                int intVal = (int)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => intVal.Convert<int, bool>(),
                    VariableType.Byte => intVal.Convert<int, byte>(),
                    VariableType.SByte => intVal.Convert<int, sbyte>(),
                    VariableType.Int16 => intVal.Convert<int, short>(),
                    VariableType.UInt16 => intVal.Convert<int, ushort>(),
                    VariableType.Int32 => intVal.Convert<int, int>(),
                    VariableType.UInt32 => intVal.Convert<int, uint>(),
                    VariableType.Int64 => intVal.Convert<int, long>(),
                    VariableType.UInt64 => intVal.Convert<int, ulong>(),
                    VariableType.Single => intVal.Convert<int, float>(),
                    VariableType.Double => intVal.Convert<int, double>(),
                    VariableType.Char => (char)intVal.Convert<int, ushort>(),
                    VariableType.String => intVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.UInt32:
                uint uintVal = (uint)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => uintVal.Convert<uint, bool>(),
                    VariableType.Byte => uintVal.Convert<uint, byte>(),
                    VariableType.SByte => uintVal.Convert<uint, sbyte>(),
                    VariableType.Int16 => uintVal.Convert<uint, short>(),
                    VariableType.UInt16 => uintVal.Convert<uint, ushort>(),
                    VariableType.Int32 => uintVal.Convert<uint, int>(),
                    VariableType.UInt32 => uintVal.Convert<uint, uint>(),
                    VariableType.Int64 => uintVal.Convert<uint, long>(),
                    VariableType.UInt64 => uintVal.Convert<uint, ulong>(),
                    VariableType.Single => uintVal.Convert<uint, float>(),
                    VariableType.Double => uintVal.Convert<uint, double>(),
                    VariableType.Char => (char)uintVal.Convert<uint, ushort>(),
                    VariableType.String => uintVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Int64:
                long longVal = (long)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => longVal.Convert<long, bool>(),
                    VariableType.Byte => longVal.Convert<long, byte>(),
                    VariableType.SByte => longVal.Convert<long, sbyte>(),
                    VariableType.Int16 => longVal.Convert<long, short>(),
                    VariableType.UInt16 => longVal.Convert<long, ushort>(),
                    VariableType.Int32 => longVal.Convert<long, int>(),
                    VariableType.UInt32 => longVal.Convert<long, uint>(),
                    VariableType.Int64 => longVal.Convert<long, long>(),
                    VariableType.UInt64 => longVal.Convert<long, ulong>(),
                    VariableType.Single => longVal.Convert<long, float>(),
                    VariableType.Double => longVal.Convert<long, double>(),
                    VariableType.Char => (char)longVal.Convert<long, ushort>(),
                    VariableType.String => longVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.UInt64:
                ulong ulongVal = (ulong)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => ulongVal.Convert<ulong, bool>(),
                    VariableType.Byte => ulongVal.Convert<ulong, byte>(),
                    VariableType.SByte => ulongVal.Convert<ulong, sbyte>(),
                    VariableType.Int16 => ulongVal.Convert<ulong, short>(),
                    VariableType.UInt16 => ulongVal.Convert<ulong, ushort>(),
                    VariableType.Int32 => ulongVal.Convert<ulong, int>(),
                    VariableType.UInt32 => ulongVal.Convert<ulong, uint>(),
                    VariableType.Int64 => ulongVal.Convert<ulong, long>(),
                    VariableType.UInt64 => ulongVal.Convert<ulong, ulong>(),
                    VariableType.Single => ulongVal.Convert<ulong, float>(),
                    VariableType.Double => ulongVal.Convert<ulong, double>(),
                    VariableType.Char => (char)ulongVal.Convert<ulong, ushort>(),
                    VariableType.String => ulongVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Single:
                float floatVal = (float)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => floatVal.Convert<float, bool>(),
                    VariableType.Byte => floatVal.Convert<float, byte>(),
                    VariableType.SByte => floatVal.Convert<float, sbyte>(),
                    VariableType.Int16 => floatVal.Convert<float, short>(),
                    VariableType.UInt16 => floatVal.Convert<float, ushort>(),
                    VariableType.Int32 => floatVal.Convert<float, int>(),
                    VariableType.UInt32 => floatVal.Convert<float, uint>(),
                    VariableType.Int64 => floatVal.Convert<float, long>(),
                    VariableType.UInt64 => floatVal.Convert<float, ulong>(),
                    VariableType.Single => floatVal.Convert<float, float>(),
                    VariableType.Double => floatVal.Convert<float, double>(),
                    VariableType.Char => (char)floatVal.Convert<float, ushort>(),
                    VariableType.String => floatVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Double:
                double doubleVal = (double)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => doubleVal.Convert<double, bool>(),
                    VariableType.Byte => doubleVal.Convert<double, byte>(),
                    VariableType.SByte => doubleVal.Convert<double, sbyte>(),
                    VariableType.Int16 => doubleVal.Convert<double, short>(),
                    VariableType.UInt16 => doubleVal.Convert<double, ushort>(),
                    VariableType.Int32 => doubleVal.Convert<double, int>(),
                    VariableType.UInt32 => doubleVal.Convert<double, uint>(),
                    VariableType.Int64 => doubleVal.Convert<double, long>(),
                    VariableType.UInt64 => doubleVal.Convert<double, ulong>(),
                    VariableType.Single => doubleVal.Convert<double, float>(),
                    VariableType.Double => doubleVal.Convert<double, double>(),
                    VariableType.Char => (char)doubleVal.Convert<double, ushort>(),
                    VariableType.String => doubleVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.Char:
                ushort charVal = (ushort)(char)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => charVal.Convert<ushort, bool>(),
                    VariableType.Byte => charVal.Convert<ushort, byte>(),
                    VariableType.SByte => charVal.Convert<ushort, sbyte>(),
                    VariableType.Int16 => charVal.Convert<ushort, short>(),
                    VariableType.UInt16 => charVal.Convert<ushort, ushort>(),
                    VariableType.Int32 => charVal.Convert<ushort, int>(),
                    VariableType.UInt32 => charVal.Convert<ushort, uint>(),
                    VariableType.Int64 => charVal.Convert<ushort, long>(),
                    VariableType.UInt64 => charVal.Convert<ushort, ulong>(),
                    VariableType.Single => charVal.Convert<ushort, float>(),
                    VariableType.Double => charVal.Convert<ushort, double>(),
                    VariableType.Char => (char)charVal.Convert<ushort, ushort>(),
                    VariableType.String => charVal.ToString()
                };
                break;
            case global::StackMachine.VariableType.String:
                string stringVal = (string)v1.Value;
                ret = variableType switch
                {
                    VariableType.Boolean => Boolean.Parse(stringVal),
                    VariableType.Byte => Byte.Parse(stringVal),
                    VariableType.SByte => SByte.Parse(stringVal),
                    VariableType.Int16 => Int16.Parse(stringVal),
                    VariableType.UInt16 => UInt16.Parse(stringVal),
                    VariableType.Int32 => Int32.Parse(stringVal),
                    VariableType.UInt32 => UInt32.Parse(stringVal),
                    VariableType.Int64 => Int64.Parse(stringVal),
                    VariableType.UInt64 => UInt64.Parse(stringVal),
                    VariableType.Single => Single.Parse(stringVal),
                    VariableType.Double => Double.Parse(stringVal),
                    VariableType.Char => stringVal[0],
                    VariableType.String => stringVal
                };
                break;
            default:
                break;
        }
        return new Variable
        {
            VariableType = variableType,
            Value = ret,
        };
    }
};

//var result = parser.Parse("12 + 4 * 3 * ((1 + 2) * 2) - 6").ToArray();
//result = parser.Parse("\"Hello\" + ' ' + \"world\"").ToArray();




//result = result;

//var sortResult = sortStation.Sort(result).ToArray();
//sortResult = sortResult;

//engine.AddSymbolRange(sortResult);
//var variable = engine.Execute(sortResult);
//variable = variable;

while (true)
{
    try
    {

        string str = Console.ReadLine();
        var variable = engine.Execute(str);
        Console.WriteLine(variable.Value);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}