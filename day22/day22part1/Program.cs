var input = File.ReadAllLines("input.txt");

var test = 123L;

for (int i = 0; i < 10; i++)
{
    test = CalculateSecret(test);
    Console.WriteLine(test);
}


var total = 0L;

foreach (var line in input)
{
    Console.Write($"{line} : ");

    // step 1.a multiple by 64
    // step 1.b MIX: XOR with secret
    // step 1.c PRUNE: mod 16777216 
    // step 2.a divide by 32
    // step 2.b round down
    // step 2.c MIX: XOR with secret
    // step 2.d PRUNE: mod 16777216
    // step 3.a multiple by 2048
    // step 3.b MIX: XOR with secret
    // step 3.c PRUNE: mod 16777216

    var secret = long.Parse(line);
    for (int i = 0; i < 2000; i++)
    {
        
        secret = CalculateSecret(secret);
        
    }
    total += secret;
    Console.WriteLine(secret);
}

Console.WriteLine(total);

long CalculateSecret (long secret)
{
    var result = Step1(secret);
    result = Step2(result);
    result = Step3(result);
    return result;
}

long Step1(long secret)
{
    var result = secret;
    result *= 64;
    result ^= secret;
    result %= 16777216;
    return result;
}

long Step2(long secret)
{
    var result = secret;
    result = (long) Math.Floor(result / 32.0);
    result ^= secret;
    result %= 16777216;
    return result;
}

long Step3(long secret)
{
    var result = secret;
    result *= 2048;
    result ^= secret;
    result %= 16777216;
    return result;
}