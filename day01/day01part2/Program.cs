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


var simularity = secondList.GroupBy(x => x).Select(x=> new {x.Key, Count = x.Count()}).ToDictionary(x=>x.Key, x=>x.Count);
var total = 0;
foreach (var item in firstList)
{
//     var d = item * simularity.FirstOrDefault((key, _) => item==key)Select.Count;
//      total += d;
    if (simularity.ContainsKey(item))
    {
        total += item * simularity[item];
    }
}

Console.WriteLine(total);