var input = File.ReadAllLines("input.txt");

var secondChange = new List<List<int>>();
var count = 0;
foreach (var (index, line) in input.Index())
{
    var parts = line.Split(' ').Select(int.Parse).ToList();
    var safe = IsSafe(index, parts);
    if (safe)
    {
        Console.WriteLine($"{index}: Safe ({string.Join(" ", parts)})");
        count++;
    }
    else
    {
        //secondChange.Add(parts);
        Console.WriteLine($"{index}: Unsafe ({string.Join(" ", parts)})");
        for (int i = 0; i < parts.Count; i++)
        {
            var copy = parts.ToList();
            copy.RemoveAt(i);
            var safe2 = IsSafe(index, copy);
            if (safe2)
            {
                Console.WriteLine($"{index}-{i}: Safe ({string.Join(" ", copy)})");
                count++;
                break;
            }
            else
            {
                Console.WriteLine($"{index}-{i}: Unsafe ({string.Join(" ", copy)})");
            }
        }
    }
}

Console.WriteLine(count);

bool IsSafe(int index, List<int> list)
{
    var decs = Direction.Unknown;
    if (list[0] < list[1])
    {
        decs = Direction.Up;
    }
    else if (list[0] > list[1])
    {
        decs = Direction.Down;
    }
    else
    {
        // Console.WriteLine($"{index}: Unsafe {list[0]} {list[1]} ({string.Join(" ", list)})");
        return false;
    }

    for (int i = 0; i < list.Count - 1; i++)
    {
        if (list[i] == list[i + 1])
        {
            //  Console.WriteLine($"{index}: Unsafe {list[i]} {list[i+1]} ({string.Join(" ", list)})");
            return false;
        }
        if (list[i] < list[i + 1])
        {
            if (decs != Direction.Up)
            {
                //  Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
            if (list[i + 1] - list[i] > 3)
            {
                //  Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
        }
        if (list[i] > list[i + 1])
        {
            if (decs != Direction.Down)
            {
                // Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
            if (list[i] - list[i + 1] > 3)
            {
                //  Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
        }
    }

    return true;
}

enum Direction
{
    Unknown,
    Up,
    Down
}