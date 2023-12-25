namespace Lab7
{
	internal class Generator
	{
		//Метод генерації матриці з числами з плаваючою комою
		public static double[,] Numbers(int size)
		{
			//Ініціалізація матриці заданого розміру
			var matrix = new double[size, size];

			//Створення генератора випадкових чисел
			var random = new Random();

			//Заповнення матриці випадковими числами з плаваючою комою
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
					matrix[i, j] = double.Round(random.NextDouble(), 3) * random.Next(-1, 2);
			}

			return matrix;
		}

		//Метод генерації матриці цілих чисел
		public static int[,] Integers(int size)
		{
			//Ініціалізація матриці заданого розміру
			var matrix = new int[size, size];

			//Створення генератора випадкових чисел
			var random = new Random();

			//Заповнення матриці випадковими цілими числами
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
					matrix[i, j] = random.Next(-9, 10);
			}

			return matrix;
		}
	}
}