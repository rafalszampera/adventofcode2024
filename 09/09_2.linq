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

string[] MoveNumbers(string[] parsedLine)
{	
	for(var j = parsedLine.Length - 1; j >= 0; j--)
	{
		if(parsedLine[j] == ".") continue;
		var sizeNumered = ComputeSizeOfNumbered(parsedLine, j);
		
		for(var i = 0; i < j; i++)
		{
			var size = ComputeSizeOfEmpty(parsedLine, i);
			
			if(size < sizeNumered) 
			{
				i += size;
				continue;
			}
			
			parsedLine = MoveNumber(parsedLine, i, j, sizeNumered);
			
			break;
		}
		j -= sizeNumered-1;
	}
	
	return parsedLine;
}

string[] MoveNumber(string[]  parsedLine, int i, int j, int size)
{
	while(i < j && size > 0)
	{
		if(i == j) break;
		if(parsedLine[j] == "." || parsedLine[i] != ".") 
			break; 
		
		parsedLine[i] = parsedLine[j];
		parsedLine[j] = ".";
		i++;
		j--;
		size--;
	}
	
	return parsedLine;
}

int ComputeSizeOfEmpty(string[]  parsedLine, int i) 
{
	for(var size = i; size < parsedLine.Length; size++)
	{
		if(parsedLine[size] != ".")
			return size - i;
	}
	
	return (parsedLine.Length - i - 1);
}

int ComputeSizeOfNumbered(string[] parsedLine, int j) 
{
	var firstValue = string.Empty;
	for(var size = j; size > 0 ; size--)
	{			
		if(parsedLine[size] == ".")
			return j - size;
			
		if(firstValue == string.Empty)
			firstValue = parsedLine[size];
		else if(parsedLine[size] != firstValue) 
			return j - size;
	}
	
	return j + 1;
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

long CheckedSum(string[]  parsedLine)
{
	return parsedLine.Select((x, i) => x == "." ? 0 : Convert.ToInt64(x) * i).Sum();
}

#region private::Tests

[Fact] void Test_basic_input_from_day_9_description() => Assert.Equal(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2", ], Parsenumber("12345"));
[Fact] void Test_basic_longer_input_from_day_9_description() => Assert.Equal("00...111...2...333.44.5555.6666.777.888899", string.Join("", Parsenumber("2333133121414131402")));
[Fact] void Test_imaginary_input_from_day_9_description() => Assert.Equal("00...111...2...333.44.5555.6666.777.888899..101010", string.Join("", Parsenumber("233313312141413140223")));

[Fact] void Test_basic_parsed_input_from_day_9_description() => Assert.Equal("0..111....22222", string.Join("",MoveNumbers(Parsenumber("12345"))));
[Fact] void Test_basic_longer_parsed_input_from_day_9_description() => Assert.Equal("00992111777.44.333....5555.6666.....8888..", string.Join("",MoveNumbers(Parsenumber("2333133121414131402"))));

[Fact] void Test_checksum_input_from_day_9_description() => Assert.Equal(60, CheckedSum(["0", "2", "2", "1", "1", "1", "2", "2", "2", ".",  ".",  ".",  ".",  ".",  ".", ]));
[Fact] void Test_basic_checksum_longerd_input_from_day_9_description() => Assert.Equal(2858, CheckedSum(MoveNumbers(Parsenumber("2333133121414131402"))));

[Fact] void Test_CopmuteSizeOfEmpty() => Assert.Equal(2, ComputeSizeOfEmpty(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2"], 1));
[Fact] void Test_CopmuteSizeOfEmpty_for_longer() => Assert.Equal(4, ComputeSizeOfEmpty(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2"], 6));
[Fact] void Test_CopmuteSizeOfEmpty_for_end_of_line() => Assert.Equal(7, ComputeSizeOfEmpty(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  ".", ".", ".", ".",], 6));

[Fact] void Test_ComputeSizeOfNumbered() => Assert.Equal(5, ComputeSizeOfNumbered(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2"], 14));
[Fact] void Test_ComputeSizeOfNumbered_for_longer() => Assert.Equal(3, ComputeSizeOfNumbered(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  "2",  "2",  "2",  "2",  "2"], 5));
[Fact] void Test_ComputeSizeOfNumbered_for_end_of_line() => Assert.Equal(1, ComputeSizeOfNumbered(["0", ".", ".", "1", "1", "1", ".", ".", ".", ".",  ".", ".", ".", ".",], 0));
[Fact] void Test_ComputeSizeOfNumbered_for_end_of_line_long() => Assert.Equal(2, ComputeSizeOfNumbered(Parsenumber("2333133121414131402"), 41));

#endregion