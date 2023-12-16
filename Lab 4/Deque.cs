namespace Lab4
{
	//Користувацька реалізація черги
	public class Deque<T> : Queue<T>
	{
		//Додавання елемента, якщо стек порожній
		private bool PushToEmpty(T value)
		{
			if (IsEmpty) Last = First = new Node<T>(value, null, null);
			Length++;
			return Length == 1;
		}

		//Додавання елемента на початок
		public void PushForward(T value)
		{
			if (!PushToEmpty(value))
			{
				First = new Node<T>(value, null, First);
				First.Next.Previous = First;
			}
		}

		//Додавання елемента в кінець
		public override void PushBack(T value)
		{
			if (!PushToEmpty(value))
			{
				Last = new Node<T>(value, Last, null);
				Last.Previous.Next = Last;
			}
		}

		//Перегляд першого елемента
		public override T PeekFirst() => IsEmpty ? default : First.Value;

		//Перегляд останнього елемента
		public T PeekLast() => IsEmpty ? default : Last.Value;

		//Видалення елемента
		private void Pop(ref Node<T> edge, Node<T> newEdge, ref Node<T> otherEdge, ref Node<T> nearOtherEdge)
		{
			if (!IsEmpty)
			{
				edge = newEdge;
				Length--;

				if (Length == 0) otherEdge = null;
				else nearOtherEdge = null;
			}
		}

		//Видалення першого елемента
		public override void PopFirst() => Pop(ref First, First.Next, ref Last, ref First.Previous);

		//Видалення останнього елемента
		public void PopLast() => Pop(ref Last, Last.Previous, ref First, ref Last.Next);

		//Видалення елемента за його значенням
		public bool Remove(T value)
		{
			bool success = false;

			for (int i = Length; i > 0; i--)
			{
				T current = PeekFirst();
				PopFirst();

				if (!success && value.Equals(current)) success = true;
				else PushBack(current);
			}

			return success;
		}
	}
}