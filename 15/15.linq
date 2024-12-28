<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
    string file = File.ReadAllText(@"input");
    var parsed = file.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

    var map = parsed[0]
        .Split(new string[] { "\r\n" }, StringSplitOptions.None)
        .Select(line => line.ToCharArray())
        .ToArray();
    var moves = parsed[1];

    var (robot_i, robot_j) = FindRobot(map);

    foreach (var move in moves)
    {
        map = MoveRobot(map, move, ref robot_i, ref robot_j);
    }

    var sum = map
        .SelectMany((row, rowIndex) => row.Select((cell, colIndex) => new { cell, rowIndex, colIndex }))
        .Where(pos => pos.cell == 'O')
        .Sum(pos => 100 * pos.rowIndex + pos.colIndex);

    sum.Dump();
}

char[][] MoveRobot(char[][] map, char move, ref int robot_i, ref int robot_j)
{
    if (CheckIfPossible(map, move, robot_i, robot_j))
    {
        map[robot_i][robot_j] = '.';
        Move(map, move, '@', ref robot_i, ref robot_j);
    }

    return map;
}

void Move(char[][] map, char move, char sign, ref int robot_i, ref int robot_j)
{
    switch (move)
    {
        case '<': robot_j--; break;
        case '>': robot_j++; break;
        case 'v': robot_i++; break;
        case '^': robot_i--; break;
    }

    var new_sign = map[robot_i][robot_j];
    if (new_sign != '.')
    {
        Move(map, move, new_sign, ref robot_i, ref robot_j);
    }
    map[robot_i][robot_j] = sign;
}

bool CheckIfPossible(char[][] map, char move, int robot_i, int robot_j)
{
    int next_i = robot_i, next_j = robot_j;
    switch (move)
    {
        case '<': next_j--; break;
        case '>': next_j++; break;
        case 'v': next_i++; break;
        case '^': next_i--; break;
    }

    while (IsPointAccessible(map, next_i, next_j))
    {
        if (map[next_i][next_j] == '.')
        {
            return true;
        }
        switch (move)
        {
            case '<': next_j--; break;
            case '>': next_j++; break;
            case 'v': next_i++; break;
            case '^': next_i--; break;
        }
    }
    return false;
}

bool IsPointAccessible(char[][] map, int i, int j)
{
    return i >= 0 && j >= 0 && i < map.Length && j < map[0].Length && map[i][j] != '#';
}

(int, int) FindRobot(char[][] map)
{
    var robotPosition = map
        .SelectMany((row, rowIndex) => row
            .Select((cell, colIndex) => new { cell, rowIndex, colIndex }))
        .FirstOrDefault(position => position.cell == '@');

    return robotPosition != null
        ? (robotPosition.rowIndex, robotPosition.colIndex)
        : (-1, -1);
}

#region private::Tests

[Fact] void Test_CheckIfPossible_left_false() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '#', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '<', 1, 2));
[Fact] void Test_CheckIfPossible_left_true() => Assert.True(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '<', 1, 2));

[Fact] void Test_CheckIfPossible_right_false() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '>', 1, 2));
[Fact] void Test_CheckIfPossible_right_true() => Assert.True(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '@', '.', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '>', 1, 2));

[Fact] void Test_CheckIfPossible_up_true() => Assert.True(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '.', '.', '#' }, new char[] { '#', '.', '@', '.', '#' }, new char[] { '#', '.', '.', '.', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '^', 2, 2));
[Fact] void Test_CheckIfPossible_up_false() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '#', '.', '#' }, new char[] { '#', '.', '@', '.', '#' }, new char[] { '#', '.', '#', '.', '#' }, new char[] { '#', '#', '#', '#', '#' } }, '^', 2, 2));

[Fact] void Test_CheckIfPossible_down_true() => Assert.True(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '.', '.', '#' }, new char[] { '#', '.', '@', '.', '#' }, new char[] { '#', '.', '.', '.', '#' }, new char[] { '#', '#', '#', '#', '#' } }, 'v', 2, 2));
[Fact] void Test_CheckIfPossible_down_false() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#' }, new char[] { '#', '.', '#', '.', '#' }, new char[] { '#', '.', '@', '.', '#' }, new char[] { '#', '.', '#', '.', '#' }, new char[] { '#', '#', '#', '#', '#' } }, 'v', 2, 2));

[Fact] void Test_Move_left_true() => Assert.True(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#', '#' }, new char[] { '#', '.', 'O', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#', '#' } }, '<', 1, 3));
[Fact] void Test_Move_right_false() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#', '#' }, new char[] { '#', '.', 'O', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#', '#' } }, '>', 1, 3));
[Fact] void Test_Move_right_false2() => Assert.False(CheckIfPossible(new char[][] { new char[] { '#', '#', '#', '#', '#', '#' }, new char[] { '#', '#', 'O', '@', '#', '#' }, new char[] { '#', '#', '#', '#', '#', '#' } }, '>', 1, 3));

#endregion
