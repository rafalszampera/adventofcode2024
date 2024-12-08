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
		.Where(x => x.value != '.' && x.value != '#').GroupBy(x => x.value);
		
    //.ToList();
	
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
				
				var a1 = antinode1;
				var a2 = antinode2;
				while(true)
				{ 
					var antinodeDeltaRowIndex = a2.rowIndex - a1.rowIndex;
					var antinodeDeltaColIndex = a2.colIndex - a1.colIndex;
					
					var y = a2.colIndex + antinodeDeltaColIndex;
					var x = a2.rowIndex + antinodeDeltaRowIndex;
					
					if(y >= 0 && y < mapY && x >= 0 && x < mapX )
					{
						if(map[x][y] != '#')
						{
							map[x][y] = '#';
							sumOfAntinodes++;
						}
						a1 = a2;
						a2 = new {value=a2.value, rowIndex=x, colIndex=y};
					}	
					else break;
				}			
			}
		}
	}
	
	(sumOfAntinodes + nodeList.Sum(x => x.Count())).Dump();
}