using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var pattern =@"mul\(\d{1,3}\,\d{1,3}\)";

var regex = new Regex(pattern);

var matches = regex.Matches(input[0]);
var total = 0;
foreach (Match match in matches)
{
    var values = match.Value.Split(",");
    var a = int.Parse(values[0].Substring(4));
    var b = int.Parse(values[1].Substring(0, values[1].Length - 1));
    var result = a * b;
    total += result;
}

Console.WriteLine(total);