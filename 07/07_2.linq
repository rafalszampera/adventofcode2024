<Query Kind="Program" />

void Main()
{
	ulong totalSum = 0;
	var file = File.ReadAllText(@"input");
	var lines = file.Split("\n");
	
	foreach(var line in lines)
	{
		var values = line.Split(':');
		var sum = (ulong)Convert.ToInt64(values[0]);		
		var numbers = values[1].Split(' ').Where(x=>x != "").Select(x => ((ulong)Convert.ToInt64(x))).ToArray();		
		var result = Compute(numbers, 0, sum);
		
		totalSum += result ? sum : 0;
	}
	totalSum.Dump();
}

bool Compute(ulong[] numbers, ulong currentSum, ulong sum)
{

	if(numbers.Length == 0)
		return currentSum == sum;	
	else
	{
		var number = numbers.First();
		var newNumbers = numbers.Skip(1).ToArray();;
		
		return Compute(newNumbers, number * (currentSum == 0 ? 1 : currentSum), sum) 
		|| Compute(newNumbers, number + currentSum, sum)
		|| Compute(newNumbers, 
			(
				(ulong)Convert.ToInt64
				(
					currentSum.ToString() + number.ToString()
				)
		), sum);
	}
}