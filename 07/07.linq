<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	long totalSum = 0;
	var file = File.ReadAllText(@"input");
	var lines = file.Split("\n");
	
	foreach(var line in lines)
	{
		var values = line.Split(':');
		var sum = Convert.ToInt64(values[0]);		
		var numbers = values[1].Split(' ').Where(x=>x != "").Select(x => Convert.ToInt64(x)).ToArray();		
		var result = Compute(numbers, 0, sum);
		
		totalSum += result ? sum : 0;		
	}
	totalSum.Dump();
}

bool Compute(long[] numbers, long currentSum, long sum)
{

	if(numbers.Length == 0)
		return currentSum == sum;	
	else
	{
		var number = numbers.First();
		var newNumbers = numbers.Skip(1).ToArray();;
		
		return Compute(newNumbers, number * (currentSum == 0 ? 1 : currentSum), sum) || Compute(newNumbers, number + currentSum, sum);
	}
}