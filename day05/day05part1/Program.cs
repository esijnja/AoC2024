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

foreach (var sequence in sequences)
{
    
    total += sequence.ElementAt(sequence.Count/2);
}

Console.WriteLine(total);

record Rule(int before, int after);