<Query Kind="Program" />

void Main()
{
	var leftArray = new List<int>();
	var rightArray = new List<int>();
	
	FileStream fileStream = new FileStream(@"input", FileMode.Open);
	using (StreamReader reader = new StreamReader(fileStream))
	{
        string line = String.Empty;
		while ((line = reader.ReadLine()) != null)
	    {
			var numbers = line.Split("   ");
			leftArray.Add(Convert.ToInt32(numbers[0]));
			rightArray.Add(Convert.ToInt32(numbers[1]));
		}
	}
	
	var sum = leftArray.Sum(left => left * rightArray.Count(right => right == left));
	sum.Dump();
}
