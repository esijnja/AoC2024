using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");
int i = 0;
var Oper = new List <IInput>();
for (; !string.IsNullOrEmpty(input[i]); i++)
{
    var parts = input[i].Split(": ");
    Oper.Add(new InputRecord(parts[0], parts[1] == "1"));
}

i++;

var pattern = $@"(\w+) (\w+) (\w+) -> (\w+)";
for (;i < input.Length; i++)
{
    
    var match =  Regex.Match(input[i], pattern);
    var input1 = match.Groups[1].Value;
    var input2 = match.Groups[3].Value;
    var @operator = match.Groups[2].Value;
    var output = match.Groups[4].Value;
    var logicOperator = @operator switch
    {
        "AND" => Operator.AND,
        "OR" => Operator.OR,
        "XOR" => Operator.XOR,
        _ => throw new Exception($"Invalid operator {@operator}")
    };
    var logicOperator2 = new LogicOperator(output, logicOperator, new List<string> {input1, input2});
    Oper.Add(logicOperator2);
}

foreach (var item in Oper)
{
    if (item is LogicOperator logicOperator)
    {
        logicOperator.Input1 = Oper.First(x => x.Name == logicOperator.Inputs[0]);
        logicOperator.Input2 = Oper.First(x => x.Name == logicOperator.Inputs[1]);
    }
}

var outputs = Oper.Where(x=> x.Name[0] == 'z').ToList();
outputs.Sort((x, y) => string.Compare(x.Name, y.Name));
var total = 0L;
foreach (var (index, item) in outputs.Index())
{
    total |=  (item.Output ? 1L : 0L) << index;
    Console.WriteLine($"{item.Name}: {item.Output}");
}

Console.WriteLine(total);

enum Operator
{
    AND,
    OR,
    XOR
}

class LogicOperator (string name, Operator @operator,  List<string> inputs) : IInput
{
    public string Name => name;
    public List<string> Inputs => inputs;

    public bool Output {get  {
        if (@operator == Operator.AND)
        {
            return Input1.Output && Input2.Output;
        }
        else if (@operator == Operator.OR)
        {
            return Input1.Output || Input2.Output;
        }
        else if (@operator == Operator.XOR)
        {
            return Input1.Output ^ Input2.Output;
        }
        return true;
    }
    
    }
    
    public  IInput? Input1 { get; set;}
    public IInput? Input2 { get; set;}

}

interface IInput
{
    public string Name { get; }
    public bool Output { get; }
}

record InputRecord (string Name, bool inputValue) : IInput
{
    public bool Output => inputValue;
}
