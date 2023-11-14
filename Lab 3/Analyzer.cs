using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab_3
{
	internal class Analyzer
    {
        private const string Brackets = "()[]"; //Константа, що містить всі типи дужок

        //Створює стек із цілими числами на основі даних з поля для вводу тексту
        public bool ReadNumbers(TextBox input, out Stack<int> numbers)
        {
            numbers = new Stack<int>(); //Ініціалізація стека з числами
            var matches = new Regex(@"-?\d+").Matches(input.Text); //Знаходження чисел через регулярні вирази

			foreach (Match match in matches) //Перелік всіх знайдених чисел
				numbers.Push(int.Parse(match.Value)); //Додавання числа у стек

			return matches.Count > 0; //Було знайдено хоч одне число
		}

		//Повертає стек із двох стеків (парні і непарні числа) на основі стеку з числами
		public bool SortNumbers(TextBox input, out Stack<int>[] sortedNumbers)
        {
            var success = ReadNumbers(input, out Stack<int> numbers); //Зчитування чисел
			sortedNumbers = new Stack<int>[2]; //Кінцевий стек

			while (numbers.Peek(out int value)) //Перерахунок всіх чисел
                sortedNumbers[value % 2].Push(value); //Додавання числа до парних або непарних чисел

			return success; //Вивід інформації про наявність чисел
        }

        //Створює стек із дужками на основі даних з поля для вводу тексту, а також перевіряє, чи таке їх поєднання можливе
        public bool CheckBrackets(TextBox input, out Stack<char> output)
        {
            output = new Stack<char>(); //Стек для виводу
			//Балнс відкриваючих та закриваючих дужок (якщо дужки правильні, то повинен в кінці бути 0)
			var count = new Dictionary<Shape, int>() { { Shape.Round, 0 }, { Shape.Square, 0 } };
			Bracket previous = default; //Поточна та попередня дужки

			for (int i = 0; i < input.Text.Length; i++) //Проходження по всім символам в рядку
            {
                if (CreateBracket(input.Text[i], out Bracket bracket)) //Перевірка на те, чи є символ дужеою
                {
                    output.Push(input.Text[i]); //Запис дужки в стек для виводу

					if (!previous.Equals(default) //Попередня дужка існує
						&& previous.Shape != bracket.Shape //Поточна й попередня дужки мають різну форму
						&& previous.Kind == Kind.Close && //Попередня дужка закриваюча
						bracket.Kind == Kind.Open) //Поточна дужка відкриваюча
						return false; //Перевірка виключень (] та [)

					count[bracket.Shape] += (int)bracket.Kind; //Додавання інформації про поточну дужку
					if (count[bracket.Shape] < 0) return false; //Перевірка наявності відкриваючої дужки при відсутності відповідної закриваючої

					previous = bracket; //Присвоєння значення попередній дужці
				}
            }

			return count[Shape.Round] == 0 && count[Shape.Square] == 0; //Переіврка на те чи кожна закриваюча дужка має відкриваючу
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
    }

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

    internal enum Kind { Open = -1, Close = 1 } //Тип дужки
}