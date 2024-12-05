<Query Kind="Program" />

void Main()
{
	var file = File.ReadAllText(@"C:\EDF\adventofcode2024\05\input");
	var lines = file.Split("\n\n");
	var rules = new HashSet<string>(lines[0].Split('\n'));
	var orders = lines[1].Split('\n').Select(x => x.Split(',')).ToArray();
	var unvalid = new HashSet<int>();
	
	for(var k=0; k < orders.Count(); k++)
	{		
		var order = orders[k];
		var isValud = ShuffleOrder(rules, orders, k);
		if(!isValud) 
		{
			unvalid.Add(k);
			k--;
		}
	}
	
	orders
		.Where((order, index) => unvalid.Contains(index))
		.Sum(order => Convert.ToInt32(order[order.Length / 2]))
		.Dump();
}	

private static bool ShuffleOrder(HashSet<string> rules, string[][] orders, int k)
{
	var order = orders[k];
	var isValud = true;
	for (var i = 0; i < order.Length; i++)
	{
		for (var j = i + 1; j < order.Length; j++)
		{
			var pair = string.Format("{0}|{1}", order[j], order[i]); // check each reversed pair if the reversed order is required to be valid
			var forbidenPair = rules.Contains(pair);
			if (forbidenPair)
			{
				ReplaceStringsInOrder(order, i, j);
				isValud = false;
				i = 0;
				break;
			}
		}
	}

	return isValud;
}

private static void ReplaceStringsInOrder(string[] order, int i, int j)
{
	var val1 = order[j];
	var val2 = order[i];
	order[j] = val2;
	order[i] = val1;
}