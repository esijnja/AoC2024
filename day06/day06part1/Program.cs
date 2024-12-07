var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

var queue = new Queue<(int x, int y, Direction direction)>();

char[,] map = new char[width, height];
char[,] visited = new char[width, height];



for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
        visited[x, y] = '.';
    }
}

(int x, int y, Direction direction) start = FindStart(map, width, height);

Console.WriteLine($"Start: {start.x}, {start.y}, {start.direction}");
var count = 0;
var step = Move(start);
while (! (step.x <= 0 || step.x >= width || step.y <= 0 || step.y >= height))
{
    step = Move(step);
}


(int x, int y, Direction) Move((int x, int y, Direction direction) current)
{
    visited[current.x, current.y] = '#';
    try
    {
        switch (current.direction)
        {
            case Direction.Up:

                Console.WriteLine($"Up: {current.x}, {current.y}");
                if (map[current.x, current.y - 1] == '#')
                {
                    return (current.x + 1, current.y, Direction.Right);
                }
                else
                {
                    return (current.x, current.y - 1, current.direction);
                }
                break;
            case Direction.Down:
                Console.WriteLine($"Down: {current.x}, {current.y}");
                if (map[current.x, current.y + 1] == '#')
                {
                    return (current.x - 1, current.y, Direction.Left);
                }
                else
                {
                    return (current.x, current.y + 1, current.direction);
                }
                break;
            case Direction.Left:
                Console.WriteLine($"Left: {current.x}, {current.y}");
                if (map[current.x - 1, current.y] == '#')
                {
                    return (current.x, current.y - 1, Direction.Up);
                }
                else
                {
                    return (current.x - 1, current.y, current.direction);
                }
                break;
            case Direction.Right:
                Console.WriteLine($"Right: {current.x}, {current.y}");
                if (map[current.x + 1, current.y] == '#')
                {
                    return (current.x, current.y + 1, Direction.Down);
                }
                else
                {
                    return (current.x + 1, current.y, current.direction);
                }
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    return (-1, -1, Direction.Up);
}

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[y].Length; x++)
    {
        if (visited[x, y] == '#')
        {
            count++;
        }
    }
}

Console.WriteLine(count);

(int x, int y, Direction direction) FindStart(char[,] map, int width, int height)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (map[x, y] == '^')
            {
                return (x, y, Direction.Up);
            }
            else if (map[x, y] == 'v')
            {
                return (x, y, Direction.Down);
            }
            else if (map[x, y] == '<')
            {
                return (x, y, Direction.Left);
            }
            else if (map[x, y] == '>')
            {
                return (x, y, Direction.Right);
            }
        }
    }

    return (-1, -1, Direction.Up);
}


enum Direction
{
    Up,
    Down,
    Left,
    Right
}