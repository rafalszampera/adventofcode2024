<Query Kind="Program" />

void Main()
{
    string file = File.ReadAllText(@"input");
    int sumofXMAS = 0;
    string[] lines = file.Split('\n');
    char[][] map = lines.Select(line => line.ToCharArray()).ToArray();

    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < map[i].Length; j++)
        {
            if (j + 3 < map[i].Length 						&& IsXMAS(map, i, j, 0, 1)) sumofXMAS++;
            if (j - 3 >= 0 									&& IsXMAS(map, i, j, 0, -1)) sumofXMAS++;
            if (i + 3 < map.Length 							&& IsXMAS(map, i, j, 1, 0)) sumofXMAS++;
            if (i - 3 >= 0 									&& IsXMAS(map, i, j, -1, 0)) sumofXMAS++;
            if (j + 3 < map[i].Length && i + 3 < map.Length && IsXMAS(map, i, j, 1, 1)) sumofXMAS++;
            if (i - 3 >= 0 && j - 3 >= 0 					&& IsXMAS(map, i, j, -1, -1)) sumofXMAS++;
            if (j + 3 < map[i].Length && i - 3 >= 0 		&& IsXMAS(map, i, j, -1, 1)) sumofXMAS++;
            if (i + 3 < map.Length && j - 3 >= 0 			&& IsXMAS(map, i, j, 1, -1)) sumofXMAS++;
        }
    }
	
	sumofXMAS.Dump();
}
bool IsXMAS(char[][] map, int i, int j, int di, int dj)
{
    return map[i][j] == 'X' && map[i + di][j + dj] == 'M' && map[i + 2 * di][j + 2 * dj] == 'A' && map[i + 3 * di][j + 3 * dj] == 'S';
}