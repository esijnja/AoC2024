using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");
int width = 101;//  = 11;
int height = 103; //= 7;


var pattern = @"p=(-?\d+),(-?\d+).v=(-?\d+),(-?\d+)";

var regex = new Regex(pattern);
var total = 0;

var gaurds = new List<Guard>();
foreach (var (index, line) in input.Index())
{
    var match = regex.Match(line);
    var Xp = int.Parse(match.Groups[1].Value);
    var Yp = int.Parse(match.Groups[2].Value);
    var Xv = int.Parse(match.Groups[3].Value);
    var Yv = int.Parse(match.Groups[4].Value);

    var p = new Point(Xp, Yp);
    var v = new Velocity(Xv, Yv);
    var g = new Guard(p, v);
    gaurds.Add(g);

}


var points = new List<Point>();

var ii = 0;
while ((55 + (101 * ++ii)) % 103 != 0);

Console.WriteLine(ii);

var lcmA = lcm(78, 79);
Console.WriteLine(lcmA);

for (var i = 0; i < 10403; i++)
{
    
    foreach (var guard in gaurds)
    {
        var x = (guard.P.X + guard.V.X) % width;
        var y = (guard.P.Y + guard.V.Y) % height;
        if (x < 0)
        {
            x += width;
        }
        if (y < 0)
        {
            y += height;
        }
        var p = new Point(x, y);
    
        guard.P = p;
    
       //Console.WriteLine($"{p.X} {p.Y}");
    }
    if (!gaurds.GroupBy(g => new Point(g.P.X, g.P.Y)).Any(xx => xx.Count() > 1))
    {
        Console.WriteLine(i);
        PrintGrid(gaurds);
    }   
}

// 18 103
// 74 101

// 18+ a*103 = 74 + a*101
// 18 + 103a = 74 + 101a
// 2a = 74 - 18
// a = 28

// 18 + 103*28 = 74 + 101*28

// 18 + 2864 = 74 + 2828
// 2882 = 2902

void PrintGrid(List<Guard> gaurds)
{
    var grid = new char[height, width];
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            grid[i, j] = '.';
        }
    }

    foreach (var guard in gaurds)
    {
        grid[guard.P.Y, guard.P.X] = '#';
    }

    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            Console.Write(grid[i, j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static int gcf(int a, int b)
{
    while (b != 0)
    {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

static int lcm(int a, int b)
{
    return (a / gcf(a, b)) * b;
}


public record Point(int X, int Y);
public record Velocity(int X, int Y);
public class Guard()
{
    public Point P { get; set; }
    public Velocity V { get; set; }

    public Guard(Point p, Velocity v) : this()
    {
        this.P = p;
        this.V = v;
    }
}