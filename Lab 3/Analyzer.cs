using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab3
{
	internal partial class Analyzer
    {
        private const string Brackets = "()[]"; //Константа, що містить всі типи дужок

        //Створює стек із цілими числами на основі даних з поля для вводу тексту
        public bool ReadNumbers(TextBox input, out Stack<int> numbers)
        {
            numbers = new Stack<int>(); //Ініціалізація стека з числами
            var matches = Numbers().Matches(input.Text); //Знаходження чисел через регулярні вирази

			foreach (Match match in matches) //Перелік всіх знайдених чисел
				numbers.Push(int.Parse(match.Value)); //Додавання числа у стек

			return !numbers.IsEmpty; //Було знайдено хоч одне число
		}

		//Повертає стек із двох стеків (парні і непарні числа) на основі стеку з числами
		public bool SelectNumbers(TextBox input, out Stack<int> numbers, Number type)
        {
			numbers = new Stack<int>(); //Ініціалізація стека з числами
			var matches = Numbers().Matches(input.Text); //Знаходження чисел через регулярні вирази

			foreach (Match match in matches) //Перелік всіх знайдених чисел
			{
				int number = int.Parse(match.Value); //Збереження числа

				if (number % 2 == (int)type) //Перевірка на парність
					numbers.Push(number); //Додавання числа до стека
			}

			return !numbers.IsEmpty; //Хоба б одне число підходить
        }

        //Створює стек із дужками на основі даних з поля для вводу тексту, а також перевіряє, чи таке їх поєднання можливе
        public bool CheckBrackets(TextBox input, out Stack<char> output, out int analyzedCount)
        {
			analyzedCount = 0; //Вказує на те через скільки елементів стало відомо, що дужки неправильні
			output = new Stack<char>(); //Стек для виводу
			var count = new int[] { 0, 0 }; //Балнс відкриваючих та закриваючих дужок (якщо дужки правильні, то повинен в кінці бути 0)
			Bracket previous = default; //Поточна та попередня дужки
			bool isIncorrect = false; //Дужки правильні

			for (int i = 0; i < input.Text.Length; i++) //Проходження по всім символам в рядку
            {
                if (CreateBracket(input.Text[i], out Bracket bracket)) //Перевірка на те, чи є символ дужеою
                {
                    output.Push(input.Text[i]); //Запис дужки в стек для виводу
					if (isIncorrect) continue; //Якщо відмо, що дужки неправильні, то відбувається лише запис у стек

					analyzedCount++; //Збільшення показника лічильника проаналізованих дужок
					count[(int)bracket.Shape] += (int)bracket.Kind; //Додавання інформації про поточну дужку

					if (isIncorrect = 
						count[(int)bracket.Shape] < 0 || //Наявність відкриваючої дужки при відсутності відповідної закриваючої
						(!previous.Equals(default) //Попередня дужка існує
						&& previous.Shape != bracket.Shape //Поточна й попередня дужки мають різну форму
						&& previous.Kind == Kind.Close && //Попередня дужка закриваюча
						bracket.Kind == Kind.Open)) //Поточна дужка відкриваюча
						continue; //Відомо, що дужки неправильні, то ж ітерацію завершено

					previous = bracket; //Присвоєння значення попередній дужці
				}
            }

			return !isIncorrect && count[(int)Shape.Round] == 0 && count[(int)Shape.Square] == 0; //Переіврка на те чи кожна закриваюча дужка має відкриваючу
		}

		//Метод для створення дужки із символа
		private bool CreateBracket(char token, out Bracket bracket)
        {
            int index = Brackets.IndexOf(token); //Тип дужки

            if (index != -1) //Символ є дужкою
            {
				bracket = new Bracket(index < 2, index % 2 == 0); //Створення дужки
                return true; //Дужка створено
			}

			bracket = default; //Порожня дужка
			return false; //Некоректний символ
        }

		[GeneratedRegex("-?\\d+")]
		private static partial Regex Numbers(); //Збередений регулярний вираз для зчитування чисел
	}

	internal enum Number { Even, Odd } //Парне та непарне число

    internal struct Bracket //Дужка
    {
        public Shape Shape { get; private set; } //Кругла чи квадратна
        public Kind Kind { get; private set; } //Закриваюча чи відкриваюча

        public Bracket(bool round, bool opening) //Коструктор
        {
			Shape = round ? Shape.Round : Shape.Square;
			Kind = opening ? Kind.Open : Kind.Close;
		}
    }


    internal enum Shape { Round, Square } //Форма дужки

    internal enum Kind { Open = 1, Close = -1 } //Тип дужки
}