using System.Text.RegularExpressions;

//var input = File.ReadAllLines("input.txt");

using StreamReader reader = new("input.txt");

    // Read the stream as a string.
var input = reader.ReadToEnd();

//var pattern =@"Button A: X\+(\d+), Y\+(\d+).Button B: X\+(\d+), Y\+(\d+).Prize: X=(\d+), Y=(\d+)";
var pattern = @"Button A: X\+(\d+), Y\+(\d+)
Button B: X\+(\d+), Y\+(\d+)
Prize: X=(\d+), Y=(\d+)";

var regex = new Regex(pattern/*, RegexOptions.Singleline*/);
var total = 0;
var matches = regex.Matches(input);
foreach (var (index, match) in matches.Index())
{
    var Xa = int.Parse(match.Groups[1].Value);
    var Ya = int.Parse(match.Groups[2].Value);
    var Xb = int.Parse(match.Groups[3].Value);
    var Yb = int.Parse(match.Groups[4].Value);
    var Xp = int.Parse(match.Groups[5].Value);
    var Yp = int.Parse(match.Groups[6].Value);

    var b = ((Xa * Yp) - (Ya * Xp)) / ((Xa * Yb) - (Ya * Xb));
    var a = ((Yp * Xb) - (Xp * Yb)) / ((Ya * Xb) - (Xa * Yb));
    
    var XX = a * Xa + b * Xb;
    var YY = a * Ya + b * Yb;
    Console.WriteLine($"Case {index + 1}: {a} {b}");
    if (XX == Xp && YY == Yp)
    {
        var tokens = 3 * a +  b;
        total += tokens;
        Console.WriteLine($"{index + 1}: {tokens} {total}");
    }
}

Console.WriteLine(total);