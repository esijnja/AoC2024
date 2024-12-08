var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

var Antennas = new List<Antenna>();

for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
        if (row[x] != '.')
        {
            var Antenna = new Antenna(row[x], new Position(x, y));
            Antennas.Add(Antenna);
        }
    }
}

var grouped = Antennas.GroupBy(x => x.Letter).ToList();

var antinodes = new List<Position>();

foreach (var group in grouped)
{
    Console.WriteLine($"Antenna {group.Key} has {group.Count()} antennas");
    foreach (var antenna in group)
    {
        Console.WriteLine($"Antenna {antenna.Letter} is at {antenna.Position.x}, {antenna.Position.y}");
    }
    for (int i = 0; i < group.Count(); i++)
    {
        // Add the antennes as antinode 
        antinodes.Add(group.ElementAt(i).Position);
        for (int j = i + 1; j < group.Count(); j++)
        {
            var antenna1 = group.ElementAt(i);
            var antenna2 = group.ElementAt(j);
            var xdistance = antenna1.Position.x - antenna2.Position.x;
            var ydistance = antenna1.Position.y - antenna2.Position.y;
            Console.WriteLine($"Distance between is {xdistance}  {ydistance}");
            // fo the left
            var ii = -1;
            bool done = false;
            while (!done) {
                var point = new Position(antenna1.Position.x + (ii*xdistance), antenna1.Position.y + (ii*ydistance));
                if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
                {
                    done = true;
                }
                else
                {
                    ii--;
                    antinodes.Add(point);
                }
            }
            
            done = false;
            // to the right
            ii = 1;
            while (!done) 
            {
                var point = new Position(antenna1.Position.x + (ii*xdistance), antenna1.Position.y + (ii*ydistance));
                if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
                {
                    done = true;
                }
                else
                {
                    ii++;
                    antinodes.Add(point);
                }
            }
        }
    }
}

Console.WriteLine($"Antinodes: {antinodes.Count()}");

foreach (var antinode in antinodes)
{
    Console.WriteLine($"Antinode at {antinode.x}, {antinode.y}");
}

var filteredAntinodes = antinodes.Where(x => x.x >= 0 && x.x < width && x.y >= 0 && x.y < height).ToList();

Console.WriteLine($"Filtered Antinodes: {filteredAntinodes.Count()}");


Console.WriteLine($"Antinodes: {filteredAntinodes.Distinct().Count()}");

record Position(int x, int y);
record Antenna(char Letter, Position Position);