<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
	Blink([41078,18,7,0,4785508,535256,8154, 447], 75).Sum(x=> x.Value).Dump();
}

Dictionary<ulong, long> Blink(ulong[] stones, int numberOfBlinks)
{
	var dict = new Dictionary<ulong, long>();
	
	foreach(var stone in stones)
		dict.Add(stone, 1);
		
	return Blink(numberOfBlinks, dict);
}

Dictionary<ulong, long> Blink(int numberOfBlinks, Dictionary<ulong, long> dict)
{
	if(numberOfBlinks == 0)
		return dict;
		
	var blinkedStones = new Dictionary<ulong, long>();
	foreach(var stoneDict in dict)
	{
		var stone = stoneDict.Key;
		if(stone == 0)
		{
			AddValueToDict(blinkedStones, (ulong)Convert.ToInt64(1), stoneDict.Value);
				
			continue;
		}
			
		var number = stone.ToString();
		if(number.Length % 2 == 0)
		{
	        int mid = number.Length / 2;

	        string firstPart = number.Substring(0, mid);
	        string secondPart = number.Substring(mid);
			
			AddValueToDict(blinkedStones, (ulong)Convert.ToInt64(firstPart), stoneDict.Value);
			AddValueToDict(blinkedStones, (ulong)Convert.ToInt64(secondPart), stoneDict.Value);	
			
			continue;
		}
		AddValueToDict(blinkedStones, (ulong)Convert.ToInt64(stone * 2024), stoneDict.Value);
	}
	
	return Blink(numberOfBlinks - 1, blinkedStones);
}

void AddValueToDict(Dictionary<ulong, long> blinkedStones,ulong stone, long stoneValue)
{
	if(blinkedStones.ContainsKey((ulong)Convert.ToInt64(stone)))
		blinkedStones[(ulong)Convert.ToInt64(stone)] += stoneValue;
	else
		blinkedStones.Add((ulong)Convert.ToInt64(stone), stoneValue);
}

#region private::Tests

[Fact] void Test_Blink_0() => Assert.Equal(1, Blink([1], 0).Count());

[Fact] void Test_Blink_1() => Assert.Equal(1, Blink([0], 1).Count());
[Fact] void Test_Blink_1_With_1() => Assert.Equal(1, Blink([1], 1).Count());

[Fact] void Test_Blink_With_Data_From_task_Blink_1() => Assert.Equal(3, Blink([125, 17], 1).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_2() => Assert.Equal(4, Blink([125, 17], 2).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_3() => Assert.Equal(5, Blink([125, 17], 3).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_4() => Assert.Equal(9, Blink([125, 17], 4).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_5() => Assert.Equal(13, Blink([125, 17], 5).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_6() => Assert.Equal(22, Blink([125, 17], 6).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_10() => Assert.Equal(109, Blink([125, 17], 10).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_11() => Assert.Equal(170, Blink([125, 17], 11).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_12() => Assert.Equal(235, Blink([125, 17], 12).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_15() => Assert.Equal(853, Blink([125, 17], 15).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_20() => Assert.Equal(6837, Blink([125, 17], 20).Sum(x=> x.Value));
[Fact] void Test_Blink_With_Data_From_task_Blink_25() => Assert.Equal(55312, Blink([125, 17], 25).Sum(x=> x.Value));

#endregion