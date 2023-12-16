using System.Text.RegularExpressions;

namespace Lab5Logics
{
	public partial class Document
	{
		private Tree<string, char> _tree = new();

		public Tree<string, char> Read(string file)
		{
			_tree = new();
			using StreamReader reader = new(file);
			string line;
			List<int> counter = [];
			
			while ((line = reader.ReadLine()) != null)
			{
				counter.Add(0);
				MatchCollection matches = Words().Matches(line);
				foreach (Match match in matches)
				{
					counter[^1]++;
					Tree<string, char>.Insert(_tree, match.Value.ToLower(), char.ToLower(match.Value[0]));
				}
			}

			return _tree;
		}

		public int Delete(char letter, out Tree<string, char> tree)
		{
			tree = _tree;
			return Delete(_tree, char.ToLower(letter));
		}

		public static int Delete(Tree<string, char> tree, char letter)
		{
			if (tree.IsEmpty) return 0;
			int count = 0;

			while (!tree.IsEmpty && tree.Value == letter)
			{
				Tree<string, char>.Delete(tree);
				count++;
			}

			count += Delete(tree.Left, letter) + Delete(tree.Right, letter);

			return count;
		}

		[GeneratedRegex(@"\w+")]
		private static partial Regex Words();
	}
}