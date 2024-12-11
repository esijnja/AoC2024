using System.Diagnostics;


var input = File.ReadAllLines("input__.txt");

var stones = input[0].Split(' ').Select(long.Parse).ToArray();
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int i = 0; i < 20; i++)
{  
    var newStones = new List<long>();
    foreach (var stone in stones)
    {
        if (stone == 0)
        {
            newStones.Add(1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            var stoneStr = stone.ToString();
            var half = stoneStr.Length / 2;
            var left = long.Parse(stoneStr.Substring(0, half));
            var right = long.Parse(stoneStr.Substring(half));
            newStones.Add(left);
            newStones.Add(right);
        }
        else
        {
            newStones.Add(stone * 2024);
        }
    }
    Console.WriteLine($"{string.Join( " ", newStones)} : {i} {newStones.Count} {stopWatch.ElapsedMilliseconds}");
    //Console.WriteLine($"{i} {stones.Length} {stopWatch.ElapsedMilliseconds}");
    stones = newStones.ToArray();
}
Console.WriteLine(stones.Length);
// If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
// If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. 
//   The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
// If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.