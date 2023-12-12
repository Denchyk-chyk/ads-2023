namespace Lab_6_Logics
{
	internal static class ItemsCreator
	{
		public static int[,] AdjacencyMatrix(int size, params (int y, int x)[] adjacent)
		{
			int[,] matrix = new int[size, size];
			foreach ((int y, int x) in adjacent) matrix[y, x] = 1;
			return matrix;
		}

		public static int[,] UnorientedIncidentMatrix(int nodes, int lines, params (int y, int x)[] incidents)
		{
			int[,] matrix = new int[nodes, lines];
			foreach ((int y, int x) in incidents) matrix[y, x] = 1;
			return matrix;
		}

		public static int[,] OrientedIncidentMatrix(int nodes, int lines, params (int y, int x, int value)[] incidents)
		{
			int[,] matrix = new int[nodes, lines];
			foreach ((int y, int x, int value) in incidents) matrix[y, x] = value;
			return matrix;
		}

		public static int[,] AdjMatUnoriented() => AdjacencyMatrix(8,
			(0, 1), (0, 2), (0, 4),
			(1, 0), (1, 3), (1, 4),
			(2, 0), (2, 5), (2, 7),
			(3, 1), (3, 5), (3, 6),
			(4, 0), (4, 1), (4, 6), (4, 7),
			(5, 2), (5, 3), (5, 6), (5, 7),
			(6, 3), (6, 4), (6, 5),
			(7, 2), (7, 4), (7, 5));

		public static int[,] AdjMatOriented() => AdjacencyMatrix(8,
			(1, 0),
			(2, 0),
			(3, 1), (3, 6),
			(4, 0), (4, 1), (4, 2), (4, 3), (4, 5), (4, 6),
			(5, 2),
			(7, 4), (7, 5), (7, 6));

		public static int[,] IncMatUnoriented() => UnorientedIncidentMatrix(8, 14
			(0, 0), (0, 1), (0, 2),
			(1, 0), (1, 2), (1, 3),
			(2, 1), (2, 4), (2, 5),
			(3, 3), (3, 6), (3, 7),
			(4, 0), (4, 1), (4, 2), (4, 3), (4, 5), (4, 6),
			(5, 2),
			(5, 2),
			(7, 4), (7, 5), (7, 6));

		public static int[,] IncMatOriented() => OrientedIncidentMatrix(8, 14,
			(0, 0, 1), (0, 1, 1), (0, 2, 1),
			(1, 0, -1), (1, 3, 1), (1, 4, 1),
			(2, 1, -1), (2, 5, 1), (2, 6, 1),
			(3, 4, -1), (3, 7, 1), (3, 11, -1),
			(4, 2, -1), (4, 3, -1), (4, 5, -1), (4, 7, -1), (4, 8, 1), (4, 9, -1), (4, 12, -1),
			(5, 6, -1), (5, 9, 1), (5, 10, 1),
			(6, 11, 1), (6, 12, 1), (6, 13, 1),
			(7, 8, -1), (7, 9, -1), (7, 13, -1));

		public static void PrintAdj<T>(this T[,] matrix)
		{
			int rows = matrix.GetLength(0);
			int columns = matrix.GetLength(1);

			Console.Write("\\ ");
			for (int j = 0; j < columns; j++) Console.Write($"{(char)('a' + j)} ");
			Console.WriteLine();

			for (int i = 0; i < rows; i++)
			{
				Console.Write($"{(char)('a' + i)} ");
				for (int j = 0; j < columns; j++) Console.Write($"{matrix[i, j]} ");
				Console.WriteLine();
			}

			Console.WriteLine();
		}

		public static void PrintInc<T>(this T[,] matrix)
		{
			int rows = matrix.GetLength(0);
			int columns = matrix.GetLength(1);

			Console.Write("\\ ");
			for (int j = 0; j < columns; j++) Console.Write((j + 1).ToString().PadLeft(3));
			Console.WriteLine();

			for (int i = 0; i < rows; i++)
			{
				Console.Write($"{(char)('a' + i)} ");
				for (int j = 0; j < columns; j++) Console.Write(matrix[i, j].ToString().PadLeft(3));
				Console.WriteLine();
			}

			Console.WriteLine();
		}
	}
}