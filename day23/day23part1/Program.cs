var input = File.ReadAllLines("input.txt");

var connections = new List<Connection>();
var lans = new List<Lan>();
foreach (var line in input)
{
    var parts = line.Split('-');

    connections.Add(new Connection(parts[0], parts[1]));    
    connections.Add(new Connection(parts[1], parts[0]));
    
}

foreach (var connection in connections)
{
    var a = connection.A;
    var b = connection.B;

    var alist = connections.Where(x => (x.A == a && x.B != b)).Select(x => x.B).ToList();
    
    var blist = connections.Where(x => (x.A == b && x.B != a)).Select(x => x.B).ToList();

    foreach (var x in alist)
    {
            var c =  connections.SingleOrDefault(c=> c.A == x && c.B == b);
            if (c != null)
            {
                Console.WriteLine($"{a}-{b}-{x}");
                
                if (lans.Any(lan => (lan.A == a && lan.B == b && lan.C == x) || 
                                    (lan.A == a && lan.B == x && lan.C == b) || 
                                    (lan.A == b && lan.B == x && lan.C == a) || 
                                    (lan.A == b && lan.B == a && lan.C == x) || 
                                    (lan.A == x && lan.B == a && lan.C == b) || 
                                    (lan.A == x && lan.B == b && lan.C == a)))
                {
                    continue;
                }
                lans.Add(new Lan(a, b, x));
            }
    }
    
    

    

}

var tolal = lans.Count(lan => lan.A[0] == 't' || lan.B[0] == 't' || lan.C[0] == 't');
Console.WriteLine(tolal);

record Lan(string A, string B, string C);
record Connection(string A, string B);