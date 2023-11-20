namespace Lab_4
{
	//Користувацька реалізація черги
	internal class Queue<T>
	{
		private int _length; //Довжина черги
		private Node<T>? _last = null; //Останній елемент
		private Node<T>? _first = null; //Перший елемент

		//Додавання елемента, якщо стек порожній
		private bool PushToEmpty(T value)
		{
			if (_length == 0)
			{
				_first = new Node<T>(value, null, null);
				_last = _first;
			}
			
			_length++;
			return _length == 1;
		}

		//Додавання елемента на початок
		public void PushForward(T value)
		{
			if (!PushToEmpty(value))
			{
				Node<T> formerFirst = _first;
				_first = new Node<T>(value, null, formerFirst);
				formerFirst.Previous = _first;
			}
		}

		//Додавання елемента в кінець
		public void PushBack(T value)
		{
			if (!PushToEmpty(value))
			{
				Node<T> formerLast = _last;
				_last = new Node<T>(value, formerLast, null);
				formerLast.Next = _last;
			}
		}

		//Перегляд та видалення елемента
		private T Remove(ref Node<T> source, Node<T> newSource, Action doIfEmpty, Action doIfNot)
		{
			if (_length == 0)
			{
				return default;
			}
			else
			{
				T value = source.Value;
				source = newSource;
				_length--;

				if (_length == 0) doIfEmpty();
				else doIfNot();

				return value;
			}
		}

		//Перегляд та видалення поточного елемента
		public T RemoveFirst() => Remove(ref _first, _first.Next, () => _last = null, () => _first.Previous = null);

		//Перегляд та видалення поточного елемента
		public T RemoveLast() => Remove(ref _last, _last.Previous, () => _first = null, () => _last.Next = null);

		//Очищення черги
		public bool Clean()
		{
            if (_length == 0)
            {
				return false;
			}
			else
			{
				_first = null;
				_last = null;
				_length = 0;
				return false;
			}
		}

		//Реалізація циклу foreach
		public void DoForEach(Action<T> action)
		{
			for (int i = _length; i > 0; i--)
			{
				T value = RemoveFirst();
				PushBack(value);
				action(value);
			}
		}

		//Видалення елемента за його значенням
		public bool Remove(T value)
		{
			bool success = false;

			DoForEach(current => 
			{ 
				if (!success && value.Equals(current))
				{
					RemoveLast();
					success = true;
				}
			});

			return success;
		}
	}

	//Користувацька реалізація вузлів зв'язаних списків (тут черги)
	internal class Node<T>(T value, Node<T> previous, Node<T> next)
	{
		public T Value { get; set; } = value; //Значення
		public Node<T> Next { get; set; } = next; //Посилання на наступний вузол
		public Node<T> Previous { get; set; } = previous; //Посилання на попередній вузол
	}
}