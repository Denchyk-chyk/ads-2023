using System.Windows.Controls;

namespace Lab2
{
	internal class FamiliesDataBase
	{
		private FamilyReader? _reader = null; //Об'єкт, що зчитує інформацію про нову сім'ю 

		private static LinkedList Families { get; } = new LinkedList(); //Список сімей

		//Знаходить родини з двома дітьми
		public LinkedList FindTwoChildrenFamilies()
		{
			LinkedList list = new LinkedList(); //Створення порожнього списку
			Families.MoveToHead(); //Переміщення вказівника списку сімей до голови

			do
			{
				object value = Families.Peek(); //Отримання поточного елемента списку
				if (((Family)value).Children.Count == 2) //Сім'я має двох дітей
					list.Add(value); //Додавання сім'ї в новий список
			}
			while (Families.MoveToNext()); //Переміщення вказівника вписку ссімей до наступного вузла (false при його відсутності)

			return list; //Повернення нового спсику
		}

		//Знаходить всіх жінок з двома дітьми
		public LinkedList FindTwoChildrenWomen()
		{
			LinkedList list = FindTwoChildrenFamilies(); //Збереження списку сімей з довма дітьми
			list.MoveToHead(); //Переміщення вказівника у ньому до голови

			do list.Edit(((Family)list.Peek()).Wife.GetName()); //Переміщення вказівника до наступного елемента (false при його відсутності)
			while (list.MoveToNext()); //Переміщення вказівника до наступного елемента (false при його відсутності)
			
			return list; //Поврнення нового спсику
		}

		//Знаходить всіх людей (допоміжний метод)
		private LinkedList FindPeople()
		{
			LinkedList list = new LinkedList(); //Створення порожнього списку
			Families.MoveToHead(); //Переміщення вказівника списку сімей до голови

			do
			{
				Family family = (Family)Families.Peek(); //Отриманн поточної сім'є
				list.AddIfNo(family.Wife); //Додавання дружини (за відстуності в новому списку)
				list.AddIfNo(family.Husband); //Додавання чоловіка (за відстуності в новому списку)
				family.Children.MoveToHead(); //Переміщення вказівника спсику дітей до голови

				do list.AddIfNo(family.Children.Peek()); //Додавання дитини (за відстуності в новому списку)
				while (family.Children.MoveToNext()); //Переміщення вказівника списку дітей до наступного елемента (false при його відсутності)
			}
			while (Families.MoveToNext()); //Переміщення вказівника до наступного елемента (false при його відсутності)

			return list; //Повернення нового списку
		}

		//Знаходить імена всіх людей (на основі запиту про всіх людей)
		public LinkedList FindNames()
		{
			LinkedList list = FindPeople(); //Отримання спсику з усіма людьми
			list.MoveToHead();
				
			do list.Edit(((Person)list.Peek()).GetName()); //Отримання імені поточної людини та її збереження в новий список
			while (list.MoveToNext()); //Переміщення вказівника до наступного елемента (false при його відсутності)

			return list; //Повернення списку
		}

		//Знаходить всіх дружин, що працюють
		public LinkedList FindWorkingWifes()
		{
			LinkedList list = new LinkedList(); //Створення порожнього спсику
			Families.MoveToHead(); //Переміщення вказвіника спивку сіме до голови

			do
			{
				Person wife = ((Family)Families.Peek()).Wife; //Отримання дружини з поточної сім'ї

				if (!wife.Work.Unemployed) //Дружина працює
					list.Add(wife.GetName()); //Додавання імені дружини в новий список
			}
			while (Families.MoveToNext()); //Переміщення вказівника до наступного елемента (false при його відсутності)

			return list; //Повернення новго списку
		}

		//Прізвища людей, чий стаж менший за 10 років (на основі запиту про всіх людей)
		public LinkedList FindLowExperienceEmployees()
		{
			LinkedList people = FindPeople(); //Отримання списку людей
			LinkedList lastNames = new LinkedList(); //Створення порожнього списку
			
			do
			{
				Person person = (Person)people.Peek(); //Отримання поточної людини
				
				if (!person.Work.Unemployed && person.Work.Experience < 10) //Стаж людини менше 10 років
					lastNames.Add(person.LastName); //Додавання прізвища людини до нового списку
			}
			while (people.MoveToNext()); //Переміщення вказівника списаку людей до наступного елемента (false при його відсутності)

			return lastNames; //Повренення порожнього списку
		}

		//Додавання сім'ї до бази даних
		public void AddFamily(TextBox input, ListBox output)
		{
			if (int.TryParse(input.Text, out int count)) //Отримання кількості дітей в сім'ї
				_reader = new FamilyReader(count, input, output); //Створення об'єкта для зчитування даних про членів родиини
			else
				input.Text = "Введіть кількість дітей в сім'ї"; //Вивід повідомлення
		}

		//Додавання члена сім'ї
		public void AddPerson(TextBox input)
		{
			if (_reader == null) //Елемент для зчитування даних створено
			{
				input.Text = "Спочатку створіть нову сім'ю"; //Вивід повідомлення
			}
			else
			{
				if (_reader.AddPerson()) //Зчитування інформації про члена родини (true, якщо це останній)
				{
					Families.Add(_reader.GetFamily()); //Додавання нової сім'ї до бази даних
					_reader = null; //ВИдалення об'єкта для зчитування даних
				}
			}
		}

		static FamiliesDataBase() 
		{
			//Перша сім'я
			Families.Add(new Family(new Person("Іванна", "Колодніцька", new Work(14, "Еколог"), 23, 10, 1986), new Person("Юрій", "Хімій", new Work(17, "Інженер"), 12, 8, 1982)));
			((Family)Families.Peek()).AddChild(new Person("Денис", "Хімій", new Work(), 13, 6, 2005));

			//Друга сім'я
			Families.Add(new Family(new Person("Юлія", "Підгородецька", new Work(6, "Колорист"), 7, 4, 1991), new Person("Олександр", "Підгородецький", new Work(7, "Підприємець"), 12, 6, 1990)));
			((Family)Families.Peek()).AddChildren(new Person("Данило", "Підгородецький", new Work(), 31, 1, 2013), new Person("Поліна", "Підгородецька", new Work(), 17, 9, 2017));
		}
	}

	//Структура, що містить членів сім'ї
	internal struct Family
	{
		public Person Wife { get; private set; } //Дружина
		public Person Husband { get; private set; } //Чоловік
		public LinkedList Children { get; private set; } //Діти

		//Перевизначення методу, що перетворює структру на рядок
		public override string ToString()
		{
			string result = $"{Husband.GetInfo()}, {Wife.GetInfo()} | ";
			Children.MoveToHead();

			do result += ((Person)Children.Peek()).GetInfo() + ", ";
			while (Children.MoveToNext());

			return result.Substring(0, result.Length - 2);
		}

		//Додавання дітей у список (через масив)
		public void AddChildren(params Person[] children)
		{
			foreach (var child in children)
				Children.Add(child);
		}

		//Додавання дітей у список (поелементно(
		public void AddChild(Person child)
		{
			Children.Add(child);
		}

		public Family(Person wife, Person husband)
		{
			Wife = wife;
			Husband = husband;
			Children = new LinkedList();
		}
	}

	//Структура, що містить інформацію про людину
	internal struct Person
	{
		public string LastName { get; private set; } //Ім'я
		public string FirstName { get; private set; } //Прізвище
		public Work Work { get; private set; } //Робота
		public Date BirthDate { get; private set; } //Вік

		//Коротка інформація
		public string GetInfo() => $"{FirstName} {LastName} [{BirthDate.Day:D2}.{BirthDate.Month:D2}.{BirthDate.Year}] - {Work.GetInfo()}";

		//Повне ім'я
		public string GetName() => $"{FirstName} {LastName}";

		//Конструктор
		public Person(string firstName, string lastName, Work work, params int[] birthDate)
		{ 
			FirstName = firstName;
			LastName = lastName;
			Work = work;
			BirthDate = new Date(birthDate[0], birthDate[1], birthDate[2]);
		}
	}

	//Структура, що містить дані про дату
	internal struct Date
	{
		public int Day { get; private set; } //День
		public int Year { get; private set; } //Рік
		public int Month { get; private set; } //Місяць

		//Конструктор
		public Date(int day, int month, int year)
		{
			Day = day;
			Year = year;
			Month = month;
		}
	}

	//Структура, що містить інформацію про роботу людини
	internal struct Work
	{
		public bool Unemployed { get; private set; } //Непрацевлаштованість
		public int Experience { get; private set; } //Досвід
		public string Name { get; private set; } //Назва

		//Вивід інформації
		public string GetInfo()
		{
			return Unemployed ? "Не працює" : $"{Name} (стаж - {Experience}рр)";
		}

		//Конструктор для безробітних
		public Work() => Unemployed = true;

		//Конструктор для працюючих
		public Work(int experience, string name) 
		{
			Experience = experience;
			Name = name;
		}
	}
}