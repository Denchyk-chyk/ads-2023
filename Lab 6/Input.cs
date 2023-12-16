namespace Lab_6
{
	internal class Input
	{
		public static sbyte[,] AdjMatrix()
		{
			Console.WriteLine("Кількість вершин:");
			var size = int.Parse(Console.ReadLine());
			var matrix = new sbyte[size, size];

			Console.WriteLine($"Введіть елементи {size}x{size} рядок за рядком:");

			for (int i = 0; i < size; i++)
			{
				string[] rowValues = Console.ReadLine().Split(' ');

				for (int j = 0; j < size; j++)
					matrix[i, j] = sbyte.Parse(rowValues[j]);
			}

			return matrix;
		}

		public static sbyte[,] IncMatrix()
		{
			Console.WriteLine("Кількість вершин:");
			var height = int.Parse(Console.ReadLine()); 

			Console.WriteLine("Кількість ребер:");
			var width = int.Parse(Console.ReadLine());

			var matrix = new sbyte[height, width];

			Console.WriteLine($"Введіть елементи {height}x{width} рядок за рядком:");

			for (int i = 0; i < height; i++)
			{
				string[] rowValues = Console.ReadLine().Split(' ');

				for (int j = 0; j < width; j++)
					matrix[i, j] = sbyte.Parse(rowValues[j]);
			}

			return matrix;
		}
	}
}