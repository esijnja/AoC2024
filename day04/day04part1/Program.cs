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
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[x,y] == 'X')
        {
            // North
            if(x >= 3 && map[x-1,y] == 'M' && map[x-2,y] == 'A' && map[x-3,y] == 'S')
            {
                count++;
            }
            // North west
            if(x >= 3  &&  y >= 3 && map[x-1,y-1] == 'M' && map[x-2,y-2] == 'A' && map[x-3,y-3] == 'S')
            {
                count++;
            }
            // West
            if( y >= 3 && map[x,y-1] == 'M' && map[x,y-2] == 'A' && map[x,y-3] == 'S')
            {
                count++;
            }
            // South west
            if(width - x > 3 && y >= 3 &&  map[x+1,y-1] == 'M' && map[x+2,y-2] == 'A' && map[x+3,y-3] == 'S')
            {
                count++;
            }
            // South
            if(width - x > 3 && map[x+1,y] == 'M' && map[x+2,y] == 'A' && map[x+3,y] == 'S')
            {
                count++;
            }
            // South east
            if(width - x > 3 && height - y >3 && map[x+1,y+1] == 'M' && map[x+2,y+2] == 'A' && map[x+3,y+3] == 'S')
            {
                count++;
            }
            // East
            if(height - y >3  && map[x,y+1] == 'M' && map[x,y+2] == 'A' && map[x,y+3] == 'S')
            {
                count++;
            }
            // North east
            if(x >= 3  &&  height - y >3  && map[x-1,y+1] == 'M' && map[x-2,y+2] == 'A' && map[x-3,y+3] == 'S')
            {
                count++;
            }
        }
    }
}
Console.WriteLine(count);