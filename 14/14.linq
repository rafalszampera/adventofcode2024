<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
    int mapSizeX = 101;
    int mapSizeY = 103;
    int numberOfMoves = 100;

    string filePath = @"input";
    string[] lines = File.ReadAllText(filePath).Split("\r\n");

    var robots = lines
        .Select(line => line.Split(" ")
        .SelectMany(code => code.Split("=")[1].Split(",")))
        .Select(line => new
        {
            p_x = int.Parse(line.ToArray()[0]),
            p_y = int.Parse(line.ToArray()[1]),
            v_y = int.Parse(line.ToArray()[2]),
            v_x = int.Parse(line.ToArray()[3])
        });

    var map = InitializeMap(mapSizeY, mapSizeX, robots);

    map = Mover(map, numberOfMoves);

    var quadrants = CalculateQuadrants(map, mapSizeY, mapSizeX);
    (quadrants.q1, quadrants.q2 * quadrants.q3 * quadrants.q3 * quadrants.q4).Dump();
}

List<Tuple<int, int>>[,] InitializeMap(int mapSizeY, int mapSizeX, IEnumerable<dynamic> robots)
{
    var map = new List<Tuple<int, int>>[mapSizeY, mapSizeX];
    for (int i = 0; i < mapSizeY; i++)
    {
        for (int j = 0; j < mapSizeX; j++)
        {
            var robotList = robots
                .Where(robot => robot.p_x == j && robot.p_y == i)
                .Select(robot => new Tuple<int, int>(robot.v_x, robot.v_y))
                .ToList();
            map[i, j] = robotList;
        }
    }
    return map;
}

List<Tuple<int, int>>[,] Mover(List<Tuple<int, int>>[,] map, int numberOfMoves)
{
    for (int i = 0; i < numberOfMoves; i++)
    {
        map = MoveRobots(map);
    }
    return map;
}

List<Tuple<int, int>>[,] MoveRobots(List<Tuple<int, int>>[,] map)
{
    var sizeX = map.GetLength(1) - 1;
    var sizeY = map.GetLength(0) - 1;
    var newMap = new List<Tuple<int, int>>[map.GetLength(0), map.GetLength(1)];

    for (int i = 0; i <= sizeY; i++)
    {
        for (int j = 0; j <= sizeX; j++)
        {
            if (newMap[i, j] == null) newMap[i, j] = new List<Tuple<int, int>>();

            foreach (var robot in map[i, j])
            {
                int newRobotX = (sizeX + 1 + j + robot.Item2) % (sizeX + 1);
                newRobotX = newRobotX >= 0 ? newRobotX : sizeX + newRobotX;

                int newRobotY = (sizeY + 1 + i + robot.Item1) % (sizeY + 1);
                newRobotY = newRobotY >= 0 ? newRobotY : sizeY + newRobotY;

                if (newMap[newRobotY, newRobotX] == null) newMap[newRobotY, newRobotX] = new List<Tuple<int, int>>();
                newMap[newRobotY, newRobotX].Add(new Tuple<int, int>(robot.Item1, robot.Item2));
            }
        }
    }

    return newMap;
}

(dynamic q1, dynamic q2, dynamic q3, dynamic q4) CalculateQuadrants(List<Tuple<int, int>>[,] map, int mapSizeY, int mapSizeX)
{
    int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
    for (int j = 0; j < mapSizeY; j++)
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            if (j < mapSizeY / 2 && i < mapSizeX / 2) q1 += map[j, i].Count;
            if (j < mapSizeY / 2 && i >= mapSizeX / 2) q2 += map[j, i].Count;
            if (j >= mapSizeY / 2 && i < mapSizeX / 2) q3 += map[j, i].Count;
            if (j >= mapSizeY / 2 && i >= mapSizeX / 2) q4 += map[j, i].Count;
        }
    }
    return (q1, q2, q3, q4);
}