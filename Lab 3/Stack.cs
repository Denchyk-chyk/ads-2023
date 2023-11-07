namespace Lab_3
{
	public class Stack
	{
		private bool _isntEmpty; //Зберігає інформацію про те чи порожній стек
		private Node? _head; //Перший вузол

		//Додавання вузла
		public void Push(object? value)
		{
			_head = new Node(value, _head);
			_isntEmpty = true;
		}

		//Видалення поточного вузла
		public bool Pop()
		{
			if (_isntEmpty)
			{
				_head = _head.Next;
				_isntEmpty = _head != null;
				return true;
			}

			return false;
		}

		//Перегляд та видалення поточного вузла
		public bool Peek(out object? value)
		{
			value = _isntEmpty ? _head.Value : new object();
			return Pop();
		}
	}

	//Користувацька реалізація вузлів зв'язаних списків (тут стеків)
	public class Node
	{
		public object Value { get; set; } //Значення
		public Node? Next { get; set; } //Посилання на наступний елемент

		public Node(object value, Node? next)
		{
			Value = value;
			Next = next;
		}
	}
}