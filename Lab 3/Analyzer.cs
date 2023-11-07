using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab_3
{
	internal class Analyzer
    {
        private const string Brackets = "()[]";

        //Створює стек із цілими числами на основі даних з поля для вводу тексту
        public bool ReadNumbers(TextBox input, out Stack numbers)
        {
            numbers = new Stack();
            bool result = false;

            foreach (Match match in new Regex(@"-?\d+").Matches(input.Text))
            {
				numbers.Push(match.Value);
                result = true;
			}

			return result;
		}

		//Повертає стек із двох стеків (парні і непарні числа) на основі стеку з числами
		public bool SortNumbers(TextBox input, out Stack sortedNumbers)
        {
            bool result =  ReadNumbers(input, out Stack numbers);

			sortedNumbers = new Stack();
            Stack even = new Stack();
            Stack odd = new Stack();

			while (numbers.Peek(out object value))
                (int.Parse(value.ToString()) % 2 == 0 ? even : odd).Push(value);

            sortedNumbers.Push(odd);
            sortedNumbers.Push(even);
			return result;
        }

		//Створює стек із дужками на основі даних з поля для вводу тексту, а також перевіряє, чи таке їх поєднання можливе
        public bool CheckBrackets(TextBox input, out Stack brackets)
        {
			Stack stack = new Stack();
			brackets = new Stack();

			//Запис дужок в стек для аналізу та стек для виводу
			for (int i = 0; i < input.Text.Length; i++)
			{
                if (CreateBracket(input.Text[i], out Bracket bracket))
                {
                    brackets.Push(input.Text[i]);
					stack.Push(bracket);
				}
			}

			int round = 0, square = 0;
			ref int toUse = ref round;
            Bracket current, previous = default;

			while (stack.Peek(out object? value))
            {
				current = (Bracket)value;

                if (!previous.Equals(default) && previous.Round != current.Round && !previous.Opening && current.Opening) return false; //Перевірка виключень (] та [)

                //Перевірка наявності відкриваючої дужки при відсутності відповідної щакриваючої
				if (!current.Round) toUse = ref square;
				toUse += current.Opening ? -1 : 1;
                if (toUse < 0) return false;

                previous = current;
            }

			return round == 0 && square == 0; //Переіврка на те чи кожна закриваюча дужка має відкриваючу
        }

        //Допоміжний метод для створення дужок
        private bool CreateBracket(char token, out Bracket bracket)
        {
            bracket = new Bracket();
            int index = Brackets.IndexOf(token);
            bool result;

            if (result = index != -1)
                bracket.Set(index < 2, index % 2 == 0);

			return result;
        }
    }

    internal struct Bracket
    {
        public bool Round { get; private set; }
        public bool Opening{ get; private set; }

        public void Set(bool round, bool opening)
        {
			Round = round;
			Opening = opening;
		}
    }
}