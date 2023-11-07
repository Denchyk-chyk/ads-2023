using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab_4
{
	internal class Solution
	{
		public Queue Queue = new Queue(); //Черга, з якою відбувається вся робота

		//Додавання в список тексту з поля для вводу тексту
		public void Add(TextBox box)
		{
			if (box.Text.Length > 0)
				Queue.Push(box.Text);
			else
				Queue.Push("Порожнє значення (NULL)");
		}

		//Додавання елемента зі списку, зай його значенням введеним з текствого поля
		public string Remove(TextBox box)
		{
			Queue = Queue.Remove(box.Text, out bool success);
			return success ? $"Елемент \"{box.Text}\" видалено" : $"Елемент \"{box.Text}\" відсутній";
		}

		//Очищення списку
		public void Clear()
		{
			Queue.Clear();
		}

		//Видалення кожного третього елемента зі списку
		public void RemoveThird()
		{
			Queue newQueue = new Queue();
			int counter = 0;

			while (Queue.Peek(out string value))
			{
				if (counter < 2)
				{
					newQueue.Push(value);
					counter++;
				}
				else
				{
					counter = 0;
				}
			}

			Queue = newQueue;
		}
	}
}