using System.ComponentModel.Design;

namespace Lab8
{
	internal class Sorter
	{
		public static int[] BubbleSort(int[] array, out int difficulty)
		{
			difficulty = 0;
			//Створення копії вхідного масиву
			var sorted = new int[array.Length];
			Array.Copy(array, sorted, array.Length);

			//Зовнішній цикл пройде через усі елементи масиву
			for (int i = 0; i < sorted.Length; i++)
			{
				//Внутрішній цикл порівнює та обмінює сусідні елементи
				for (int j = i + 1; j < sorted.Length; j++)
				{
					if (sorted[i] > sorted[j])
					{
						//Обмін елементів
						(sorted[i], sorted[j]) = (sorted[j], sorted[i]);
					}
					difficulty++; //Збільшення лічильника операцій
				}
			}

			return sorted;
		}

		public static int[] InsertionSort(int[] array, out int difficulty)
		{
			//Використання черги для простоти вставки нового елемента в відсортований список
			Queue<int> unsorted = new(array);
			//Створення впорядкованого зв'язаного списку
			LinkedList<int> sorted = new();

			//Додаємо перший елемент до відсортованого списку
			sorted.AddFirst(unsorted.Peek());
			unsorted.Dequeue();

			difficulty = 2;

			//Проходження через решту елементів у несортованій черзі
			while (unsorted.Count > 0)
			{
				int current = unsorted.Peek();
				bool inserted = false;
				var node = sorted.First;

				//Проходження через відсортований список та вставка нового елемента на відповідне місце
				do
				{
					difficulty++;

					if (current < node.Value)
					{
						sorted.AddBefore(node, current);
						inserted = true;
						break;
					}

					node = node.Next;
				}
				while (node != null);

				//Якщо елемент не вставлено, додаємо його в кінець
				if (!inserted) sorted.AddLast(unsorted.Peek());
				unsorted.Dequeue();
			}

			return [.. sorted];
		}

		public static int[] QuickSort(int[] array, out int difficulty)
		{
			difficulty = 0;
			return [.. QuickSort([.. array], ref difficulty)];
		}

		public static List<int> QuickSort(List<int> list, ref int difficulty)
		{
			//Рекурсивний метод швидкого сортування
			if (list.Count < 2) return list;

			//Вибір опорного елемента випадковим чином
			int pivot = list[new Random().Next(0, list.Count)];
			List<int> smaller = [], equal = [], bigger = [];

			//Розподіл елементів навколо опорного
			foreach (var item in list)
			{
				if (item < pivot) smaller.Add(item);
				else if (item > pivot) bigger.Add(item);
				else equal.Add(item);
				difficulty++;
			}

			//Рекурсивно сортуємо менші та більші списки
			list.Clear();
			list.AddRange(QuickSort(smaller, ref difficulty));
			list.AddRange(equal);
			list.AddRange(QuickSort(bigger, ref difficulty));
			difficulty += list.Count;

			return list;
		}

		public static int[] MergeSort(int[] array, out int difficulty)
		{
			difficulty = 0;
			//Створення копії вхідного масиву
			int[] sorted = new int[array.Length];
			Array.Copy(array, sorted, array.Length);
			//Його сортування злиттям
			MergeSortRecursive(sorted, ref difficulty);
			return sorted;
		}

		private static void MergeSortRecursive(int[] array, ref int difficulty)
		{
			//Рекурсивний метод злиття
			if (array.Length < 2) return;

			int middle = array.Length / 2;
			int[] left = new int[middle];
			int[] right = new int[array.Length - middle];

			//Копіювання елементів в окремі підмасиви
			Array.Copy(array, 0, left, 0, middle);
			Array.Copy(array, middle, right, 0, array.Length - middle);
			difficulty += array.Length;

			//Рекурсивний виклик для лівого та правого підмасивів
			MergeSortRecursive(left, ref difficulty);
			MergeSortRecursive(right, ref difficulty);

			//Злиття відсортованих підмасивів
			Merge(array, left, right, ref difficulty);
		}

		private static void Merge(int[] result, int[] left, int[] right, ref int difficulty)
		{
			int l = 0, r = 0, i = 0;

			//Злиття двох підмасивів у відсортований масив
			while (l < left.Length && r < right.Length)
			{
				if (left[l] <= right[r]) result[i++] = left[l++];
				else result[i++] = right[r++];

				difficulty++;
			}

			//Завершення злиття залишків
			while (l < left.Length)
			{
				result[i++] = left[l++];
				difficulty++;
			}

			while (r < right.Length)
			{
				result[i++] = right[r++];
				difficulty++;
			}
		}

		public static int[] HeapSort(int[] array, out int difficulty)
		{
			//Використання пріоритетної черги для побудови бінарної купи
			Queue<int> sorted = new();
			PriorityQueue<int, int> heap = new();
			difficulty = 0;

			//Додавання елементів до купи
			for (int i = 0; i < array.Length; i++)
			{
				heap.Enqueue(i, array[i]);
				difficulty += (int)Math.Log2(heap.Count);
			}

			//Вилучення елементів з купи в відсортований список
			while (heap.Count > 0)
			{
				difficulty += (int)Math.Log2(heap.Count);
				sorted.Enqueue(array[heap.Dequeue()]);
			}

			return [.. sorted];
		}

		public static int[] CountingSort(int[] array, out int difficulty)
		{
			//Використання словника для підрахунку кількості елементів
			Dictionary<int, int> counted = [];
			difficulty = 0;

			//Підрахунок елементів у вхідному масиві
			foreach (var item in array)
			{
				if (counted.ContainsKey(item)) counted[item]++;
				else counted[item] = 1;
				difficulty += (int)Math.Log2(counted.Count) * 2;
			}

			//Створення відсортованого масиву з підрахованих значень
			var sorted = new int[array.Length];
			int index = 0, min, count = 0;

			while (counted.Count > 0)
			{
				min = int.MaxValue;

				//Пошук мінімального значення у словнику
				foreach (var item in counted)
				{
					if (item.Key < min)
					{
						min = item.Key;
						count = item.Value;
					}
					difficulty++;
				}

				//Заповнення відсортованого масиву знайденим значенням
				Array.Fill(sorted, min, index, count);
				index += count;
				difficulty += (int)Math.Log2(counted.Count);
				counted.Remove(min);
			}

			return sorted;
		}

		public delegate int[] Sort(int[] array, out int difficulty);
	}
}