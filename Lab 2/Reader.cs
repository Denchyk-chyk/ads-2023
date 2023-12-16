using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab2
{
	internal class Reader
	{
		//Записує всі числа з рядка в список
		public LinkedList ReadIntegers(TextBox input)
		{
			LinkedList numbers = new LinkedList(); //Створення порожнього списку
			MatchCollection matches = new Regex(@"-?\d+").Matches(input.Text); //Знаходження всіх чисел в полі для вводу тексту через регулярний вираз

			foreach (Match match in matches) //Перерахунок всіх чисел, знайдених раніше
				numbers.Add(int.Parse(match.Value)); //Додавання їх до спсику

			//На випадок порожнього рядка
			if (matches.Count == 0) //Перевірка на порожність списку
				numbers.Add("Чисел не виявлено"); //Вивід повідомлення у список

			return numbers; //Повернення списку
		}

		//Записує всі символи з рядка в список
		public LinkedList ReadSymbols(TextBox input)
		{
			LinkedList symbols = new LinkedList(); //Створення порожнього списку

			//На випадок порожнього рядка
			if (input.Text.Length == 0) //Перевірка на порожність рядка
			{
				symbols.Add("Рядок порожній"); //Вивід повідомлення у список
				return symbols; //Повернення списку
			}

			for (int i = 0; i < input.Text.Length; i++) //Перерахунок всіз символів у рядку
				symbols.Add(input.Text[i]); //Додавання їх до списку

			return symbols; //Повернення списку
		}

		//Записує всі атоми з рядка в список
		public LinkedList ReadAtoms(TextBox input)
		{
			LinkedList atoms = new LinkedList(); //Створення порожнього списку
			MatchCollection matches = new Regex(@"\b[\w\d\p{P}]+\b").Matches(input.Text); //Знаходження всіх атомів у полі для вводу тексту через регулярний вираз
			
			foreach (Match match in matches) //Перерахунок всіх знайдених атомів
			{
				//LinkedList atom = new LinkedList();
				//atom.Add(match.Value);
				//list.Add(atom);
				atoms.Add(match.Value); //Додавання атому до списку
			}

			//На випадок порожнього рядка
			if (matches.Count == 0) //Перевірка на порожність списку
				atoms.Add("Атомів не виявлено"); //Вивід повідомлення у список

			return atoms; //Повернення списку
		}
	}
}