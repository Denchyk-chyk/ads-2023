using Lab6;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
	Console.WriteLine
		($"Виберіть дію:\n" +
		$"1. Вивести на еран всі представлення заданих графів\n" +
		$"2. Вивести на екран всі пердставелння графа на основі введеної матриці суміжності\n" +
		$"3. Вивести на екран всі пердставелння графа на основі введеної матриці інцидентності");
	
	var value = byte.Parse(Console.ReadLine());
	switch (value)
	{
		case 1:
			Creator.Display();
			break;
		case 2:
			Input.AdjMatrix().AdjGraphFormat("Представлення графа:");
			break;
		case 3:
			Input.IncMatrix().IncGraphFormat("Представлення графа:");
			break;
	}
}