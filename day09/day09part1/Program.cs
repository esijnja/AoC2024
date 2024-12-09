using System;
using System.Text;

var input = File.ReadAllLines("input_.txt");


var encryptedDisk = input[0].ToCharArray().Select(c => int.Parse($"{c}")).ToArray();
var diskSb = new StringBuilder();


for (int i = 0; i < encryptedDisk.Length; i++)
{
    if (i%2 == 0)
    {
        Console.WriteLine($"{i} {encryptedDisk[i]}");
        for (int j = 0; j < encryptedDisk[i]; j++)
        {
            diskSb.Append(i/2);
        }  
    }
    else
    {
        for (int j = 0; j < encryptedDisk[i]; j++)
        {
            diskSb.Append('.');
        }
    }
}
Console.WriteLine(input[0]);
var disk = diskSb.ToString();
Console.WriteLine(disk);
int jj = 0;
var disk2 = disk.ToCharArray();
for (int i = disk2.Length-1; i >= 0; i--)
{
    if (disk2[i] != '.')
    {
        for (; jj < disk2.Length; jj++)
        {
            if (disk2[jj] == '.')
            {
                disk2[jj] = disk2[i];
                disk2[i] = '.';
                break;
            }            
        }
    }
    Console.WriteLine(string.Join("", disk2)); 
}

//var disk2 = disk.Replace(".", string.Empty);
Console.WriteLine(disk2);

var total = 0L;
for (long i = 0; i < disk2.Length; i++)
{
    if (disk2[i] != '.') total += (long.Parse($"{disk2[i]}") * i);
}

Console.WriteLine(total);