using System.Windows.Controls;

namespace Lab_4
{
	internal class DequeSolution(TextBox box)
	{
		public Deque<string> Queue { get; private set; } = new Deque<string>(); //Черга, з якою відбувається вся робота

		private TextBox _box { get; } = box; //Поле з текстом

		//Додати в список
		private string Push(Action<string> action)
		{
			string value = _box.Text.Length > 0 ? _box.Text : "null";
			action(value);
			return $"Додано {value}";
		}

		//Додавання в список тексту з поля для вводу тексту
		public string PushBack() => Push(Queue.PushBack);

		//Додавання в список тексту з поля для вводу тексту
		public string PushForward() => Push(Queue.PushForward);

		//Видалення елемента з краю'
		private string Remove(Func<string> peek, Action pop)
		{
			if (Queue.IsEmpty) return $"Спсиок порожній";
			string value = peek();
			pop();
			return $"Видалено {value}";
		}

		//Видалення першого елемента
		public string RemoveFirst() => Remove(Queue.PeekFirst, Queue.PopFirst);

		//Видалення останнього елемента
		public string RemoveLast() => Remove(Queue.PeekLast, Queue.PopLast);

		//Видалення елемента зі списку, за його значенням, введеним з текствого поля
		public string Remove() => Queue.Remove(_box.Text) ? $"Елемент \"{_box.Text}\" видалено" : $"Елемент \"{_box.Text}\" відсутній";

		//Очищення списку
		public string Clean() => Queue.Clean() ? "Список очищено" : "Список і так порожній";

		//Видалення кожного третього елемента зі списку
		public string RemoveThird()
		{
			int counter = 0, removedCount = 0;

			for (int i = Queue.Length; i > 0; i--)
			{
				if (counter < 2)
				{
					counter++;
					Queue.PushBack(Queue.PeekFirst());
				}
				else
				{
					counter = 0;
					removedCount++;
				}

				Queue.PopFirst();
			};

			return removedCount > 0 ? $"Було видалено елементів - {removedCount}" : "Недостатньо елемнтів для видалення";
		}

		//Перевірка на паліндром
		public string CheckPalindrom()
		{
			Deque<char> queue = new(); //Черга сиволів
			foreach (var symbol in _box.Text)  queue.PushBack(symbol); //Запис рядка в неї

			bool palindrom = true;

			while (palindrom && queue.Length > 1) //Перевірка на паліндром
				if (queue.PeekFirst() != queue.PeekLast()) palindrom = false;

			return (palindrom ? "П" : "Не п") + "аліндром";
		}
	}
}