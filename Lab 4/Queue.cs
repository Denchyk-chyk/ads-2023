namespace Lab_4
{
	//Користувацька реалізація черги
	internal class Queue
	{
		public bool IsntEmpty { get; private set; } //Збергіає те чи черга не порожня

		private Node _head = default; //Перший елемент
		private Node _tail = default; //Останній елемент

		//Додавання елемента
		public void Push(string value)
		{
			if (_head == default)
			{
				_head = new Node(value, default, default);
				_tail = _head;
				IsntEmpty = true;
			}
			else
			{
				Node formerHead = _head;
				_head = new Node(value, default, formerHead);
				formerHead.Previous = _head;
			}
		}

		//Видалення поточного елемента
		public bool Peek(out string value)
		{
			value = IsntEmpty ? _tail.Value : string.Empty;
			return Pop();
		}

		//Перегляд та видалення поточного елемента
		public bool Pop()
		{
			if (IsntEmpty)
			{
				_tail = _tail.Previous;

				if (_tail == default)
				{
					IsntEmpty = false;
					_head = default;
				}
			}

			return IsntEmpty;
		}

		//Очищення черги
		public void Clear()
		{
			_head = default;
			_tail = default;
		}

		//Видалення елемента за його значенням
		public Queue Remove(string value, out bool success)
		{
			Queue newQueue = new Queue();
			success = false;

			while (newQueue.Peek(out string current))
			{
				if (success || !value.Equals(current))
				{
					newQueue.Push(current);
					success = true;
				}
			}

			return newQueue;
		}
	}

	//Користувацька реалізація вузлів зв'язаних списків (тут стеків)
	public class Node
	{
		public string Value { get; set; } //Значення
		public Node Next { get; set; } //Посилання на наступний елемент
		public Node Previous { get; set; } //Посилання на попередній елемент

		public Node(string value, Node previous, Node next)
		{
			Value = value;
			Next = next;
			Previous = previous;
		}
	}
}
