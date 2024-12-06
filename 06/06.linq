<Query Kind="Program" />

void Main()
 {
	var file = File.ReadAllText(@"input");   
    var lines = file.Split('\n');
    var map = lines.Select(line => line.ToCharArray()).ToArray();
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
	
	while(true)
	{
		steps += map[x][y] == '.'? 1 : 0;
		map[x][y] = 'X';
		if(stepsUp > 0)
		{
			x = x - stepsUp;
			if(x - stepsUp < 0)
				break;
			else if(map[x - stepsUp][y] == '#')
			{
				stepsUp = 0;
				stepRight = 1;
			}
		}
		else if(stepsDown > 0)
		{
			x = x + stepsDown;
			if(x + stepsDown >= map.Length)
				break;
			else if(map[x + stepsDown][y] == '#')
			{
				stepsDown = 0;
				stepsLeft = 1;
			}
		}
		else if(stepRight > 0)
		{
			y= y + stepRight;
			if(y + stepRight >= map[x].Length)
				break;
			else if(map[x][y+ stepRight] == '#')
			{
				stepRight = 0;
				stepsDown = 1;
			}
		}
		else if(stepsLeft > 0)
		{
			y = y - stepsLeft;
			if(y - stepsLeft < 0)
				break;
			else if(map[x][y- stepsLeft] == '#')
			{
				stepsLeft = 0;
				stepsUp = 1;
			}
		}
	}
	(++steps).Dump(); //last step to jump out of the board
}