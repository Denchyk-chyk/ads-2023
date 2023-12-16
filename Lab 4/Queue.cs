using System.Collections;

namespace Lab4
{
	//Абстрактний клас, що є основою, як двосторонньої черги, так і черги з пріоритетом
	public abstract class Queue<T> : IEnumerable<T>
	{
		public int Length //Довжина черги
		{
			get => _length;

			set
			{
				if (value != _length) IsEmpty = value == 0;
				_length = value;
			}
		}

		private int _length = 0;

		public bool IsEmpty { get; private set; } = true; //Черга порожня

		protected Node<T>? Last = null; //Останній елемент
		protected Node<T>? First = null; //Перший елемент

		public abstract void PushBack(T value); //Додавання елемента в кінець
		public abstract T PeekFirst(); //Перегляд першого елемента
		public abstract void PopFirst(); //Видалення першого елемента

		//Очищення черги
		public bool Clean()
		{
			if (!IsEmpty)
			{
				First = Last = null;
				Length = 0;
				return true;
			}

			return false;
		}

		//Реалізація інтерфейсу IEnumerable, необхідна для обходу черги через foreach
		public IEnumerator<T> GetEnumerator() => new QueueEnumertor<T>(First);
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		//Вивід черги, як рядка
		public override string ToString()
		{
			string elemets = $"[ {{{Length}}} | ";
			foreach (var item in this) elemets += $"{item} ";
            return $"{elemets}]";
		}
	}

	//Користувацька реалізація вузла черги
	public class Node<T>(T value, Node<T> previous, Node<T> next)
	{
		public T Value = value; //Значення
		public Node<T> Next = next; //Посилання на наступний вузол
		public Node<T> Previous = previous; //Посилання на попередній вузол
	}

	//Об'єкт потрібний для ітерації черги через foreach
	internal class QueueEnumertor<T> : IEnumerator<T>
	{
		public T Current { get => _current.Value; } //Значення поточного вузла

		private Node<T> _current; //Поточний вузол
		private Node<T> _zero { get; } //Порожній вузол, що посилається на перший вузол черги

		object IEnumerator.Current => Current;

		//Переміщення до наступного вузла
		public bool MoveNext()
		{
			_current = _current.Next;
			return _current != null;
		}

		//Повернення на початок
		public void Reset()
		{
			_current = _zero;
		}

		public void Dispose() { }

		public QueueEnumertor(Node<T> first)
		{
			_current = _zero = new Node<T>(default, null, first);
		}
	}
}