namespace Lab_2
{
	//Користувацька реалізація зв'язаних списків
	public class LinkedList
    {
		public int Count { get; private set; } //Кількість елементів списку
		
		private Node? _head; //Посилання на голову списку
		private Node? _current; //Посилання на поточний вузол

		public void MoveToHead() => _current = _head; //Переміщує вказівник на поточний вузол до голови

		//Переміщує вказівник на поточний вузол до наступного
		public bool MoveToNext()
		{
			if (_current == null || _current.Next == null) //Поточний елемент, або наступний відсутній
			{
				return false; //Повідомлення про невдале виконання переміщення
			}
			else
			{
				_current = _current.Next; //Перехід до наступного елемента
				return true; //Повідомлення про успішне виконання переміщення
			}
		}

		//Додає вузол з відповідним значенням
		public void Add(object value)
		{
			_head = new Node(value, _head); //Створення новго головного вузла
			MoveToHead(); //Переміщення вказівника до голови
			Count++; //Збільшення розміру списку
		}

		//Додає елемент, з відповідним значенням, якщо такого ще немає у списку
		public void AddIfNo(object value)
        {
            if (_head != null) //Голова наявна
            {
				MoveToHead(); //Переміщення голови

                do
                {
                    if (_current.Value.Equals(value)) //Вузол з таким значенням вже існує
                        return; //Заверешення операції
					 
                    _current = _current.Next; //Переміщення до наступного елемента
                }
                while (MoveToNext());
			}

			Add(value); //Додавання вузла
		}
		
        public object Peek() => _current == null ? null : _current.Value; //Повертає значення поточного вузла

		//Змінює значення поточного вузла
		public void Edit(object value)
		{
			if (_current != null) //Поточний вузол існує
				_current.Value = value; //Зміна значення
		}

		//Видаляє всі вузли
		public void Clear()
        {
            _head = null; //Видалення голови
            MoveToHead(); //Переміщення вказівника на null-значення
        }

		//Конструктор, що створює список із певними елементами, а не порожній
		public LinkedList(params object[] values)
		{
			foreach (var value in values) //Перепрахунок цих елементів
				Add(value); //Їх додавання у список
		}
	}

	//Користувацька реалізація вузлів зв'язаних списків
	public class Node
    {
        public object Value { get; set; } //Значення
        public Node? Next { get; set; } //Посилання на наступний елемент

		public Node(object value, Node? next) //Конструктор, що задає ці 2 значення
		{
			Value = value;
			Next = next;
		}
	}
}