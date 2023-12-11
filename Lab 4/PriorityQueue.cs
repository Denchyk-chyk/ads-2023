namespace Lab_4
{
	//Користувацька реалізація черги з пріоритетами
	public class PriorityQueue<T> : Queue<T> where T : IComparable<T>
	{ 
		//Додавання елемента в кінець
		public override void PushBack(T value)
		{
			if (IsEmpty)
			{
				First = Last = new Node<T>(value, null, null);
			}
			else
			{
				Node<T> current = Last;
				while (current != null && current.Value.CompareTo(value) < 0) current = current.Previous;

				if (current == null)
				{
					First = First.Previous = new Node<T>(value, null, First);
				}
				else
				{
					current = current.Next = new Node<T>(value, current, current.Next);
					if (current.Next != null) current.Next.Previous = current;
					else Last = current;
				}
			}

			Length++;
		}

		//Перегляд елемента з початку
		public override T PeekFirst() => IsEmpty ? default : First.Value;

		//Видалення елемента з початку
		public override void PopFirst()
		{
			if (!IsEmpty)
			{
				First = First.Next;
				Length--;
				if (!IsEmpty) First.Previous = null;
			}
		}
	}
}