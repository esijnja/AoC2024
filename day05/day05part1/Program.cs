var input = File.ReadAllLines("input.txt");

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

foreach (var sequence in sequences)
{
    var valid = true;
    foreach (var rule in rules)
    {
        var beforeIndex = sequence.IndexOf(rule.before);
        var afterIndex = sequence.IndexOf(rule.after);
        if (!(beforeIndex == -1 || afterIndex == -1 || beforeIndex < afterIndex))
        {
            valid = false;
            break;
        }
    }

    if (valid)
    {
        correntSequences.Add(sequence);
    }
}

foreach (var sequence in correntSequences)
{
    total += sequence.ElementAt(sequence.Count/2);
}

Console.WriteLine(total);

record Rule(int before, int after);