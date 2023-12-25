namespace Lab6
{
	internal class Creator
	{
		// Метод для створення та отримання ненапрямленого графа за допомогою матриці суміжності
		public static sbyte[,] InputAdjMatrix(int size, bool mirror, params (char y, char x)[] adjacent)
		{
			var matrix = new sbyte[size, size];
			foreach ((char y, char x) in adjacent)
			{
				// Встановлення зв'язку між вершинами
				matrix[y - 'a', x - 'a'] = 1;
				// Якщо потрібно, встановлення зворотнього зв'язку
				if (mirror) matrix[x - 'a', y - 'a'] = 1;
			}
			return matrix;
		}

		// Метод для демонстрації створення графів та їх виведення
		public static void Display()
		{
			// Створення та виведення ненапрямленого графа
			InputAdjMatrix(8, true,
				('a', 'b'), ('a', 'c'), ('a', 'e'),
				('b', 'd'), ('b', 'e'),
				('c', 'f'), ('c', 'h'),
				('d', 'f'), ('d', 'h'),
				('e', 'g'), ('e', 'h'),
				('f', 'g'), ('f', 'h')).AdjGraphFormat("Ненапрямлений граф");

			// Створення та виведення напрямленого графа
			InputAdjMatrix(8, false,
				('b', 'a'),
				('c', 'a'),
				('d', 'b'), ('d', 'g'),
				('e', 'a'), ('e', 'b'), ('e', 'c'), ('e', 'd'), ('e', 'f'), ('e', 'g'),
				('f', 'c'),
				('h', 'e'), ('h', 'f'), ('h', 'g')).AdjGraphFormat("Напрямлений граф");
		}
	}
}