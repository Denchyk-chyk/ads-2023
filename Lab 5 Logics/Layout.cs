namespace Lab5Logics
{
	public class Layout
	{
		public static Tree<char, char> Tokens { get; }

		public static string Switch(string text)
		{
			char[] letters = text.ToCharArray();
			for (int i = 0; i < letters.Length; i++)
			{
				char swicthed = Tokens[letters[i]];
				if (swicthed == default) swicthed = letters[i];
				letters[i] = swicthed;
			}
			return new string(letters);
		}
	
		static Layout()
		{
			var english = "`~@#$%^&qwertyuiop[]asdfghjkl;'zxcvbnm,./QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?";
			var ukrainian = "'₴\"№;%:?йцукенгшщзхїфівапролджєячсмитьбю.ЙЦУКЕНГШЩЗХЇ/ФІВАПРОЛДЖЄЯЧСМИТЬБЮ,";

			(char, char)[] elements = new (char, char)[english.Length];
			for (int i = 0; i < elements.Length; i++) elements[i] = (english[i], ukrainian[i]);

			Tokens = Tree<char, char>.Instant(elements);
		}
	}
}