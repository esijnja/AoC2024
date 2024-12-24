using System.Collections.Immutable;

var input = File.ReadAllLines("input.txt");

var connections = new List<Connection>();
var lans = new List<Lan>();
var nodes = new List<string>();
foreach (var line in input)
{
    var parts = line.Split('-');

    nodes.Add(parts[0]);
    nodes.Add(parts[1]);
    
    connections.Add(new Connection(parts[0], parts[1]));    
    connections.Add(new Connection(parts[1], parts[0]));
    
}

nodes = nodes.Distinct().ToList();

var passwords = new List<(int, string)>();

foreach (var node in nodes)
{
    var alist = connections.Where(x => x.A == node).Select(x => x.B).ToList();
    alist.Add(node);
    var clear = true;
    while (clear)
    {
        var lowest = alist.Count;
        foreach (var (index, aa) in alist.Index())
        {
            clear = false;
            var blist = connections.Where(x => x.A == aa).Select(x => x.B).ToList();
            blist.Add(aa);

            var xxx = blist.Intersect(alist).ToList();

            xxx.Sort();
            Console.WriteLine($"-- {xxx.Count} {string.Join(",", xxx)} ({aa})");
            if (xxx.Count == 2)
            {
                alist.Remove(aa);
                clear = true;
                break;
            }
        }
    }


    alist.Sort();
    
    Console.WriteLine($"{alist.Count}: {string.Join(",", alist)}");
    passwords.Add((alist.Count, string.Join(",", alist)));
    
}

passwords.Sort();
foreach (var (count, password) in passwords)
{
    Console.WriteLine($"{count}: {password}");
}
// a: bs,cf,cn,gb,gk,jf,mp,qk,qo,st,ti,uc,xw


//var group = connections.GroupBy(x => x.A).Select(x => new { x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).ToList();

// foreach (var connection in connections)
// {
//     var a = connection.A;
//     var b = connection.B;
//
//     var alist = connections.Where(x => (x.A == a && x.B != b)).Select(x => x.B).ToList();
//     
//     foreach (var x in alist)
//     {
//             var c =  connections.SingleOrDefault(c=> c.A == x && c.B == b);
//             if (c != null)
//             {
//                 Console.WriteLine($"{a}-{b}-{x}");
//                 
//                 if (lans.Any(lan => (lan.A == a && lan.B == b && lan.C == x) || 
//                                     (lan.A == a && lan.B == x && lan.C == b) || 
//                                     (lan.A == b && lan.B == x && lan.C == a) || 
//                                     (lan.A == b && lan.B == a && lan.C == x) || 
//                                     (lan.A == x && lan.B == a && lan.C == b) || 
//                                     (lan.A == x && lan.B == b && lan.C == a)))
//                 {
//                     continue;
//                 }
//                 lans.Add(new Lan(a, b, x));
//             }
//     }
// }

// var tolal = lans.Count(lan => lan.A[0] == 't' || lan.B[0] == 't' || lan.C[0] == 't');
// Console.WriteLine(tolal);

record Lan(string A, string B, string C);
record Connection(string A, string B);