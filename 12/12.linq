<Query Kind="Statements">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"


var file = File.ReadAllText(@"input");
string[] lines = file.Split("\r\n");
var map = lines.Select(line => line.Select(plant => plant).ToArray()).ToArray();
var groups = lines.Select(line => line.Select(_ => -1).ToArray()).ToArray();

var visitedNodes = new List<Tuple<int,int>>();
var newGroupNumber = 0;
	
while(groups.SelectMany((x,i) => x.Select((y, j) => new Tuple<int, int>(i, j)).Where((y) => groups[y.Item1][y.Item2] == -1)).Any())
{	
	
	var node = groups.SelectMany((x,i) => x.Select((y, j) => new Tuple<int, int>(i, j)).Where((y) => groups[y.Item1][y.Item2] == -1)).First();
	visitedNodes.Add(node);
	
	if(groups[node.Item1][node.Item2] == -1)
	{	 
		groups[node.Item1][node.Item2] = newGroupNumber;
		newGroupNumber++;
	}
		
	FindGroup( visitedNodes, map, groups, node.Item1, node.Item2, 1, 0);
	FindGroup(visitedNodes, map, groups, node.Item1, node.Item2, -1, 0);
	FindGroup(visitedNodes, map, groups, node.Item1, node.Item2, 0, 1);
	FindGroup(visitedNodes, map, groups, node.Item1, node.Item2, 0, -1);
}

var groupGroup = groups.SelectMany((x, i) => x.Select((y, j) => new {i, j, y})).GroupBy(group => group.y).Select( group => new {Key = group.Key, Elements =group.ToList()});
var totalCost = 0;
foreach(var group in groupGroup) // :)
{
	totalCost += group.Elements.Count() * group.Elements.Select(x => NumberOfBorders(map, x.i, x.j)).Sum();
}
totalCost.Dump();

void FindGroup(List<Tuple<int,int>> visitedNodes, char[][] map, int[][] groups, int i, int j, int di, int dj)
{
	if(!IsPointAccessible(map, i + di, j + dj) || groups[i + di][j + dj] != -1 )
		return;
		
	if(groups[i + di][j + dj] == -1 && map[i][j] == map[i + di][j + dj])
	{
		groups[i + di][j + dj] = groups[i][j];		
			
		FindGroup(visitedNodes, map, groups, i + di, j + dj, 0, -1);
		FindGroup(visitedNodes, map, groups, i + di, j + dj, -1, 0);
		FindGroup(visitedNodes, map, groups, i + di, j + dj, 1, 0);
		FindGroup(visitedNodes, map, groups, i + di, j + dj, 0, 1);
	}
}

static int NumberOfBorders(char[][] map, int i, int j)
{
	var sum = 
		(!IsPointAccessible(map, i+1 ,j) || map[i][j] !=  map[i+1][j]  ? 1: 0) +
		(!IsPointAccessible(map, i-1 ,j) || map[i][j] !=  map[i-1][j]  ? 1: 0) +
		(!IsPointAccessible(map, i ,j+1) || map[i][j] !=  map[i][j+1]  ? 1: 0) +
		(!IsPointAccessible(map, i ,j-1) || map[i][j] !=  map[i][j-1]  ? 1: 0);
	
	return sum;
}

static bool IsPointAccessible(char[][] map, int i, int j)
{
	return !(i < 0 || j < 0 || i >= map.Length || j >= map[0].Length);
}