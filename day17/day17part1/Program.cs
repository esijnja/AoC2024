using System.Text.RegularExpressions;

//var input = File.ReadAllLines("input.txt");

using StreamReader reader = new("input.txt");

    // Read the stream as a string.
var input = reader.ReadToEnd();

var pattern =  @"Register A: (\d+)
Register B: (\d+)
Register C: (\d+)

Program: (.*)";

var regex = new Regex(pattern/*, RegexOptions.Singleline*/);
var total = 0;
var matches = regex.Matches(input);
var program = new List<int>();
int registerA = 0;
int registerB = 0;
int registerC = 0;

foreach (var (index, match) in matches.Index())
{
    registerA= int.Parse(match.Groups[1].Value);
    registerB = int.Parse(match.Groups[2].Value);
    registerC = int.Parse(match.Groups[3].Value);
    
    Console.WriteLine($"Program: {match.Groups[4].Value}");
    match.Groups[4].Value.Split(",").ToList().ForEach(x => program.Add(int.Parse(x)));
    
}

Console.WriteLine($"Program: {string.Join(',', program)}");
int rp = 0;

var output = new List<int>();

while (rp < program.Count)
{
    var opcode = program[rp];
    var operand  = program[rp + 1];
    Console.WriteLine($"OPCODE: {opcode} OPERAND: {operand}");
    switch (opcode)
    {
        case 0:
            Adv(operand);
            rp += 2;
            break;
        case 1:
            Bxl(operand);
            rp += 2;
            break;
        case 2:
            Bst(operand);
            rp += 2;
            break;
        case 3:
            Jnz(operand);
            break;
        case 4:
            Bxc(operand);
            rp += 2;
            break;
        case 5:
            Out(operand);
            rp += 2;
            break;
        case 6:
            Bdv(operand);
            rp += 2;
            break;
        case 7:
            Cdv(operand);
            rp += 2;
            break;
    }
}

Console.WriteLine(string.Join(",", output));

// The adv instruction (opcode 0) performs division. The numerator is the value in the A register. 
// The denominator is found by raising 2 to the power of the instruction's combo operand. 
// (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) 
// The result of the division operation is truncated to an integer and then written to the A register.
void Adv(int combo)
{
    var operand = GetOperand(combo);
    registerA = (int)(registerA /Math.Pow(2, operand));
    Console.WriteLine( $"ADV: {operand}({combo}) A:{registerA} B:{registerB} C:{registerC}");
}

// The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, 
// then stores the result in register B.
void Bxl (int combo)
{
    var operand = GetOperand(combo);
    registerB = registerB ^ operand;
    Console.WriteLine( $"BXL: {operand}({combo}) A:{registerA} B:{registerB} C:{registerC}");
}

// The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), 
// then writes that value to the B register.
void Bst(int combo)
{
    var operand = GetOperand(combo);
    registerB = operand % 8;
    Console.WriteLine( $"BST: {operand}({combo}) A:{registerA} B:{registerB} C:{registerC}");
}

// The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, 
// it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, 
// the instruction pointer is not increased by 2 after this instruction.
void Jnz(int literal)
{
    //var oprand = GetOperand(combo);
    if (registerA != 0)
    {
        rp = literal;
    }
    else 
    {
        rp += 2;
    }
    Console.WriteLine( $"JNZ: {literal} {rp} A:{registerA} B:{registerB} C:{registerC}");
}

// The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C,
// then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
void Bxc(int operand)
{
    registerB ^= registerC;
    Console.WriteLine( $"BXC: {operand} A:{registerA} B:{registerB} C:{registerC}");
}

// The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value.
//  (If a program outputs multiple values, they are separated by commas.)
void Out(int combo)
{
    var operand = GetOperand(combo);
    output.Add(operand % 8);
    Console.WriteLine( $"OUT: {operand}({combo}) {string.Join(',' , output)} A:{registerA} B:{registerB} C:{registerC}");
}

// The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register.
// (The numerator is still read from the A register.)
void Bdv(int combo)
{
    var operand = GetOperand(combo);
    registerB =  (int)(registerA/Math.Pow(2, operand));
    Console.WriteLine( $"BDV: {operand}({combo}) A:{registerA} B:{registerB} C:{registerC}");
}

// The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. 
// (The numerator is still read from the A register.)
void Cdv(int combo)
{
    var operand = GetOperand(combo);
    registerC = (int)(registerA/Math.Pow(2, operand));
    Console.WriteLine( $"CDV: {operand}({combo}) A:{registerA} B:{registerB} C:{registerC}");
}

int GetOperand(int operand)
{
    return operand switch 
    {
        <= 3 => operand,
        4 => registerA,
        5 => registerB,
        6 => registerC,
        _ => throw new Exception("Invalid operand")
    };
}