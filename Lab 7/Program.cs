using Lab7;
using Lab7.Lab7;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
	Console.WriteLine
		("Виберіть дію:\n" +
		"1. На головній діагоналі знайти перший нульовий та останній від’ємний елементи двовимірного масиву\n" +
		"2. Задано двовимірний масив цілих чисел A[n, n]. При обході масиву по стовпцях знайти останній додатній елемент та його координати (номер рядка та номер стовпця)");

	//Вибір дії від користувача
	var value = byte.Parse(Console.ReadLine());

	//Обробка вибору користувача
	switch (value)
	{
		case 1:
			//Генерація матриці випадкових чисел та виведення її на екран
			var numbersMatrix = Generator.Numbers(Input.Size());
			numbersMatrix.Print();

			//Пошук нульових та від'ємних елементів на головній діагоналі та їх виведення
			Output.FoundNumbers(Searcher.FindNumbers(numbersMatrix));
			break;

		case 2:
			//Генерація матриці цілих чисел та виведення її на екран
			var integersMatrix = Generator.Integers(Input.Size());
			integersMatrix.Print();

			//Пошук останнього додатнього елемента та його координат та їх виведення
			Output.LastFoundPositiveInteger(Searcher.FindLastPositiveInteger(integersMatrix));
			break;
	}
}