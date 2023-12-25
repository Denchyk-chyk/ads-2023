using System.Drawing;

namespace Lab7
{
	internal static class Output
	{
		// Метод для виведення знайдених нульових та від'ємних елементів
		public static void FoundNumbers((int zero, double negative) values)
		{
			Console.Write("\n" + (values.zero < 0 ? "Нульові елементи відсутні!" : $"({values.zero}; {values.zero})") + $" | ");
			Console.Write((values.negative == 0 ? "Від'ємні елементи відсутні!" : values.negative) + "\n");
		}

		// Метод для виведення останнього знайденого позитивного цілочисельного елемента та його координат
		public static void LastFoundPositiveInteger((int value, Point coordinates) integer)
		{
			if (integer.value == 0) Console.WriteLine("Додатні числа відсутні!");
			else Console.WriteLine($"{integer.value} | ({integer.coordinates.X}; {integer.coordinates.Y})");
		}

		// Метод для виведення матриці на екран з вказаною шириною вирівнювання
		private static void Print<T>(this T[,] matrix, int place)
		{
			var size = matrix.GetLength(0);

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++) Console.Write(matrix[y, x].ToString().PadLeft(place));
				Console.WriteLine();
			}
		}

		// Метод для виведення цілочисельної матриці
		public static void Print(this int[,] matrix) => Print(matrix, 3);

		// Метод для виведення матриці з плаваючою комою
		public static void Print(this double[,] matrix) => Print(matrix, 7);
	}
}