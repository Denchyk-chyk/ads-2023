namespace Lab_5_Logics
{
	internal class Printer()
	{
		public static void Print<K, V>(Tree<K, V> tree, string indent = "", int level = 0) where K : IComparable<K>
		{
			if (!tree.IsEmpty)
			{
				Console.WriteLine($"{indent}{tree}");

				Print(tree.Left, Increased(indent, tree), level);
				Print(tree.Right, Increased(indent, tree), level);
			}
		}

		private static string Increased(string indent, object output) => $"{indent}{new(' ', output.ToString().Length + 2)}";
	}
}