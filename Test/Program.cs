// See https://aka.ms/new-console-template for more information
using StackMachine;
using StackMachine.Symbol;

StackMachineEngine engine = new StackMachineEngine();
engine.Variables["time"] = new VariableGet
{
    Func = () => DateTime.Now.Ticks,
    VariableType = VariableType.Int64
};
engine.Variables["add"] = new VariableFunc
{
    Func = input => new Variable() { Value = ((long)input[0].Value) + ((long)input[1].Value), VariableType = VariableType.Int64 }
};

while (true)
{
    try
    {


        string str = Console.ReadLine();
        var p = engine.Parser.Parse(str).ToArray();
        var s = engine.SortStation.Sort(p).ToArray();
        foreach (var item in s)
        {
            Console.WriteLine(item);
        }


        var variable = engine.Execute(s);
        Console.WriteLine(variable?.Value ?? "NULL");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}