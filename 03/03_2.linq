<Query Kind="Program" />

void Main()
 {
    var pattern = @"mul\((\d{1,3}),(\d{1,3})\)|(do\(\))|(don't\(\))";
	var sum = 0;
	var disable = false;
	string line = File.ReadAllText(@"input");
   
	MatchCollection matches = Regex.Matches(line, pattern);
    foreach (Match match in matches)
    {    
        var text = match.Groups[0].Value;
		text.Dump();
		if(text.StartsWith("don't()"))
		{
			disable = true;
			continue;
		}
		else if (text.StartsWith("do()"))
		{
			disable = false;
			continue;
		}
		else if(!disable)
		{
	        var numbers = text.Substring(4, text.Length - 4 - 1).Split(',');
	        sum += Convert.ToInt32(numbers[0]) * Convert.ToInt32(numbers[1]);
		}
    }
	sum.Dump();
}