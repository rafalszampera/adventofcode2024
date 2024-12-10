<Query Kind="Statements">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"


var file = File.ReadAllText(@"input");
string[] lines = file.Split("\r\n");
var map = lines.Select(line => line.Select(c => c - '0').ToArray()).ToArray();

ComputeTrailheads(map).Dump();


int ComputeTrailheads(int[][] map)
{
	var sumOfPoints = 0;
	var points = map.SelectMany((row, x) => row.Select((value, y) => new {value, x, y}).Where(point => point.value == 0)).ToList();
	
	foreach(var p in points)
	{
		var copyPoints = new List<dynamic>();
		var topPoints = new List<dynamic>();
		copyPoints.Add(p);
		
		while(copyPoints.Any())
		{
			var point = copyPoints.First();	
			if(point.value == 9) topPoints.Add(point);
			copyPoints.RemoveAt(0);
				
			if (IsPointAccessible(map, point.x + 1, point.y)
				&& IsNearPointDifferenceByOne(map, point.x, point.y, 1, 0))
			{
				copyPoints.Add(new { value = (int)map[point.x + 1][point.y], x = point.x + 1, y = point.y});
			}
			if (IsPointAccessible(map, point.x - 1, point.y) 
				&& IsNearPointDifferenceByOne(map, point.x, point.y,  -1, 0))
			{
				copyPoints.Add(new { value = (int)map[point.x - 1][point.y], x = point.x - 1, y = point.y});
			}		
			if (IsPointAccessible(map, point.x, point.y + 1)  
				&& IsNearPointDifferenceByOne(map, point.x, point.y, 0, 1))
			{
				copyPoints.Add(new { value = (int)map[point.x][point.y + 1], x = point.x, y = point.y + 1});
			}		
			if (IsPointAccessible(map, point.x, point.y - 1)  
				&& IsNearPointDifferenceByOne(map, point.x, point.y, 0, -1))
			{
				copyPoints.Add(new { value = (int)map[point.x][point.y - 1], x = point.x, y = point.y - 1});
			}
		}
		
		sumOfPoints += topPoints.DistinctBy(x => x).Count();
	}
	return sumOfPoints;
}

static bool IsNearPointDifferenceByOne(int[][] map, int i, int j, int di, int dj)
{
	return map[i + di][j+dj] == map[i][j] + 1;
}

static bool IsPointAccessible(int[][] map, int i, int j)
{
	return !(i < 0 || j < 0 || i >= map.Length || j >= map[0].Length);
}

#region private::Tests

[Fact] void check_1_element_map() => Assert.Equal(0, ComputeTrailheads([[0]]));
[Fact] void check_2x2_map() => Assert.Equal(2, ComputeTrailheads([[0,1,2,3,4,5,6,7,8,9], [1, 2,3,4,5,6,7,8,9,8]]));


[Fact] void check_IsPointAccesable_0_0() => Assert.True(IsPointAccessible([[0, 1, 2], [1, 2, 3], [2, 3, 4]], 0, 0));
[Fact] void check_IsPointAccesable_middle() => Assert.True(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 1, 1));
[Fact] void check_IsPointAccesable_2_2() => Assert.True(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 2, 2));
[Fact] void check_false_IsPointAccesable_3_2() => Assert.False(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 3, 2));
[Fact] void check_false_IsPointAccesable_2_3() => Assert.False(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 2, 3));
[Fact] void check_false_IsPointAccesable__1_0() => Assert.False(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], -1, 0));
[Fact] void check_false_IsPointAccesable_0__1() => Assert.False(IsPointAccessible([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 0, -1));


[Fact] void check_IsNearPointDifferenceByOne_0_0() => Assert.True(IsNearPointDifferenceByOne([[0, 1, 2], [1, 2, 3], [2, 3, 4]], 0, 0, 0, 1));
[Fact] void check_IsNearPointDifferenceByOne_middle() => Assert.True(IsNearPointDifferenceByOne([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 1, 1, 1, 0));
[Fact] void check_IsNearPointDifferenceByOne_2_2() => Assert.False(IsNearPointDifferenceByOne([[0, 1, 3], [1, 2, 3], [2, 3, 5]], 2, 2, 0, -1));
[Fact] void check_false_IsNearPointDifferenceByOne_3_2() => Assert.False(IsNearPointDifferenceByOne([[0, 1, 3], [1, 2, 4], [2, 3, 5]], 2, 1, 0, 1));

#endregion