namespace Lab_3
{
	public class Stack<T>
	{
		private bool _isEmpty = true; //Зберігає інформацію про те чи порожній стек
		private Node<T>? _head; //Перший вузол (голова)

		//Додавання вузла
		public void Push(T? value)
		{
			_head = new Node<T>(value, _head); //Створення нвої голови
			_isEmpty = false; //Стек тепер точно не порожній
		}

		//Видалення поточного вузла
		public bool Pop()
		{
			if (!_isEmpty) //Стек не порожній
			{
				_head = _head.Next; //Видалення
				_isEmpty = _head == null; //Знаходження того, чи стек порожній
				return true; //Вузол було видалено
			}

			return _isEmpty; //Не було елементів для видалення
		}

		//Перегляд та видалення поточного вузла
		public bool Peek(out T? value)
		{
			value = _isEmpty ? _head.Value : default; //Повернення значення поточного вузла
			return Pop(); //Видалення поточного вузла
		}
	}

	//Користувацька реалізація вузлів стека на основі вузлів зв'яаних спиків
	public class Node<T>
	{
		public T Value { get; set; } //Значення
		public Node<T>? Next { get; set; } //Посилання на наступний елемент

		public Node(T value, Node<T>? next) //Конструктор
		{
			Value = value;
			Next = next;
		}
	}
}