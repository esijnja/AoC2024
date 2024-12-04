﻿var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width,height];
int[,] route = new int[width,height];

for (int y = 0; y< input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x,y] = row[x];
    }  
}
var count =0;
for (int y = 1; y < height-1; y++)
{
    for (int x = 1; x < width-1; x++)
    {
        if (map[x,y] == 'A')
        {
            if (((map[x-1,y-1] == 'M' && map[x+1,y+1] == 'S') || 
                (map[x-1,y-1] == 'S' && map[x+1,y+1] == 'M')) &&
                ((map[x-1,y+1] == 'M' && map[x+1,y-1] == 'S') || 
                (map[x-1,y+1] == 'S' && map[x+1,y-1] == 'M')))
            {
                count++;
            }
        }
    }
}
Console.WriteLine(count);