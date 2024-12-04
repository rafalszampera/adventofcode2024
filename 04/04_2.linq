<Query Kind="Program" />

void Main()
{
    string file = File.ReadAllText(@"input");
    int sumofXMAS = 0;
    string[] lines = file.Split('\n');
    char[][] map = lines.Select(line => line.ToCharArray()).ToArray();

    for (int i = 1; i < map.Length-1; i++)
    {
        for (int j = 1; j < map[i].Length-1; j++)
        {		
            if (IsXMAS(map, i, j, 1, 1)) sumofXMAS++;
            if (IsXMAS(map, i, j, 1, -1)) sumofXMAS++;
            if (IsXMAS(map, i, j, -1, 1)) sumofXMAS++;
            if (IsXMAS(map, i, j, -1, -1)) sumofXMAS++;
        }
    }
	
	(sumofXMAS/2).Dump();
}
bool IsXMAS(char[][] map, int i, int j, int di, int dj)
{
    return map[i][j] == 'A' 
		&& map[i - di][j - dj] == 'M' && map[i + di][j + dj] == 'S' && 
		(
			(map[i + di][j - dj] == 'S' && map[i - di][j + dj] == 'M')
			|| 
			(map[i + di][j - dj] == 'M' && map[i - di][j + dj] == 'S')
		);
}