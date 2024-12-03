<Query Kind="Program" />

void Main()
 {
    var pattern = @"mul\(\d{1,3},\d{1,3}\)";
	var sum = 0;
	
	
	string line = File.ReadAllText(@"C:\EDF\adventofcode2024\03\input");
   
	MatchCollection matches = Regex.Matches(line, pattern);
    foreach (Match match in matches)
    {    
        var text = match.Groups[0].Value;
        var numbers = text.Substring(4, text.Length - 4 - 1).Split(',');
        sum += Convert.ToInt32(numbers[0]) * Convert.ToInt32(numbers[1]);
    }
	sum.Dump();
}