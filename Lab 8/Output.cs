namespace Lab8
{
	internal static class Output
	{
		public static void Print<T>(this T[] array) => Console.WriteLine(string.Join(" ", array));
	}
}