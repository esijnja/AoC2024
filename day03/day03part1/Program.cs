using System.Text.RegularExpressions;

//var input = File.ReadAllLines("input.txt");

using StreamReader reader = new("input.txt");

    // Read the stream as a string.
var input = reader.ReadToEnd();

var pattern =@"mul\(\d{1,3}\,\d{1,3}\)";

Console.WriteLine(input.Length);

var regex = new Regex(pattern);

var matches = regex.Matches(input);
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