var input = File.ReadAllLines("input__.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

var end = new Position(0, 0);
var start = new Position(0, 0);
var walls = new List<Position>();
var y = 0;
for (; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        if (row[x] == 'E')
        {
            map[x, y] = 'E';
            end = new Position(x, y);
        }
        else if (row[x] == 'S')
        {
            map[x, y] = 'S';
            start = new Position(x, y);
        }
        else if (row[x] == '#')
        {
            walls.Add(new Position(x, y));
            map[x, y] = row[x];
        }
        else
        {
            map[x, y] = row[x];
        }
    }
}

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Position other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}