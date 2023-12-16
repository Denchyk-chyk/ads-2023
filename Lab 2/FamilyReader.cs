using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab2
{
	internal class FamilyReader //Клас, призначений для додавання нових сімей у базу даних
	{
		private TextBox _input; //Поле для вводу
		private ListBox _output; //Поле для виводу
		private Writer _writer { get; } = new Writer(); //Об'єкт для виводу повідомлень

		private Person _wife; //Дружина
		private Person _husband; //Чоловік
		private Person[] _children; //Діти
		
		int pointer = - 2; //Вказівник на члена родини

		//Отримання родини
		public Family GetFamily()
		{
			Family family = new Family(_wife, _husband); //Створення сім'ї
			family.AddChildren(_children); //Додавання до неї дітей
			ShowMessage(true, family.ToString(), "Додавання членів сім'ї завершено"); //Вивід повідомлення
			return family; //Повернення сім'ї
		}

		//Додавання члена сім'ї
		public bool AddPerson()
		{
			if (ReadPerson(out Person person)) //Pxbnedf
			{
				if (pointer == -2) //Початкова позиція вказівника
				{
					_wife = person; //Додавання інформації про дружину
					ShowMessage(false, "Введіть інформацію про чоловіка", _wife.GetInfo()); //Вивід повідомлення
				}
				else if (pointer == -1) //Позиція після вводу інформації про дружину
				{
					_husband = person; //Додавання інформації про чоловіка
					ShowMessage(false, "Введіть інформацію про дітей", _husband.GetInfo()); //Вивід повідомлення
				}
				else //Позиції після вводу інформації про чоловіка
				{
					_children[pointer] = person; //Додавання інформації про дитину
					ShowMessage(false, _children[pointer].GetInfo()); //Вивід повідомлення

					if (pointer == _children.Length - 1) //
						return true; //Повідомлення про завершення додаваня інформації про членів родини
				}

				pointer++; //Переміщення вказівника
			}
			else
			{
				ShowMessage(false, "Некоректні дані, спробуйте ще раз"); //Вивід повідомлення про помилку
			}

			return false; //Повідомлення про те, що додаванні нформаціїх про членів родини ще не заврешено
		}

		//Зчитування даних з рядка про члена сім'ї за шаблоном Ім'я Прізвище рік місяць день працює професія стаж
		private bool ReadPerson(out Person person)
		{
			MatchCollection matches = new Regex(@"\b[\w\d\p{P}]+\b").Matches(_input.Text); //Зчитвання інформації з тексту

			try
			{
				person = new Person( //Повернення людини
					matches[0].Value, matches[1].Value, //Ім'я та прізвище
					matches[5].Value == "є" ? new Work(int.Parse(matches[7].Value), matches[6].Value) : new Work(), //Робота
					int.Parse(matches[2].Value), int.Parse(matches[3].Value), int.Parse(matches[4].Value)); //Вік
				return true; //Успішне виконання
			}
			catch
			{
				person = new Person(); //Повернення порожньої людини в разі помилки
				return false; //Невдале виконання 
			}
		}

		//Вивід повідомлення в ListBox
		private void ShowMessage(bool clean, params string[] lines)
		{
			_writer.Write(_output, new LinkedList(lines), clean); //Вивід повідомлення з використанням класу Writer (у зворотному порядку)
		}

		//Конструктор, що приймає кількітсть дітей, а також поля для вводу та виводу
		public FamilyReader(int childrensCount, TextBox input, ListBox output)
		{
			_children = new Person[childrensCount];
			_input = input;
			_output = output;

			//Вивід початкового повідомлення
			ShowMessage(true, 
				"Інформація про дружину", 
				"Наприклад: Іван Петренко 18 7 1993 є Інженер 14", 
				"Ім'я Прізвище День народження Місяць Рік Наявність роботи (є/нема) Професія Стаж", 
				"Введіть інформацію про членів сім'ї у форматі");
		}
	}
}