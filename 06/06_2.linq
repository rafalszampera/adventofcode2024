<Query Kind="Program" />

	void Main()
	{
		var file = File.ReadAllText(@"input");
		var lines = file.Split('\n');
		var map = lines.Select(line => line.ToCharArray()).ToArray();
		var loops = 0;
		var steps = 1;
		var stepsUp = 1;
		var stepsDown = 0;
		var stepsLeft = 0;
		var stepRight = 0;

		var loc = map
		.SelectMany((array, i) => array.Select((ch, j) => new { Char = ch, Row = i, Col = j }))
		.FirstOrDefault(x => x.Char == '^');

		var x = loc.Row;
		var y = loc.Col;

		while (true)
		{
		   char[][] deepCopyMap = new char[map.Length][];
			for (int i = 0; i < map.Length; i++)
				deepCopyMap[i] = (char[])map[i].Clone();
			
		   var checkParadox =  Paradox(deepCopyMap, 0, 10000, stepsUp, stepsDown, stepsLeft, stepRight, x, y);
		   if(checkParadox)
		   	loops++;
			
		   var stopWhileLoop =  Step(map, ref steps, ref stepsUp, ref stepsDown, ref stepsLeft, ref stepRight, ref x, ref y);
		   if(stopWhileLoop)
		   	break;
		}
		loops.Dump();
	}
	
	
	private static bool Paradox(char[][] map,  int steps, int maxSteps, int stepsUp, int stepsDown, int stepsLeft, int stepRight, int x, int y)
	{	
		if (stepsUp > 0 && x - stepsUp >= 0 && map[x - stepsUp][y] != '#')
			map[x - stepsUp][y] = '#';
		else if (stepsDown > 0 && x + stepsDown <= map.Length && map[x + stepsDown][y] != '#')
			map[x + stepsDown][y] = '#';
		else if (stepRight > 0 && y + stepRight < map[x].Length && map[x][y + stepRight] != '#')
			 map[x][y + stepRight] = '#';
		else if (stepsLeft > 0 && y - stepsLeft >= 0 && map[x][y - stepsLeft] != '#')
			map[x][y - stepsLeft] = '#';
			
		map[x][y] = 'X';
			
		while (true)
		{
			if(steps > maxSteps)
				return true;
				
			steps += 1;
			map[x][y] = 'X';
			if (stepsUp > 0)
			{
				if (map[x - stepsUp][y] == '#')
				{
					stepsUp = 0;
					stepRight = 1;
				}
				else 
				{
					x = x - stepsUp;
					if (x - stepsUp < 0)
						return false;
				}
			}
			else if (stepsDown > 0)
			{
				if (map[x + stepsDown][y] == '#')
				{
					stepsDown = 0;
					stepsLeft = 1;
				}
				else
				{
					x = x + stepsDown;
					if (x + stepsDown >= map.Length)
						return false;
				}
			}
			else if (stepRight > 0)
			{
				if (map[x][y + stepRight] == '#')
				{
					stepRight = 0;
					stepsDown = 1;
				}
				else
				{
					y = y + stepRight;
					if (y + stepRight >= map[x].Length)
						return false;
				}
			}
			else if (stepsLeft > 0)
			{if (map[x][y - stepsLeft] == '#')
				{
					stepsLeft = 0;
					stepsUp = 1;
				}
				else
				{
				y = y - stepsLeft;
				if (y - stepsLeft < 0)
					return false;
				}
			}
		}
	}
	
	private static bool Step(char[][] map, ref int steps, ref int stepsUp, ref int stepsDown, ref int stepsLeft, ref int stepRight, ref int x, ref int y)
	{
		steps += map[x][y] == '.' ? 1 : 0;
		map[x][y] = 'X';
		if (stepsUp > 0)
		{
			x = x - stepsUp;
			if (x - stepsUp < 0)
				return true;
			else if (map[x - stepsUp][y] == '#')
			{
				stepsUp = 0;
				stepRight = 1;
			}
		}
		else if (stepsDown > 0)
		{
			x = x + stepsDown;
			if (x + stepsDown >= map.Length)
				return true;
			else if (map[x + stepsDown][y] == '#')
			{
				stepsDown = 0;
				stepsLeft = 1;
			}
		}
		else if (stepRight > 0)
		{
			y = y + stepRight;
			if (y + stepRight >= map[x].Length)
				return true;
			else if (map[x][y + stepRight] == '#')
			{
				stepRight = 0;
				stepsDown = 1;
			}
		}
		else if (stepsLeft > 0)
		{
			y = y - stepsLeft;
			if (y - stepsLeft < 0)
				return true;
			else if (map[x][y - stepsLeft] == '#')
			{
				stepsLeft = 0;
				stepsUp = 1;
			}
		}
		return false;
	}