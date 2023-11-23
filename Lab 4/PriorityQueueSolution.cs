using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Lab_4
{
	internal class PriorityQueueSolution(TextBox box)
    {
		public PriorityQueue<Person> Queue { get; private set; } = new PriorityQueue<Person>(); //Черга, з якою відбувається вся робота
		private TextBox _box { get; } = box; //Поле з текстом

		//Зчитування даних з рядка про члена сім'ї за шаблоном
		private bool ReadPerson(out Person person)
		{
			MatchCollection matches = new Regex(@"\b[\w\d\p{P}]+\b").Matches(_box.Text); //Зчитвання інформації з тексту

			try
			{
				person = new Person( //Повернення людини
					matches[0].Value, matches[1].Value, //Ім'я та прізвище
					(Status)int.Parse(matches[5].Value), //Статус
					matches[6].Value == "є" ? new Work(int.Parse(matches[8].Value), matches[7].Value) : new Work(), //Робота
					int.Parse(matches[2].Value), int.Parse(matches[3].Value), int.Parse(matches[4].Value)); //Вік
				return true; //Успішне виконання
			}
			catch
			{
				person = new Person(); //Повернення порожньої людини в разі помилки
				return false; //Невдале виконання 
			}
		}

		//Додавання в список людини з поля для вводу тексту
		public string Add()
		{
			if (ReadPerson(out Person person))
			{
				Queue.PushBack(person);
				return $"Додано {person}";
			}
			else
			{
				return $"Введіть дані за шаблоном: Ім'я Прізвище День народження Місяць Рік Статус(0 - дитина, 1 - дружина, 2 - чоловік) Робота(є, нема) Професія Стаж";
			}
		}

		//Видалення останньої людини зі списку
		public string Remove()
		{
			if (Queue.IsEmpty) return "Список порожній";

			Person first = Queue.PeekFirst();
			Queue.PopFirst();
			return $"Видалено {first}";
		}

		//Очищення списку
		public string Clean() => Queue.Clean() ? "Список очищено" : "Список і так порожній";
	}

	//Структура, що містить інформацію про людину
	internal struct Person(string firstName, string lastName, Status status, Work work, params int[] birthDate) : IComparable<Person>
	{
		public string LastName { get; private set; } = lastName;
		public string FirstName { get; private set; } = firstName;
		public Status Status { get; private set; } = status;
		public Work Work { get; private set; } = work;
		public Date BirthDate { get; private set; } = new Date(birthDate[0], birthDate[1], birthDate[2]);

		//Реалізація інтерфейсу
		public int CompareTo(Person other)
		{
			int result = 0;
			IComparable[,] toCompare = new IComparable[3, 2]
			{
				{ Status, other.Status },
				{ Work.Experience, other.Work.Experience },
				{ BirthDate.Age, other.BirthDate.Age }
			};

			for (int i = 0; i < 3; i++)
				if ((result = toCompare[i, 0].CompareTo(toCompare[i, 1])) != 0) break;

			return result;
		}

		public override string ToString() => $"{FirstName} {LastName} [{BirthDate.Day:D2}.{BirthDate.Month:D2}.{BirthDate.Year}] - {Work.GetInfo()}";
	}

	//Структура, що містить дані про дату
	internal struct Date(int day, int month, int year)
	{
		public int Day { get; private set; } = day;
		public int Year { get; private set; } = year;
		public int Month { get; private set; } = month;
		public int Age
		{
			get
			{
				if (_age == -1)
					_age = DateTime.Now.Year - Year + ((Month.CompareTo(DateTime.Now.Month) + Day.CompareTo(DateTime.Now.Day) > 0 ? 1 : 0));

				return _age;
			}
		}

		private int _age = -1;
	}

	//Структура, що містить інформацію про роботу людини
	internal struct Work
	{
		public bool Unemployed { get; private set; } //Непрацевлаштованість
		public int Experience { get; private set; } //Досвід
		public string Name { get; private set; } //Назва

		//Вивід інформації
		public string GetInfo() => Unemployed ? "Не працює" : $"{Name} (стаж - {Experience}рр)";

		//Конструктор для безробітних
		public Work() => Unemployed = true;

		//Конструктор для працюючих
		public Work(int experience, string name)
		{
			Experience = experience;
			Name = name;
		}
	}

	//Стастус людини
	internal enum Status { Child = 0, Wife = 1, Husband = 2 }
}