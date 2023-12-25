using System.Text.RegularExpressions;

namespace Lab8
{
	internal partial class Input
	{
		//Метод для отримання послідовності цілих чисел введеної користувачем
		public static int[] Sequence()
		{
			Console.WriteLine("Введіть послідовність цілих чисел");
			var input = Console.ReadLine();
			MatchCollection numbers = Numbers().Matches(input);

			//Перевірка, чи введено команду для генерації випадкової послідовності
			if (input[0] == ':')
			{
				int[] random = new int[int.Parse(numbers[0].Value)];

				//Заповнення масиву випадковими числами в заданому діапазоні
				for (int i = 0; i < random.Length; i++)
					random[i] = new Random().Next(int.Parse(numbers[1].Value), int.Parse(numbers[2].Value));

				return random; //Повертає згенеровану послідовність
			}

			//Повертає масив цілих чисел, якщо користувач ввід послідовність
			return numbers.Cast<Match>().Select(match => int.Parse(match.Value)).ToArray();
		}

		//Частковий клас, що містить метод регулярного виразу для знаходження цілих чисел
		[GeneratedRegex(@"-?\d+")]
		private static partial Regex Numbers();
	}
}