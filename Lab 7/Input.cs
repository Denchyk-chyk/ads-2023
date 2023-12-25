namespace Lab7
{
	internal class Input
	{
		public static int Size()
		{
			Console.WriteLine("Введіть розмір масиву:");
			return int.Parse(Console.ReadLine());
		}
	}
}