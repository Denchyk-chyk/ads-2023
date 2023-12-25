namespace Lab6
{
	internal static class Output
	{
		// Внутрішній метод для форматування графів за матрицями суміжності та включення
		private static void GraphFormat(sbyte[,] adjMatrix, sbyte[,] incMatrix, string header)
		{
			Console.WriteLine($"\n{header}\n");

			// Виведення матриці суміжності
			adjMatrix.AdjFormat();

			// Виведення матриці включення
			incMatrix.IncFormat();

			// Виведення списку вузлів з матриці включення
			incMatrix.ToNodes().NodesFormat();

			// Виведення списку суміжності з матриці суміжності
			adjMatrix.ToAdjList().AdjListFormat();
		}

		// Метод для форматування та виведення графу, заданого матрицею суміжності
		public static void AdjGraphFormat(this sbyte[,] adjMatrix, string header)
		{
			var incMatrix = adjMatrix.ToInc();
			GraphFormat(adjMatrix, incMatrix, header);
		}

		// Метод для форматування та виведення графу, заданого матрицею включення
		public static void IncGraphFormat(this sbyte[,] incMatrix, string header)
		{
			var adjMatrix = incMatrix.ToAdj();
			GraphFormat(adjMatrix, incMatrix, header);
		}

		// Метод для виведення матриці суміжності
		public static void AdjFormat(this sbyte[,] matrix)
		{
			int rows = matrix.GetLength(0);
			int columns = matrix.GetLength(1);

			Console.Write("\\ | ");
			for (int j = 0; j < columns; j++) Console.Write($"{(char)('a' + j)} ");
			Console.WriteLine("\n" + new string('-', (columns + 2) * 2 - 1));

			for (int i = 0; i < rows; i++)
			{
				Console.Write($"{(char)('a' + i)} | ");
				for (int j = 0; j < columns; j++) Console.Write($"{matrix[i, j]} ");
				Console.WriteLine();
			}

			Console.WriteLine();
		}

		// Метод для виведення матриці включення
		public static void IncFormat(this sbyte[,] matrix)
		{
			int rows = matrix.GetLength(0);
			int columns = matrix.GetLength(1);

			Console.Write("\\ |");
			for (int j = 0; j < columns; j++) Console.Write((j + 1).ToString().PadLeft(3));
			Console.WriteLine("\n" + new string('-', (columns + 1) * 3));

			for (int i = 0; i < rows; i++)
			{
				Console.Write($"{(char)('a' + i)} |");
				for (int j = 0; j < columns; j++) Console.Write(matrix[i, j].ToString().PadLeft(3));
				Console.WriteLine();
			}

			Console.WriteLine();
		}

		// Метод для виведення списку вузлів
		public static void NodesFormat(this (sbyte first, sbyte second)[] list) =>
			Console.WriteLine(string.Join(" ", list.Select(t => $"({(char)('A' + t.first)}, {(char)('A' + t.second)})")) + "\n");

		// Метод для виведення списку суміжності
		public static void AdjListFormat(this Dictionary<sbyte, sbyte[]> list)
		{
			foreach (var item in list)
				Console.WriteLine($"{(char)('A' + item.Key)} → {{{string.Join(", ", item.Value.Select(num => (char)('A' + num)))}}}");

			Console.WriteLine();
		}
	}
}
