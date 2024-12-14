using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");
int width =101;//  = 11;
int height =103; //= 7;


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
foreach (var guard in gaurds)
{
    var x = (guard.P.X + 100*guard.V.X) % width;
    var y = (guard.P.Y + 100*guard.V.Y) % height;
    if (x < 0)
    {
        x += width;
    }
    if (y < 0)
    {
        y += height;
    }
    var p = new Point(x,y);

    points.Add(p);

    Console.WriteLine($"{p.X} {p.Y}");
}


var q1 = points.Where(p => p.X >= 0 && p.X < width/2 && p.Y >= 0 && p.Y < height/2).Count();
var q2 = points.Where(p => p.X > width/2 && p.X < width && p.Y >= 0 && p.Y < height/2).Count();
var q3 = points.Where(p => p.X >= 0 && p.X < width/2 && p.Y > height/2 && p.Y < height).Count();
var q4 = points.Where(p => p.X > width/2 && p.X < width && p.Y > height/2 && p.Y < height).Count();
total= q1*q2*q3*q4;
Console.WriteLine($"{q1} {q2} {q3} {q4} ");
Console.WriteLine(total);
record Point(int X, int Y);
record Velocity(int X, int Y);
record Guard(Point P, Velocity V);