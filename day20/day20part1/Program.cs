var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

var end = new Point(0, 0);
var start = new Point(0, 0);
var walls = new List<Point>();
var y = 0;
for (; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        if (row[x] == 'E')
        {
            map[x, y] = '.';
            end = new Point(x, y);
        }
        else if (row[x] == 'S')
        {
            map[x, y] = '.';
            start = new Point(x, y);
        }
        else if (row[x] == '#')
        {
            walls.Add(new Point(x, y));
            map[x, y] = row[x];
        }
        else
        {
            map[x, y] = row[x];
        }
    }
}

Console.WriteLine($"Start: {start.X}, {start.Y}");
Console.WriteLine($"End: {end.X}, {end.Y}");

var steps = new List<Node>();

var totalWithoutCheating = BFS(start, end);

var saves = new List<int>();

var shortCuts = new List<(Point, bool)>();
for (int i = 1; i < width - 1; i++)
{
    for (int j = 1; j < height - 1; j++)
    {
        if (map[i, j] == '#')
        {
            Console.WriteLine($"Wall: {i}, {j}");
            if (map[i - 1, j] == '.' && map[i + 1, j] == '.')
            {
                var d1 = steps.FirstOrDefault(x => x.Point.X == i - 1 && x.Point.Y == j)?.Distance ?? -1;
                var d2 = steps.FirstOrDefault(x => x.Point.X == i + 1 && x.Point.Y == j)?.Distance ?? -1;
                if (d1 > 0 && d2 > 0)
                {
                    if (d1 > d2)
                    {
                        saves.Add(d1 - d2- 2);
                    }
                    else
                    {
                        saves.Add(d2 - d1-2);
                    }
                }else {
                    Console.WriteLine($"No steps: {d1} - {d2}");
                }

                shortCuts.Add((new Point(i, j), true));
            }
            if (map[i, j - 1] == '.' && map[i, j + 1] == '.')
            {
                var d1 = steps.FirstOrDefault(x => x.Point.X == i && x.Point.Y == j - 1)?.Distance ?? -1;
                var d2 = steps.FirstOrDefault(x => x.Point.X == i && x.Point.Y == j + 1)?.Distance ?? -1;
                if (d1 > 0 && d2 > 0)
                {
                    if (d1 > d2)
                    {
                        saves.Add(d1 - d2-2);
                    }
                    else
                    {
                        saves.Add(d2 - d1-2);
                    }
                }
                else {
                    Console.WriteLine($"No steps: {d1} - {d2}");
                }
                shortCuts.Add((new Point(i, j), false));
            }
        }
    }
}

Console.WriteLine($"Shortcuts: {shortCuts.Count()} {saves.Count()}");


Console.WriteLine($"Without Cheating {totalWithoutCheating}");

saves.GroupBy(x => x).OrderBy(x => x.Key).ToList().ForEach(x => Console.WriteLine($"{x.Count()} - {x.Key}"));

Console.WriteLine($"Total: {saves.Where(x => x >= 100).Count()}");

bool IsValid(Point p)
{
    return p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height;
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

    var visited = new bool[width, height];
    var queue = new Queue<Node>();
    var startNode = new Node(start, 1);
    steps.Add(startNode);
    queue.Enqueue(startNode);
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
            if (IsValid(next) && !visited[next.X, next.Y] && map[next.X, next.Y] == '.')
            {
                visited[next.X, next.Y] = true;
                var nextNode = new Node(next, current.Distance + 1);
                queue.Enqueue(nextNode);
                steps.Add(nextNode);
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