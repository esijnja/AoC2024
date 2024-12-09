using System;
using System.Text;

var input = File.ReadAllLines("input_.txt");


var encryptedDisk = input[0].ToCharArray();
var diskSb = new StringBuilder();


for (int i = 0; i < encryptedDisk.Length; i++)
{
    if (i%2 == 0)
    {
        for (int j = 0; j < encryptedDisk[i]; j++)
        {
            diskSb.Append(i);
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

var disk = diskSb.ToString();
var disk2 = disk.Replace(".", string.Empty);
Console.WriteLine(disk2);

var diskChars = disk2.ToCharArray();
var total = 0L;
for (long i = 0; i < diskChars.Length; i++)
{
    total += (long.Parse($"{diskChars[i]}") * i);
}

Console.WriteLine(total);