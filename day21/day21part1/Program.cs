var input = File.ReadAllLines("input_.txt");
var KeyPadRoutes = new List<PadRoute>{
    new PadRoute('A', '0', new List<char>{'<', 'A'}),
    new PadRoute('A', '1', new List<char>{'^', '<', '<', 'A'}),
    new PadRoute('A', '2', new List<char>{'^', 'A'}),
    new PadRoute('A', '3', new List<char>{'^', 'A'}),
    new PadRoute('A', '4', new List<char>{'^', '^', '<', '<', 'A'}),
    new PadRoute('A', '5', new List<char>{'^', '^', '<', 'A'}),
    new PadRoute('A', '9', new List<char>{'^', '^', '^', 'A'}),

    new PadRoute('0', 'A', new List<char>{'>', 'A'}),
    new PadRoute('0', '2', new List<char>{'^', 'A'}),
    new PadRoute('0', '8', new List<char>{'^', '^', '^', 'A'}),

    new PadRoute('1', '7', new List<char>{'^', '^', 'A'}),    

    new PadRoute('2', '4', new List<char>{'^', '<', 'A'}),
    new PadRoute('2', '7', new List<char>{'^', '^', '<', 'A'}),
    new PadRoute('2', '8', new List<char>{'^', '^', 'A'}),
    new PadRoute('2', '9', new List<char>{'^', '^', '>', 'A'}),

    new PadRoute('3', 'A', new List<char>{'v', 'A'}), 
    new PadRoute('3', '7', new List<char>{'<', '<', '^', '^', 'A'}),

    new PadRoute('4', '5', new List<char>{'>', 'A'}), 
    new PadRoute('4', '6', new List<char>{'>', '>', 'A'}),

    new PadRoute('5', '0', new List<char>{'v', 'v', 'A'}), 
    new PadRoute('5', '6', new List<char>{'>', 'A'}),

    new PadRoute('6', 'A', new List<char>{'v', 'v', 'A'}),
    new PadRoute('6', '3', new List<char>{'v', 'A'}),

    new PadRoute('7', '9', new List<char>{'>', '>', 'A'}),

    new PadRoute('8', 'A', new List<char>{'>', 'v', 'v', 'v', 'A'}),  // >vvvA 2 2 1 1 4 = 10, vvv>A 3 1 1 2 2 = 9  =============== 
    new PadRoute('8', '0', new List<char>{'v', 'v', 'v', 'A'}),
    new PadRoute('8', '6', new List<char>{'>', 'v', 'A'}),

    new PadRoute('9', 'A', new List<char>{'v', 'v', 'v', 'A'}),
    new PadRoute('9', '8', new List<char>{'<', 'A'})
};

var arrowPadRoute = new List<PadRoute>{
    new PadRoute('A', 'A', new List<char>{'A'}),                // 1
    new PadRoute('A', '<', new List<char>{'v', '<', '<', 'A'}), //  v<<A 2 2 1 4 = 9, <v<A 4 3 3 4 = 14 <<vA 4 1 2 3 = 10 
    new PadRoute('A', '^', new List<char>{'<', 'A'}),
    new PadRoute('A', '>', new List<char>{'v', 'A'}),
    new PadRoute('A', 'v', new List<char>{'v', '<', 'A'}),      //  v<A 3 2 4 = 9, <vA 4 2 3 = 9

    new PadRoute('<', 'A', new List<char>{'>', '>', '^', 'A'}), //  >>^A 2 1 3 2 = 8, >^>A 2 3 3 2 = 10 , ^>>A 2 3 1 2 = 8 
    new PadRoute('<', '^', new List<char>{'>', '^', 'A'}),      //  >^A 2 3 2 = 7, ^>A 2 3 2 = 7
    new PadRoute('<', '>', new List<char>{'>', '>', 'A'}),      //  >>A 2 1 2 = 5
    new PadRoute('<', 'v', new List<char>{'>', 'A'}),           // 2
    new PadRoute('<', '<', new List<char>{'A'}),                // 1

    new PadRoute('^', '<', new List<char>{'v', '<', 'A'}),      //  v<A 3 2 4 = 9, <vA 4 2 3 = 9
    new PadRoute('^', 'A', new List<char>{'>', 'A'}),
    new PadRoute('^', '>', new List<char>{'v', '>', 'A'}),      //  v>A 3 2 2 = 7, >vA 2 2 3 = 7
    new PadRoute('^', 'v', new List<char>{'v', 'A'}),
    new PadRoute('^', '^', new List<char>{'A'}),

    new PadRoute('>', '<', new List<char>{'<', '<', 'A'}),
    new PadRoute('>', '^', new List<char>{'<', '^', 'A'}),     //  <^A 4 3 2 = 9, ^<A 2 3 4 = 9 
    new PadRoute('>', 'A', new List<char>{'^', 'A'}),
    new PadRoute('>', 'v', new List<char>{'<', 'A'}),
    new PadRoute('>', '>', new List<char>{'A'}),

    new PadRoute('v', '<', new List<char>{'<', 'A'}),
    new PadRoute('v', '^', new List<char>{'^', 'A'}),
    new PadRoute('v', '>', new List<char>{'>', 'A'}),
    new PadRoute('v', 'A', new List<char>{'^', '>', 'A'}),      //  ^>A 2 3 2 = 7, >^A 2 3 2 = 7
    new PadRoute('v', 'v', new List<char>{'A'}),

};
var total = 0;
foreach (var line in input)
{
    Console.WriteLine(line);
    var code = $"A{line}";
    var number = int.Parse(line[..^1]);
    var sequence = NewFunction(code.ToList(), KeyPadRoutes);
    Console.WriteLine($"Sequence: {string.Join("", sequence)} for {code} with length {sequence.Count}");
    sequence.Insert(0, 'A');
    var seq2 = NewFunction(sequence, arrowPadRoute);
    
    Console.WriteLine($"Sequence: {string.Join("", seq2)} for {code} with length {seq2.Count}");
    seq2.Insert(0, 'A');
    var seq3 = NewFunction(seq2, arrowPadRoute);
    Console.WriteLine($"Sequence: {string.Join("", seq3)} for {code} with length {seq3.Count}");
    
    var subtotal = seq3.Count * number;
    Console.WriteLine($"Subtotal: {subtotal} = {seq3.Count} * {number}");
    total += subtotal;
}
Console.WriteLine($"Total: {total}");
List<char> NewFunction(List<char> chars, List<PadRoute> padRoutes)
{
    var seq3 = new List<char>();
   
    for (var i = 0; i < chars.Count-1; i++)
    {
        var from = chars[i];
        var to = chars[i + 1];
        var route = padRoutes.FirstOrDefault(x => x.from == from && x.to == to);
        if (route != null)
        {
            //Console.WriteLine($"From {from} to {to} via {string.Join(", ", route.keys)} for {route.Distance}");
            seq3.AddRange(route.keys);
        }
        else
        {
            Console.WriteLine($"No route from {from} to {to}");
        }
    }
    return seq3;
}


record PadRoute(char from, char to, IList<char> keys)
{
    public int Distance => keys.Count;
}

// <vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
// v<<A>>^A<A>AvA<^AA>A<vAAA>^A
// <A^A>^^AvvvA
// 029A