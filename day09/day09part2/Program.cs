using System;
using System.Text;

var input = File.ReadAllLines("input_.txt");


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
;
var disk2 = diskInts.ToArray();
for (int i = disk2.Length-1; i >= 0; i--)
{
    Console.WriteLine($"{i} {disk2[i]}");
    if (disk2[i] != -1)
    {
        var id = disk2[i];
        var count = 1;
        var noot = i;
        
        while (disk2[i--] == id && i > 0)
        {
            count++;
            //i--;
        }
        Console.WriteLine($"{i} {count}");
        for (int jj = 0; jj < disk2.Length; jj++)
        {
            if (disk2[jj] == -1)
            {
                var start = jj;
                var count2 = 1;	
                while (disk2[jj] == -1 && jj < disk2.Length)
                {
                    count2++;
                    jj++;
                    if (count2 == count) 
                    {
                        for (int k = start; k < jj; k++)
                        {
                            disk2[k] = id;
                        }
                        for (int k = i+1; k <= i+count; k++)
                        {
                            disk2[k] = -1;
                        }
                        break;
                    }
                }
                break;
            }            
        }
    }
    Console.WriteLine(DiskToString(disk2)); 
}

string DiskToString(int[] disk)
{
    var sb = new StringBuilder();
    foreach (var i in disk)
    {
        if (i != -1)
        {
            sb.Append(i);
        }
        else
        {
            sb.Append(".");
        }
    }
    return sb.ToString();
}
//var disk2 = disk.Replace(".", string.Empty);
//Console.WriteLine(disk2);

var total = 0L;
for (long i = 0; i < disk2.Length; i++)
{
    if (disk2[i] != -1)
    {
        total += (long.Parse($"{disk2[i]}") * i);
        Console.Write($"{i}");
    }
    else 
    {
        Console.Write($".");
    }
}
Console.WriteLine();
Console.WriteLine(total);