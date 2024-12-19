var input = File.ReadAllLines("input_.txt");

var towel = input[0].Split(" ");

var patterns = new List<string>();

for (int i = 2; i < input.Length; i++)
{
    patterns.Add(input[i]);
}

foreach (var pattern in patterns)
{
    

}