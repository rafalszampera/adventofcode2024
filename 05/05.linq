<Query Kind="Program" />

void Main()
{
	var sum = 0;
	var file = File.ReadAllText(@"input");
	var lines = file.Split(new[] { "\n\n" }, StringSplitOptions.None);
	var rules = new HashSet<string>(lines[0].Split('\n'));
	var orders = lines[1].Split('\n').Select(x => x.Split(','));

	foreach (var order in orders)
	{
		bool isValid = true;
		for (int i = 0; i < order.Length; i++)
		{
			for (int j = i + 1; j < order.Length; j++)
			{
				string pair = $"{order[j]}|{order[i]}"; // check each reversed pair if the order is required to be valid
				if (rules.Contains(pair))
				{
					isValid = false;
					break;
				}
			}
			if (!isValid) break;
		}
		if (isValid)
			sum += Convert.ToInt32(order[order.Length / 2]);
		}
	sum.Dump();
}