var input = File.ReadAllLines("input.txt");
var total = 0l;
foreach (var (index, line) in input.Index())
{
    var parts = line.Split(":");
    var testvalue = long.Parse( parts[0]);
    var test = parts[1].Trim();
    var test2 = test.Split(" ").Select(x => int.Parse(x)).ToArray();

    
    // +
    // *
    var found = false;
    var operators = new Operator[test2.Length - 1];
    // make loop to change the operators
    for (int i = 0; i < Math.Pow(2, test2.Length - 1) && !found; i++)
    {
        var binary = Convert.ToString(i, 2).PadLeft(test2.Length - 1, '0');
        for (int j = 0; j < binary.Length; j++)
        {
            operators[j] = binary[j] == '0' ? Operator.Add : Operator.Multiply;
        }
        var sub = Calculate(test2, operators);
        if (sub == testvalue)
        {
            total += testvalue;
            Console.WriteLine($"{index}\t{testvalue}\t{sub}");
            found = true;
        }
    }
}

Console.WriteLine(total);

long Calculate(int[] test2, Operator[] operators)
{
    long sub = test2[0];
    for (int i = 0; i < test2.Length - 1; i++)
    {
        if (operators[i] == Operator.Add)
        {
            sub += test2[i + 1];
        }
        else if (operators[i] == Operator.Multiply)
        {
            sub *= test2[i + 1];
        }
    }
    return sub;
}

enum Operator
{
    Add,
    Multiply
}