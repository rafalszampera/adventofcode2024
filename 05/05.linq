<Query Kind="Program" />

void Main()
{
	var sum = 0;
    string file = File.ReadAllText(@"input");
    string[] lines = file.Split("\n\n");
	var rules = lines[0].Split('\n');
	var orders = lines[1].Split('\n').Select(x => x.Split(','));
	
	foreach(var order in orders)
	{		
		var isValud = true;
		for(var i=0; i< order.Length; i++)
		{
			for(var j=i+1; j< order.Length; j++)
			{
				var pair = string.Format("{0}|{1}", order[j], order[i]); // check each reversed pair if  the order is required to be valid
				var forbidenPair = rules.Any(x => x == pair);
				if(forbidenPair)
				{
					isValud=false;
				}
			}
		}
		sum += isValud? Convert.ToInt32(order[order.Length/2]) : 0;
	}	
	sum.Dump();
}