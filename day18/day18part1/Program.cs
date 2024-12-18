var input = File.ReadAllLines("input.txt");

// const int mapSize = 7;
// const int runFirst = 12;
const int mapSize = 71;
const int runFirst = 1024;

char[,] memoryMap = new char[mapSize, mapSize];
var droppingBytes = new List<Point>();
foreach (var line in input)
{
    var parts = line.Split(",");
    droppingBytes.Add(new Point(int.Parse(parts[0]), int.Parse(parts[1])));
}

for (int y = 0; y < mapSize; y++)
{
    for (int x = 0; x < mapSize; x++)
    {
        memoryMap[x, y] = '.';
    }
}

for (int i = 0; i < runFirst; i++)
{
    memoryMap[droppingBytes[i].X, droppingBytes[i].Y] = '#';
}

PrintMap();

void PrintMap()
{
    for (int y = 0; y < mapSize; y++)
    {
        for (int x = 0; x < mapSize; x++)
        {
            Console.Write(memoryMap[x, y]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


var total = BFS(new Point(0, 0), new Point(70, 70));

Console.WriteLine(total);


bool IsValid(Point p)
{
    return p.X >= 0 && p.X < mapSize && p.Y >= 0 && p.Y < mapSize;
}

int BFS(Point start, Point end)
{
    var directions = new List<Point>
    {
    new Point(-1, 0),
    new Point(0, -1),
    new Point(0, 1),
    new Point(1, 0)
    }.ToArray();

    var visited = new bool[mapSize, mapSize];
    var queue = new Queue<Node>();
    queue.Enqueue(new Node(start, 0));
    visited[start.X, start.Y] = true;

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (current.Point.X == end.X && current.Point.Y == end.Y)
        {
            return current.Distance;
        }

        for (int i = 0; i < directions.Length; i++)
        {
            var next = new Point(current.Point.X + directions[i].X, current.Point.Y + directions[i].Y);
            if (IsValid(next) && !visited[next.X, next.Y] && memoryMap[next.X, next.Y] == '.')
            {
                visited[next.X, next.Y] = true;
                queue.Enqueue(new Node(next, current.Distance + 1));
            }
        }
    }

    return -1;
}

class Node(Point point, int distance)
{
    public Point Point { get; set; } = point;
    public int Distance { get; set; } = distance;
}

record Point(int X, int Y);