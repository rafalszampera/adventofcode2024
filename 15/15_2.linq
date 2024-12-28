<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
    string file = File.ReadAllText(@"input");
	var parsed = file.Split("\r\n\r\n");
	
	var map = parsed[0].Split("\r\n").Select(line => line.Select(plant => plant).ToArray()).ToArray();
	var moves = parsed[1];
	
	map = TransformMap(map);
	
	var (robot_i, robot_j) = FindRobot(map);
	
	foreach(var move in moves)
	{
		map = MoveRobot(map, move, robot_i, robot_j);
		(robot_i, robot_j) = FindRobot(map);
	}
	
    var sum = map.SelectMany((row, i) => row.Select((cell, j) => new { cell, i, j }))
             .Where(pos => pos.cell == '[')
             .Sum(pos => 100 * pos.i + pos.j);

    sum.Dump();
}

(int, int) FindRobot(char[][] map)
{
    for(int i = 0; i < map.Length; i++)
	{
		for(int j = 0; j < map[0].Length; j++)
		{
			if(map[i][j] == '@')
			{
				return ( i, j);
			}
		}
	}
	return (-1, -1);
}

char[][] MoveRobot(char[][] map, char move, int robot_i, int robot_j)
{
	if(CheckIfPossible(map, move, robot_i, robot_j))
	{
		map[robot_i][robot_j] = '.';
		map = Move2(map, move, '@', robot_i, robot_j);
	}
	
	return map;
}

char[][] Move2(char[][] map, char move, char sign, int robot_i, int robot_j)
{
	if (IsWall(map, move, robot_i, robot_j))
    {
        map[robot_i][robot_j] = sign;
        return map;
    }
    UpdatePosition(ref robot_i, ref robot_j, move);
	
	var new_sign = map[robot_i][robot_j];
	if (new_sign != '.') 
	{
		Move2(map, move, new_sign, robot_i, robot_j);
		if(new_sign == '[')
		{
			if (move == 'v' || move == '^')
			{
				map[robot_i][robot_j+1] = '.';
				Move2(map, move, ']', robot_i, robot_j+1);
			}
		}
		else if(new_sign == ']')
		{
			if (move == 'v' || move == '^')
			{
				map[robot_i][robot_j-1] = '.';
				Move2(map, move, '[', robot_i, robot_j-1);
			}
		}
	}
	map[robot_i][robot_j] = sign;
		
	return map;
}

bool IsWall(char[][] map, char move, int robot_i, int robot_j)
{
    return (move == '<' && map[robot_i][robot_j - 1] == '#')
        || (move == '>' && map[robot_i][robot_j + 1] == '#')
        || (move == 'v' && map[robot_i + 1][robot_j] == '#')
        || (move == '^' && map[robot_i - 1][robot_j] == '#');
}

void UpdatePosition(ref int robot_i, ref int robot_j, char move)
{
    switch (move)
    {
        case '<': robot_j--; break;
        case '>': robot_j++; break;
        case 'v': robot_i++; break;
        case '^': robot_i--; break;
    }
}

char[][] TransformMap(char[][] map)
{
    return map.Select(row => row.SelectMany(c => TransformChar(c)).ToArray()).ToArray();
}

IEnumerable<char> TransformChar(char c)
{
    switch (c)
    {
        case '#': return new char[] { '#', '#' };
        case 'O': return new char[] { '[', ']' };
        case '.': return new char[] { '.', '.' };
        case '@': return new char[] { '@', '.' };
        default: return new char[] { c, c };
    }
}

bool CheckIfPossible(char[][] map, char move, int robot_i, int robot_j)
{
	var retVal = true;
	int point_i = robot_i, point_j = robot_j;
	if (move == '<')
	{
		point_j--;
	}
	else if (move == '>')
	{
		point_j++;
	}
	else if (move == 'v')
	{
		point_i++;
		if(map[point_i][point_j] == '[')
			retVal = retVal && CheckIfPossible(map, move, point_i, point_j+1);
			
		if(map[point_i][point_j] == ']')
			retVal = retVal && CheckIfPossible(map, move, point_i, point_j-1);
	}
	else if (move == '^')
	{
		point_i--;
		if(map[point_i][point_j] == '[')
			retVal = retVal && CheckIfPossible(map, move, point_i, point_j+1);
			
		if(map[point_i][point_j] == ']')
			retVal = retVal && CheckIfPossible(map, move, point_i, point_j-1);
	}	
		
	if(map[point_i][point_j] == '.')
		return retVal;
		
	if(map[point_i][point_j] == '#')
		return false;
	
	return retVal && CheckIfPossible(map, move, point_i, point_j);
}

static bool IsPointAccessible(char[][] map, int i, int j)
{
	return !(i < 0 || j < 0 || map[i][j] == '#' ||i >= map.Length || j >= map[0].Length);
}


#region private::Tests

[Fact] void Test_CheckIfPossible_left_false() => Assert.False(CheckIfPossible([['#','#','#','#','#'],['#','#','@','#','#'],['#','#','#','#','#']], '<', 1, 2));
[Fact] void Test_CheckIfPossible_left_true() => Assert.True(CheckIfPossible([['#','#','#','#','#'],['#','.','@','#','#'],['#','#','#','#','#']], '<', 1, 2));

[Fact] void Test_CheckIfPossible_right_false() => Assert.False(CheckIfPossible([['#','#','#','#','#'],['#','.','@','#','#'],['#','#','#','#','#']], '>', 1, 2));
[Fact] void Test_CheckIfPossible_right_true() => Assert.True(CheckIfPossible([['#','#','#','#','#'],['#','.','@','.','#'],['#','#','#','#','#']], '>', 1, 2));

[Fact] void Test_CheckIfPossible_up_true() => Assert.True(CheckIfPossible([['#','#','#','#','#'],['#','.','.','.','#'],['#','.','@','.','#'],['#','.','.','.','#'],['#','#','#','#','#']], '^', 2, 2));
[Fact] void Test_CheckIfPossible_up_false() => Assert.False(CheckIfPossible([['#','#','#','#','#'],['#','.','#','.','#'],['#','.','@','.','#'],['#','.','#','.','#'],['#','#','#','#','#']], '^', 2, 2));


[Fact] void Test_CheckIfPossible_down_true() => Assert.True(CheckIfPossible([['#','#','#','#','#'],['#','.','.','.','#'],['#','.','@','.','#'],['#','.','.','.','#'],['#','#','#','#','#']], 'v', 2, 2));
[Fact] void Test_CheckIfPossible_down_false() => Assert.False(CheckIfPossible([['#','#','#','#','#'],['#','.','#','.','#'],['#','.','@','.','#'],['#','.','#','.','#'],['#','#','#','#','#']], 'v', 2, 2));


[Fact] void Test_Move_left_true() => Assert.True(CheckIfPossible([['#','#','#','#','#','#'],['#','.','O','@','#','#'],['#','#','#','#','#','#']], '<', 1, 3));
[Fact] void Test_Move_right_false() => Assert.False(CheckIfPossible([['#','#','#','#','#','#'],['#','.','O','@','#','#'],['#','#','#','#','#','#']], '>', 1, 3));

[Fact] void Test_Move_right_false2() => Assert.False(CheckIfPossible([['#','#','#','#','#','#'],['#','#','O','@','#','#'],['#','#','#','#','#','#']], '>', 1, 3));


#endregion