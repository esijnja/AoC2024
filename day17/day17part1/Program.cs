


using System.Text.RegularExpressions;

//var input = File.ReadAllLines("input.txt");

using StreamReader reader = new("input.txt");

    // Read the stream as a string.
var input = reader.ReadToEnd();

var pattern =  @"Register A: (\d+)
Register B: (\d+)
Register C: (\d+)

Program: ((\d),)*(\d)";

var regex = new Regex(pattern/*, RegexOptions.Singleline*/);
var total = 0;
var matches = regex.Matches(input);
var program = new List<int>();
int registerA = 0;
int registerB =0;
int registerC = 0;
foreach (var (index, match) in matches.Index())
{
    registerA= int.Parse(match.Groups[1].Value);
    registerB = int.Parse(match.Groups[2].Value);
    registerC = int.Parse(match.Groups[3].Value);
    for( int i = 4; i < match.Groups.Count; i++)
    {
        program.Add(int.Parse(match.Groups[i].Value));
    }
}