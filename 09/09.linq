<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"	

void Main()
{
    var file = File.ReadAllText(@"input");
    var lines = file.Split('\n').First();
	
	var parsedLine = Parsenumber(file);
	var movedParsedLine = MoveNumbers(parsedLine);
	
	CheckedSum(movedParsedLine).Dump();
}

long CheckedSum(string[]  parsedLine)
{
	return parsedLine.Where(x => x != ".").Select((x,i) => Convert.ToInt64(x)*i).Sum();
}

string[] MoveNumbers(string[]  parsedLine)
{
 	var i = 0;
	var j = parsedLine.Length - 1;
	while(i < j)
	{
		if(i == j) break;
		if(parsedLine[j] == ".") {j--; continue; }
		if(parsedLine[i] != ".") {i++; continue; }
		parsedLine[i] = parsedLine[j];
		parsedLine[j] = ".";
		i++;
		j--;
	}
	
	return parsedLine;
}

string[] Parsenumber(string number)
{
	var sb = new List<string>();
	var isfreeSpace = false;
	var filenumber = 0;
	foreach(var c in number)
	{
		var value = Convert.ToInt32(c) - 48;

		sb.AddRange(Enumerable.Repeat(isfreeSpace ? "." : (filenumber).ToString(), value));

		if(!isfreeSpace) filenumber++;

		isfreeSpace =!isfreeSpace;
	}
	return sb.ToArray();
}

#region private::Tests

[Fact] void Test_basic_input_from_day_9_description() => Assert.Equal(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2", ], Parsenumber("12345"));
[Fact] void Test_basic_longer_input_from_day_9_description() => Assert.Equal("00...111...2...333.44.5555.6666.777.888899", string.Join("", Parsenumber("2333133121414131402")));
[Fact] void Test_imaginary_input_from_day_9_description() => Assert.Equal("00...111...2...333.44.5555.6666.777.888899..101010", string.Join("", Parsenumber("233313312141413140223")));

[Fact] void Test_basic_parsed_input_from_day_9_description() => Assert.Equal("022111222......", string.Join("",MoveNumbers(Parsenumber("12345"))));
[Fact] void Test_basic_longer_parsed_input_from_day_9_description() => Assert.Equal("0099811188827773336446555566..............", string.Join("",MoveNumbers(Parsenumber("2333133121414131402"))));

[Fact] void Test_checksum_input_from_day_9_description() => Assert.Equal(60, CheckedSum(["0", "2", "2", "1", "1", "1", "2", "2", "2", ".",  ".",  ".",  ".",  ".",  ".", ]));
[Fact] void Test_basic_checksum_longerd_input_from_day_9_description() => Assert.Equal(1928, CheckedSum(MoveNumbers(Parsenumber("2333133121414131402"))));

#endregion