<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
    string file = File.ReadAllText(@"input");

    var map = file
        .Split(new string[] { "\r\n" }, StringSplitOptions.None)
        .Select(line => line.ToCharArray())
        .ToArray();
		
	var cost =map.Select(row => row.Select(cell => int.MaxValue).ToArray()).ToArray();
	
	int reindeer_x = map.Length-2; 
	int reindeer_y = 1;
    map[reindeer_x][reindeer_y] = '.';
	Step(map, cost, '>', reindeer_x, reindeer_y, 0);
	Step(map, cost, '^', reindeer_x, reindeer_y, 1001);
	
	cost[1][cost[1].Length-2].Dump();
}

void Step(char[][] map, int[][] cost, char move, int reindeer_x, int reindeer_y, int newCost)
{	
	if(!IsPointAccessible(map, reindeer_x, reindeer_y))
		return;
	
	if(cost[reindeer_x][reindeer_y] > newCost)
	{
		cost[reindeer_x][reindeer_y] = newCost;
		map[reindeer_x][reindeer_y] = move;
	
		if(move == '>') Step(map, cost, '>', reindeer_x, reindeer_y+1, newCost+1);
		else Step(map, cost, '>', reindeer_x, reindeer_y+1, newCost+1001);		
		
		if(move == '<') Step(map, cost, '<', reindeer_x, reindeer_y-1, newCost+1);
		else Step(map, cost, '<', reindeer_x, reindeer_y-1, newCost+1001);
				
		if(move == '^') Step(map, cost, '^', reindeer_x-1, reindeer_y, newCost+1);
		else Step(map, cost, '^', reindeer_x-1, reindeer_y, newCost+1001);

		if(move == 'v') Step(map, cost, 'v', reindeer_x+1, reindeer_y, newCost+1);
		else Step(map, cost, 'v', reindeer_x+1, reindeer_y, newCost+1001);		
	}
	else if(cost[reindeer_x][reindeer_y] < newCost + 2004)
	{	
		if(move == '>') Step(map, cost, '>', reindeer_x, reindeer_y+1, newCost+1);
		
		if(move == '<') Step(map, cost, '<', reindeer_x, reindeer_y-1, newCost+1);
				
		if(move == '^') Step(map, cost, '^', reindeer_x-1, reindeer_y, newCost+1);

		if(move == 'v') Step(map, cost, 'v', reindeer_x+1, reindeer_y, newCost+1);
	}
}

static bool IsPointAccessible(char[][] map, int i, int j)
{
	return !(i < 0 || j < 0 || map[i][j] == '#' ||i >= map.Length || j >= map[0].Length);
}