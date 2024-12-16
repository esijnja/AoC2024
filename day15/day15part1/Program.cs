var input = File.ReadAllLines("input__.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

var boxes = new List<Position>();
var robot = new Position(0, 0);
var walls = new List<Position>();
var y = 0;
for (; y < input.Length; y++)
{
    if (string.IsNullOrEmpty(input[y]))
    {
        break;
    }
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        if (row[x] == '@')
        {
            map[x, y] = '.';
            robot = new Position(x, y);
        }
        else if (row[x] == 'O')
        {
            map[x, y] = '.';
            var box = new Position(x, y);
            boxes.Add(box);
        }
        else
        {
            walls.Add(new Position(x, y));
            map[x, y] = row[x];
        }
    }
}

var queue = new Queue<Direction>();

for (; y< input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        switch (row[x])
        {
            case '^':
                queue.Enqueue(Direction.Up);
                break;
            case 'v':
                queue.Enqueue(Direction.Down);
                break;
            case '<':
                queue.Enqueue(Direction.Left);
                break;
            case '>':
                queue.Enqueue(Direction.Right);
                break;
        }
    }
}

while (queue.Count > 0)
{
    var direction = queue.Dequeue();

    Console.WriteLine(direction);
    switch (direction)
    {
        case Direction.Up:
            if (walls.Any(w => w.x == robot.x && w.y == robot.y-1))
            {
                // Can't move
            }
            else
            {
                if (PushBox(boxes, robot, Direction.Up))
                {
                    robot.y--;
                }
            } 
            break;
        case Direction.Down:
            if (walls.Any(w => w.x == robot.x && w.y== robot.y+1))
            {
                // Can't move
            }
            else
            {
                if (PushBox(boxes, robot, Direction.Down))
                {
                    robot.y++;
                }
            }
            break;
        case Direction.Left:
            if (walls.Any(w => w.x-1 == robot.x && w.y== robot.y))
            {
                // Can't move
            }
            else
            {
                if (PushBox(boxes, robot, Direction.Left))
                {
                    robot.x--;
                }
            }
            break;
        case Direction.Right:
            if (walls.Any(w => w.x+1 == robot.x && w.y== robot.y))
            {
                // Can't move
            }
            else
            {
                if (PushBox(boxes, robot, Direction.Right))
                {
                    robot.x++;
                }
            }
            break;
    }
}

Console.WriteLine(robot);


bool PushBox(List<Position> boxes, Position robot, Direction direction)
{
    switch (direction)
    {
        case Direction.Up:
            // if there is a box to the left of the robot
            // and if there is a box to the left of the box
            // then we can't move the box
            var box = boxes.First(b => b.x == robot.x && b.y == robot.y-1);
            if (box != null))
            {
                return MoveBox(boxes, box, Direction.Up);
            }
            return true;
            break;
        case Direction.Down:
            if (walls.Any(w => w.x == box.x && w.y == box.y+1) || boxes.Any(b => b.x == box.x && b.y == box.y+1))
            {
                return false;
            }
            box.y++;
            break;
        case Direction.Left:
            if (walls.Any(w => w.x-1 == box.x && w.y == box.y) || boxes.Any(b => b.x == box.x-1 && b.y == box.y))
            {
                return false;
            }
            box.x--;
            break;
        case Direction.Right:
            if (walls.Any(w => w.x+1 == box.x && w.y == box.y) || boxes.Any(b => b.x == box.x+1 && b.y == box.y))
            {
                return false;
            }
            box.x++;
            break;
    }

    return true;
}

bool MoveBox(List<Position> boxes, Position box, Direction direction)
{
    switch (direction)
    {
        case Direction.Up:    
            if (walls.Any(w => w.x == box.x && w.y == box.y-1))
            {
                return false;
            }
            var nextBox = boxes.FirstOrDefault(b => b.x == box.x && b.y == box.y-1);
            if (nextBox != null)
            {
                boxMoved = MoveBox(boxes, nextBox, Direction.Up);
            }
            if (!boxMoved)
            {
                return false;
            }
            else
            {
                box.y--;
            }
            break;
        case Direction.Down:
            if (walls.Any(w => w.x == box.x && w.y == box.y+1) || boxes.Any(b => b.x == box.x && b.y == box.y+1))
            {
                return false;
            }
            box.y++;
            break;
        case Direction.Left:
            if (walls.Any(w => w.x-1 == box.x && w.y == box.y) || boxes.Any(b => b.x == box.x-1 && b.y == box.y))
            {
                return false;
            }
            box.x--;
            break;
        case Direction.Right:
            if (walls.Any(w => w.x+1 == box.x && w.y == box.y) || boxes.Any(b => b.x == box.x+1 && b.y == box.y))
            {
                return false;
            }
            box.x++;
            break;
    }

    return true;
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

class Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
} ;