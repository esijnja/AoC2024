using System;
using System.Text;

var input = File.ReadAllLines("input.txt");


var encryptedDisk = input[0].ToCharArray().Select(c => int.Parse($"{c}")).ToArray();
var diskSb = new StringBuilder();

var diskLength = encryptedDisk.Length/2;

var diskInts = new List<int>();

for (int i = 0; i < encryptedDisk.Length; i++)
{
    if (i%2 == 0)
    {
        Console.WriteLine($"{i} {encryptedDisk[i]}");
        for (int j = 0; j < encryptedDisk[i]; j++)
        {
            diskInts.Add(i/2); 
            //diskSb.Append(i/2);
        }  
    }
    else
    {
        for (int j = 0; j < encryptedDisk[i]; j++)
        {
            diskInts.Add(-1);
            //diskSb.Append('.');
        }
    }
}
Console.WriteLine(input[0]);
//var disk = diskSb.ToString();
//Console.WriteLine(disk);
int jj = 0;
var disk2 = diskInts.ToArray();
for (int i = disk2.Length-1; i >= 0; i--)
{
    if (disk2[i] != -1)
    {
        for (; jj < disk2.Length && i>jj; jj++)
        {
            if (disk2[jj] == -1)
            {
                disk2[jj] = disk2[i];
                disk2[i] = -1;
                break;
            }            
        }
    }
    //Console.WriteLine(string.Join("", disk2)); 
}

//var disk2 = disk.Replace(".", string.Empty);
//Console.WriteLine(disk2);

var total = 0L;
for (long i = 0; i < disk2.Length; i++)
{
    if (disk2[i] == -1) 
        break;
    total += (long.Parse($"{disk2[i]}") * i);
}

Console.WriteLine(total);