var input = File.ReadAllLines("input_.txt");

var totalLines = input.Length;
int lineNumber = 0;
var total=0;
var rules = new List<Rule>();
var line = string.Empty;
do
{
    line = input[lineNumber++];
    if (!string.IsNullOrEmpty(line))
    {
        var parts = line.Split('|');
        var rule = new Rule(int.Parse(parts[0]), int.Parse(parts[1]));
        rules.Add(rule);
    }
} while (!string.IsNullOrEmpty(line));

var sequences = new List<List<int>>();
while (lineNumber < totalLines)
{
    line = input[lineNumber++];
    var sequence = line.Split(',').Select(int.Parse).ToList();
    sequences.Add(sequence);
}

var correntSequences = new List<List<int>>();
var incorrectSequences = new List<List<int>>();
foreach (var sequence in sequences)
{
    foreach (var rule in rules)
    {
        var beforeIndex = sequence.IndexOf(rule.before);
        var afterIndex = sequence.IndexOf(rule.after);
        if (!(beforeIndex == -1 || afterIndex == -1 || beforeIndex < afterIndex))
        {
            // sort accotding to the rules
            
            incorrectSequences.Add(sequence);
            break;
        }
    }

    // if (valid)
    // {
    //     correntSequences.Add(sequence);
    // }
}

foreach (var sequence in incorrectSequences)
{
    var sortedSequence = sequence.Sort(new CustomComparer(rules)).ToList();
    total += sequence.ElementAt(sequence.Count/2);
}

Console.WriteLine(total);

record Rule(int before, int after);

public class CustomComparer(List<Rule> rules2) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        var s = rules2.FirstOrDefault(r => r.before == y && r.after == x);
        if (s != null)
        {
            return -1;
        }
        // Custom comparison logic
        return 0;
    }
}