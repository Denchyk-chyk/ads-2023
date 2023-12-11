using System.Text.RegularExpressions;

namespace Lab_5_Logics
{
	public partial class Numbers
	{
		private Tree<int, int> _tree = new();

		public Tree<int, int> Create(string text)
		{
			MatchCollection numbers = Integers().Matches(text);

			if (numbers.Count > 0)
			{
				(int, int)[] elements = new (int, int)[numbers.Count];

				for (int i = 0; i < numbers.Count; i++)
				{
					int number = int.Parse(numbers[i].Value);
					elements[i] = (number, number);
				}

				return _tree = Tree<int, int>.Instant(elements);
			}

			return _tree = new();
		}

		[GeneratedRegex(@"-?\d+")]
		private static partial Regex Integers();

		public bool Find(int number, out string value)
		{
			bool success = Tree<int, int>.Find(number, _tree, out Tree<int, int> tree);
			value = string.Empty;

			if (success) value = $"Батьківський: [{tree.Root}] Лівий: [{tree.Left}] Правий: [{tree.Right}]";
			return success;
		}

		public bool Max(out int value)
		{
			value = int.MinValue;

			if (!_tree.IsEmpty)
			{
				GetMax(_tree, ref value);
				return true;
			}

			return false;
		}

		private static int GetMax(Tree<int, int> tree, ref int max)
		{
			if (!tree.IsEmpty)
			{
				if (tree.Key > max) max = tree.Key;
				GetMax(tree.Left, ref max);
				GetMax(tree.Right, ref max);
			}

			return max;
		}
	}
}