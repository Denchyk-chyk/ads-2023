namespace Lab5Logics
{
	public class Line
	{
		private Tree<int, char> _tree;

		public Tree<int, char> Create(string text)
		{
			var items = new (int key, char value)[text.Length];

			for (int i = 0; i < items.Length; i++)
				items[i] = (i, text[i]);

			_tree = Tree<int, char>.Instant(items);

			return _tree;
		}

		public string Delete(out Tree<int, char> tree)
		{
			Tree<char, int> matches = new();
			Check(_tree, matches);
			string letters = string.Empty;
			Delete(matches, ref letters);
			tree = _tree;
			return letters;
		}
		
		private void Delete(Tree<char, int> current, ref string letters)
		{
			if (!current.IsEmpty)
			{
				if (current.Value > 1)
				{
					letters += $"{current.Key} ";
					Delete(_tree, current.Key, current.Value);
				}

				Delete(current.Left, ref letters);
				Delete(current.Right, ref letters);
			}
		}

		private static int Delete(Tree<int, char> current, char value, int counter)
		{
			while (counter > 0 && !current.IsEmpty && current.Value == value)
			{
				Tree<int, char>.Delete(current);
				counter--;
			}

			if (counter > 0 && !current.IsEmpty) counter = Delete(current.Right, value, Delete(current.Left, value, counter));
			return counter;
		}

		private static void Check(Tree<int, char> current, Tree<char, int> matches)
		{
			if (!current.IsEmpty)
			{
				Tree<char, int>.Insert(matches, current.Value, matches[current.Value]);
				matches[current.Value]++;

				Check(current.Left, matches);
				Check(current.Right, matches);
			}
		}
	}
}