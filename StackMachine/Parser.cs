using PCRE;
using StackMachine.Symbol;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

namespace StackMachine;

public class Parser : ICloneable
{
    public Dictionary<string, string> Groups { get; }
    public Dictionary<string, Func<string, ISymbol?[]?>> ExecuteGroups { get; }
    public string Regex { get; set; }

    public Parser()
    {
        Groups = new();
        foreach (var item in DefaultParser.GetDefaultDefs())
        {
            Groups[item.Key] = item.Value;
        }

        ExecuteGroups = new();
        foreach (var item in DefaultParser.GetDefaultSymbolCreators())
        {
            ExecuteGroups[item.Key] = item.Value;
        }
        Regex = CreateRegex(Groups, ExecuteGroups);
    }

    public Parser(Dictionary<string, string> groups, Dictionary<string, Func<string, ISymbol?[]?>> executeGroups)
    {
        Groups = groups;
        ExecuteGroups = executeGroups;
        Regex = CreateRegex(groups, executeGroups);
    }

    public static string CreateRegex(Dictionary<string, string> groups, Dictionary<string, Func<string, ISymbol?[]?>> executeGroups)
    {
        StringBuilder sb = new StringBuilder(512);
        sb.AppendLine("(?(DEFINE)");
        foreach (var item in groups)
        {
            sb.Append("    (?<def_")
                .Append(item.Key)
                .Append('>')
                .Append(item.Value)
                .AppendLine(")");
        }
        sb.AppendLine(")");
        foreach (var item in executeGroups.SkipLast(1))
        {
            sb.Append("(?<")
                .Append(item.Key)
                .Append(">(?&def_")
                .Append(item.Key)
                .AppendLine("))")
                .AppendLine("|");
        }
        var last = executeGroups.Last().Key;
        sb.Append("(?<")
                .Append(last)
                .Append(">(?&def_")
                .Append(last)
                .AppendLine("))");
        return sb.ToString();
    }

    public IEnumerable<ISymbol> Parse(string input)
    {
        ImmutableArray<int> sw = Enumerable.Range(14, 3345 - 14 + 1).Concat(Enumerable.Range(5633, 23718 - 5633 + 1)).ToImmutableArray();
        ReadOnlyCollection<int> vs = new(Enumerable.Range(14, 3345 - 14 + 1).Concat(Enumerable.Range(5633, 23718 - 5633 + 1)).ToArray());
        var m = PcreRegex.Matches(input, Regex, PcreOptions.IgnorePatternWhitespace | PcreOptions.MultiLine | PcreOptions.Extended);
        foreach (var item in m)
        {
            ISymbol[]? symbols = ExecuteGroups.Select(a => (item.Groups[a.Key], a.Value))
                .Where(a => a.Item1.Success)
                .Select(a =>
                {
                    return (new Range(a.Item1.Index, a.Item1.EndIndex), a.Item2(a.Item1.Value));
                })
                .Select(a =>
                {
                    ISymbol[]? vs = a.Item2.Where(b => b != null).ToArray();
                    foreach (var item in vs)
                    {
                        item.Range = a.Item1;
                    }
                    return vs;
                })
                .FirstOrDefault();
            if (symbols != null)
            {
                foreach (var symbol in symbols)
                {
                    yield return symbol;
                }
            }
            //var g = item.Groups.Skip(1).Where(a => a.Success).FirstOrDefault();
        }
    }

    public virtual object Clone()
    {
        Dictionary<string, string> groups = new();
        Dictionary<string, Func<string, ISymbol[]?>> executeGroups = new();
        foreach (var item in Groups)
        {
            groups[item.Key] = item.Value;
        }
        foreach (var item in ExecuteGroups)
        {
            executeGroups[item.Key] = item.Value;
        }
        Parser parser = new Parser(groups, executeGroups);
        parser.Regex = Regex;
        return parser;
    }
}
