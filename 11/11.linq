<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
	Blink([41078,18,7,0,4785508,535256,8154, 447], 50).Count().Dump();;
}

long[] Blink(long[] stones, int numberOfBlinks)
{
	if(numberOfBlinks == 0)
		return stones;
		
	var blinkedStones = new List<long>();
	foreach(var stone in stones)
	{
		if(stone == 0)
		{
			blinkedStones.Add(1);
			continue;
		}
			
		var number = stone.ToString();
		if(number.Length % 2 == 0)
		{
	        int mid = number.Length / 2;

	        string firstPart = number.Substring(0, mid);
	        string secondPart = number.Substring(mid);
			blinkedStones.Add(Convert.ToInt64(firstPart));
			blinkedStones.Add(Convert.ToInt64(secondPart));
			
			continue;
		}
		
		blinkedStones.Add(stone* 2024);
	}
	
	return Blink(blinkedStones.ToArray(), numberOfBlinks-1);
}

#region private::Tests

[Fact] void Test_Blink_0() => Assert.Equal([1], Blink([1], 0));

[Fact] void Test_Blink_1() => Assert.Equal([1], Blink([0], 1));
[Fact] void Test_Blink_1_With_1() => Assert.Equal([2024], Blink([1], 1));
[Fact] void Test_Blink_With_Data_From_task_Blink_1() => Assert.Equal([253000, 1, 7], Blink([125, 17], 1));
[Fact] void Test_Blink_With_Data_From_task_Blink_2() => Assert.Equal([253, 0, 2024, 14168], Blink([125, 17], 2));
[Fact] void Test_Blink_With_Data_From_task_Blink_3() => Assert.Equal([512072, 1, 20, 24, 28676032], Blink([125, 17], 3));
[Fact] void Test_Blink_With_Data_From_task_Blink_4() => Assert.Equal([512, 72, 2024, 2, 0, 2, 4, 2867, 6032], Blink([125, 17], 4));
[Fact] void Test_Blink_With_Data_From_task_Blink_5() => Assert.Equal([1036288, 7, 2, 20, 24, 4048, 1, 4048, 8096, 28, 67, 60, 32], Blink([125, 17], 5));
[Fact] void Test_Blink_With_Data_From_task_Blink_6() => Assert.Equal([2097446912, 14168, 4048, 2, 0, 2, 4, 40, 48, 2024, 40, 48, 80, 96, 2, 8, 6, 7, 6, 0, 3, 2], Blink([125, 17], 6));
[Fact] void Test_Blink_With_Data_From_task_Blink_22() => Assert.Equal(55312, Blink([125, 17], 25).Count());



#endregion