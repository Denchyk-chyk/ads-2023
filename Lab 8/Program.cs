using Lab8;
using System.Text;
using static Lab8.Sorter;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
	//Отримання та вивід першої послідовності
	var first = Input.Sequence();
	first.Print();

	//Отримання та вивід другої послідовності
	var second = Input.Sequence();
	second.Print();

	//Вивід довжини першої послідовності
	Console.WriteLine($"Довжина: {first.Length}");

	//Масив з методами сортування та текстовим описом для кожного методу
	var sorts = new (Sort action, string text)[]
	{
		(BubbleSort, "бульбашкою"),
		(InsertionSort, "включенням"),
		(QuickSort, "швидким методом"),
		(MergeSort, "злиттям"),
		(HeapSort, "купою"),
		(CountingSort, "підрахунком")
	};

	//Сортування та вивід результату для кожного методу
	foreach ((Sort action, string text) in sorts)
	{
		action(first, out int difficulty).Print();
		Console.WriteLine($"Посортовано {text}, складність: {difficulty}");
	}

	//Здійснення бінарного пошуку та вивід результату
	new BinarySearcher(second, first).Find().Print();
}