using System.Text;
using System.Text.RegularExpressions;

//var input = File.ReadAllLines("input.txt");

using StreamReader reader = new("input.txt");

    // Read the stream as a string.
var input = reader.ReadToEnd();

var pattern =@"mul\(\d{1,3}\,\d{1,3}\)";

var stepOne =@"do\(.*?don'";



var regexStepOne = new Regex(stepOne);
var ii = input.IndexOf("don't()");

//var inputStepOne = input.Substring(0, ii);


var sb = new StringBuilder();
foreach (Match matchSO in regexStepOne.Matches(input))
{
    sb.Append(matchSO.Value);
}

Console.WriteLine(sb.ToString());

var regex = new Regex(pattern);

var matches = regex.Matches(sb.ToString());
var total = 0;
foreach (var (index, match) in matches.Index())
{
    var values = match.Value.Split(",");
    var a = int.Parse(values[0].Substring(4));
    var b = int.Parse(values[1].Substring(0, values[1].Length - 1));
    var result = a * b;
    total += result;
    Console.WriteLine($"{index}\t{match.Value}\t:\t {a}\t*\t{b}\t= {result}\t({total})");
}

Console.WriteLine(total);