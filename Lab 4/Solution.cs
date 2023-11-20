using System.Windows.Controls;

namespace Lab_4
{
	internal class Solution
	{
		public Queue<string> Queue { get; private set; } = new Queue<string>(); //Черга, з якою відбувається вся робота

		//Додавання в список тексту з поля для вводу тексту
		public string Add(TextBox box)
		{
			string value = box.Text.Length > 0 ? box.Text : "null";
			Queue.PushBack(value);
			return $"Додано {value}";
		}

		//Видалення елемента зі списку, за його значенням, введеним з текствого поля
		public string Remove(TextBox box) => Queue.Remove(box.Text) ? $"Елемент \"{box.Text}\" видалено" : $"Елемент \"{box.Text}\" відсутній";

		//Очищення списку
		public string Clear() => Queue.Clean() ? "Список очищено" : "Список і так порожній";

		//Видалення кожного третього елемента зі списку
		public string RemoveThird()
		{
			int counter = 0, removedCount = 0;

			Queue.DoForEach(value =>
			{
				if (counter < 2)
				{
					counter++;
				}
				else
				{
					Queue.RemoveLast();
					counter = 0;
					removedCount++;
				}
			});

			return removedCount > 0 ? $"Було видалено елементів - {removedCount}" : "Недостатньо елемнтів для видалення";
		}
	}
}