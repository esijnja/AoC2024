using System.Numerics;

var input = File.ReadAllLines("input.txt");

var firstList = new List<int>();
var secondList = new List<int>();
foreach (var line in input)
{
   var numbers = line.Split(" ",StringSplitOptions.RemoveEmptyEntries);
    firstList.Add(int.Parse(numbers[0]));
    secondList.Add(int.Parse(numbers[1]));
}

firstList.Sort();
secondList.Sort();
var total = 0;
foreach (var (index, item) in firstList.Index())
{
    Console.WriteLine($" {index}: {item} - {secondList[index]}");;
    var d = Math.Abs ( item - secondList[index]);
    total += d;
}

Console.WriteLine(total);