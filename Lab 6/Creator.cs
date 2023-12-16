namespace Lab6
{
	internal class Creator
	{
		public static sbyte[,] InputAdjMatrix(int size, bool mirror, params (char y, char x)[] adjacent)
		{
			var matrix = new sbyte[size, size];
			foreach ((char y, char x) in adjacent)
			{
				matrix[y - 'a', x - 'a'] = 1;
				if (mirror) matrix[x - 'a', y - 'a'] = 1;
			}
			return matrix;
		}

		public static void Display()
		{
			InputAdjMatrix(8, true,
			('a', 'b'), ('a', 'c'), ('a', 'e'),
			('b', 'd'), ('b', 'e'),
			('c', 'f'), ('c', 'h'),
			('d', 'f'), ('d', 'h'),
			('e', 'g'), ('e', 'h'),
			('f', 'g'), ('f', 'h')).IncGraphFormat("Ненапрямлений граф");

			InputAdjMatrix(8, false,
			('b', 'a'),
			('c', 'a'),
			('d', 'b'), ('d', 'g'),
			('e', 'a'), ('e', 'b'), ('e', 'c'), ('e', 'd'), ('e', 'f'), ('e', 'g'),
			('f', 'c'),
			('h', 'e'), ('h', 'f'), ('h', 'g')).IncGraphFormat("Напрямлений граф");
		}
	}
}