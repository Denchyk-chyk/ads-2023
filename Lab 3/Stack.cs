namespace Lab_3
{
	public class Stack<T>
	{
		public bool IsEmpty { get; private set; } = true; //Зберігає інформацію про те чи порожній стек
		
		private Node<T>? _head; //Перший вузол (голова)

		//Додавання вузла
		public void Push(T value)
		{
			_head = new Node<T>(value, _head); //Створення нвої голови
			IsEmpty = false; //Стек тепер точно не порожній
		}

		//Видалення поточного вузла
		public bool Pop()
		{
			if (!IsEmpty) //Стек не порожній
			{
				_head = _head.Next; //Видалення
				IsEmpty = _head == null; //Знаходження того, чи стек порожній
				return true; //Вузол було видалено
			}

			return false; //Не було елементів для видалення
		}

		//Перегляд та видалення поточного вузла
		public bool Peek(out T value)
		{
			value = IsEmpty ? default : _head.Value; //Повернення значення поточного вузла
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