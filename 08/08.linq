<Query Kind="Program" />

void Main()
{	
    int sumOfAntinodes = 0;
    string file = File.ReadAllText(@"input");
    string[] lines = file.Split('\n');
    char[][] map = lines.Select(line => line.ToCharArray()).ToArray();
	var mapX = lines.Length;
	var mapY = lines[0].Length;	
	
	var nodeList = map
		.SelectMany((row, rowIndex) => row.Select((value, colIndex) => new {value, rowIndex, colIndex}))
		.Where(x => x.value != '.').GroupBy(x => x.value);
	
	foreach(var node in nodeList)
	{
		var antinodes = node.ToList();
		for(var i = 0; i < antinodes.Count(); i++)
		{		
			var antinode1 = antinodes[i];	
			for(var j = 0; j < antinodes.Count(); j++)
			{
				if(i == j) continue;
				var antinode2 = antinodes[j];
				var antinodeDeltaRowIndex = antinode2.rowIndex - antinode1.rowIndex;
				var antinodeDeltaColIndex = antinode2.colIndex - antinode1.colIndex;
				
				var y = antinode2.colIndex + antinodeDeltaColIndex;
				var x = antinode2.rowIndex + antinodeDeltaRowIndex;
				
				if(y >= 0 && y < mapY && x >= 0 && x < mapX && map[x][y] != '#')
				{
					map[x][y] = '#';
					sumOfAntinodes++;
				}					
			}
		}
	}
	
	sumOfAntinodes.Dump();
}