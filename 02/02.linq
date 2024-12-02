<Query Kind="Program" />

void Main()
{
    int safeLevelCount = 0;

    using (var reader = new StreamReader(@"input"))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();
            var differences = numbers.Zip(numbers.Skip(1), (prev, current) => current - prev).ToArray();

            bool isUnsafe = differences.Any(diff => Math.Abs(diff) > 3) ||
							differences.Any(diff => diff == 0) ||
                            (differences.Any(diff => diff > 0) && differences.Any(diff => diff < 0));

            if (!isUnsafe)
            {
                safeLevelCount++;
            }
        }
    }

    safeLevelCount.Dump();
}