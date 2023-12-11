namespace Lab_5_Logics
{
	public class Tree<K, V> : IComparable<Tree<K, V>>, IComparable<K> where K : IComparable<K>
	{
		public bool IsEmpty { get; private set; }
		public Child Status { get; private set; }

		public K Key { get; private set; }
		public V? Value;

		public Tree<K, V> Root { get; private set; }
		
		public Tree<K, V> Left
		{
			get => _left;
			set
			{
				_left = value;
				SetChild(value, Child.Left);
			}
		}

		public Tree<K, V> Right
		{
			get => _right;
			set
			{
				_right = value;
				SetChild(value, Child.Right);
			}
		}

		private Tree<K, V> _left;
		private Tree<K, V> _right;

		public V? this[K key]
		{
			get
			{
				Tree<K, V>.Find(key, this, out Tree<K,V>? value);
				return value.Value;
			}
			set
			{
				Tree<K, V>.Find(key, this, out Tree<K, V>? tree);
				tree.Value = value;
			}
		}

		public Tree()
		{
			IsEmpty = true;
			Root = _right = _left = this;
		}

		public Tree(K key, V value, Tree<K, V> left, Tree<K, V> right)
		{
			Instant(this, key, value, left, right);
		}

		private static void Instant(Tree<K,V> tree, K key, V value, Tree<K, V> left, Tree<K, V> right)
		{
			tree.IsEmpty = false;
			tree.Key = key;
			tree.Value = value;
			tree.Left = left;
			tree.Right = right;
		}

		private void SetChild(Tree<K, V> child, Child status)
		{
			child.Root = this;
			child.Status = status;
		}

		public static Tree<K,V> Instant(params (K key, V value)[] items)
		{
			Array.Sort(items);
			return Instant(items, 0, items.Length);
		}

		private static Tree<K,V> Instant((K key, V value)[] items, int first, int last)
		{
			if (last <= first) return new();
			int middle = first + (last - first) / 2;
			return new(items[middle].key, items[middle].value, Instant(items, first, middle), Instant(items, middle + 1, last));
		}

		public static bool Find(K key, Tree<K, V> tree, out Tree<K, V> value)
		{
			value = new();
			if (tree.IsEmpty) return false;

			if (tree.Equals(key))
			{
				value = tree;
				return true;
			}

			return Tree<K, V>.Find(key, tree.Left, out value) || Tree<K, V>.Find(key, tree.Right, out value);
		}

		public static void Insert(Tree<K, V> tree, K key, V value)
		{
			if (tree.IsEmpty)
			{
				Instant(tree, key, value, new(), new());
			}
			else if (tree.CompareTo(key) == 0)
			{
				tree.Value = value;
			}
			else if (tree.CompareTo(key) > 0)
			{
				Tree<K, V>.Insert(tree.Left, key, value);
			}
			else Tree<K, V>.Insert(tree.Right, key, value);
		}

		public static void Delete(Tree<K, V> tree)
		{
			if (tree.Left.IsEmpty && tree.Right.IsEmpty)
			{
				Replace(tree, new());
			}
			else if (!tree.Left.IsEmpty && !tree.Right.IsEmpty)
			{
				Tree<K, V> next = tree.Right;
				while (!next.Left.IsEmpty) next = next.Left;
				Replace(tree, next);
			}
			else Replace(tree, tree.Left.IsEmpty ? tree.Right : tree.Left);
		}

		private static void Replace(Tree<K, V> current, Tree<K, V> next)
		{
			if (!(current.IsEmpty = next.IsEmpty))
			{
				current.Key = next.Key;
				current.Value = next.Value;
			}

			if (next.Left.IsEmpty)
			{
				if (next.Status == Child.Left) next.Root.Left = next.Right;
				else if (next.Status == Child.Right) next.Root.Right = next.Right;
			}
			else
			{
				current.Left = next.Left;
				current.Right = next.Right;
			}

			next.Left = next.Right = next.Root = new();
		}
		
		public int CompareTo(K? other) => Key.CompareTo(other);
		public int CompareTo(Tree<K, V>? other) => Key.CompareTo(other.Key);
		
		public override bool Equals(object? obj)
		{
			if (obj is Tree<K, V> tree) return CompareTo(tree) == 0;
			if (obj is K key) return CompareTo(key) == 0;
			return false;
		}

		public override string ToString()
		{
			string status = "▮ ";
			if (Status == Child.Left) status = "◤ ";
			else if (Status == Child.Right) status = "◣ ";

			return status + (Key.Equals(Value) ? $"{Key}" : $"{Key} → {Value}");
		}
	}

	public enum Child
	{
		None, Left, Right
	}
}