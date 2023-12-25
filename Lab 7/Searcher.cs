using System.Drawing;

namespace Lab7
{
	using System.Drawing;

	namespace Lab7
	{
		internal class Searcher
		{
			//Метод для пошуку першого нульового елемента на головній діагоналі та першого від'ємного елемента
			public static (int zero, double negative) FindNumbers(double[,] matrix)
			{
				int zero = -1; //Індекс першого знайденого нульового елемента на головній діагоналі
				double negative = 0; //Перший знайдений від'ємний елемент

				int size = matrix.GetLength(0);

				for (int i = 0; i < size; i++)
				{
					if (zero < 0 && matrix[i, i] == 0) zero = i; //Запам'ятовуємо індекс першого знайденого нульового елемента
					else if (matrix[i, i] < 0) negative = matrix[i, i]; //Запам'ятовуємо перший знайдений від'ємний елемент
				}

				return (zero, negative);
			}

			//Метод для пошуку останнього позитивного цілочисельного елемента та його координат
			public static (int value, Point coordinates) FindLastPositiveInteger(int[,] matrix)
			{
				int size = matrix.GetLength(0);
				int value = 0; //Значення останнього знайденого позитивного цілочисельного елемента
				Point coordinates = new(-1, -1); //Координати останнього знайденого позитивного цілочисельного елемента

				for (int x = 0; x < size; x++)
				{
					for (int y = 0; y < size; y++)
					{
						if (matrix[y, x] > 0)
						{
							value = matrix[y, x]; //Запам'ятовуємо значення останнього знайденого позитивного цілочисельного елемента
							coordinates = new(x, y); //Запам'ятовуємо координати останнього знайденого позитивного цілочисельного елемента
						}
					}
				}

				return (value, coordinates);
			}
		}
	}

}