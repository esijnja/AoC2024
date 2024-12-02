var input = File.ReadAllLines("input.txt");

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
}

Console.WriteLine(count);


bool IsSafe(int index, List<int> list)
{
    var decs = false;
    if (list[0] < list[1])
    {
        decs = true;
    } 
    else if (list[0] > list[1])
    {
        decs = false;
    } else
    {
        Console.WriteLine($"{index}: Unsafe {list[0]} {list[1]} ({string.Join(" ", list)})");
        return false;
    }

    for (int i = 0; i < list.Count - 1; i++)
    {
        if (list[i] == list[i + 1])
        {
            Console.WriteLine($"{index}: Unsafe {list[i]} {list[i+1]} ({string.Join(" ", list)})");
            return false;
        }
        if (list[i] < list[i + 1])
        {
            if (!decs)
            {
                Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
            if (list[i+1]-list[i] > 3)
            {
                Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
        }
        if (list[i] > list[i + 1])
        {
            if (decs)
            {
                Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
            if (list[i]-list[i+1] > 3)
            {
                Console.WriteLine($"{index}: Unsafe {decs} {list[i]} {list[i+1]} ({string.Join(" ", list)})");
                return false;
            }
        }
    }

    return true;
}