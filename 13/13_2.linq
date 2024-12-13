<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"

void Main()
{
	var inputs = File.ReadAllText(@"input").Split('\n').Chunk(4).ToList();
	
  	inputs
		.Sum(input => 
		    {		
		        var v1 = input[0].Split(", ").Select(v => Convert.ToInt64(v.Split("+").Last())).ToArray();
		        var v2 = input[1].Split(", ").Select(v => Convert.ToInt64(v.Split("+").Last())).ToArray();
		        var r = input[2].Split(", ").Select(n => 10000000000000 + Convert.ToInt64(n.Split("=").Last())).ToArray();
				
		        return Solve(new long[][] {v1, v2}, r);
		    }
		).Dump();
}

long Solve(long[][] values, long[] res)
{
    var (rx, ry) = (res[0], res[1]);
    var (x1, y1) = (values.First()[0], values.First()[1]);
    var (x2, y2) = (values.Last()[0], values.Last()[1]);
	
    var b = (y1 * rx - x1 * ry) / (y1 * x2 - x1 * y2);
    var a = (rx - x2 * b) / x1;
	
    return ((y1 * rx - x1 * ry) % (y1 * x2 - x1 * y2) == 0 &&  (rx - x2 * b) % x1 == 0) ? a * 3 + b : 0;
}

#region private::Tests

[Fact] void EquationSolver() => Assert.Equal(792079208636, Solve([[35, 12],[17, 52]], [10000000009516, 10000000013408]));

#endregion