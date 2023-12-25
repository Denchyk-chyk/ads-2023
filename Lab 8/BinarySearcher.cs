namespace Lab8
{
	internal class BinarySearcher(int[] source, int[] sorted)
	{
		private readonly int[] _source = source; //Масив вхідних елементів для їх пошуку
		private readonly int[] _sorted = sorted; //Відсортований масив для виконання з нього бінарного пошуку
		private readonly string[] _result = source.Select(x => $"{x} | - ").ToArray(); //Масив результатів, що містить індекси звідсортованого масиву для кожного вхідного елементу

		//Метод для здійснення бінарного пошуку для всіх елементів вхідного масиву
		public string[] Find()
		{
			for (int i = 0; i < _source.Length; i++) Find(i, 0, _sorted.Length);
			return _result;  //Повертаємо масив результатів
		}

		//Рекурсивний метод для здійснення бінарного пошуку для конкретного вхідного елемента
		private void Find(int source, int first, int last)
		{
			if (first >= last) return;  //Умова виходу з рекурсії: діапазон не має елементів

			int middle = first + (last - first) / 2;  //Знаходження середнього елемента діапазону

			if (_source[source] == _sorted[middle])
				_result[source] = _result[source][..^2] + $"{middle} ";  //Оновлення результату, якщо елемент знайдено
			else if (_source[source] > _sorted[middle])
				Find(source, middle + 1, last);  //Рекурсивний виклик для правої половини
			else
				Find(source, first, middle);  //Рекурсивний виклик для лівої половини
		}
	}
}