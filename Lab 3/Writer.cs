using System.Windows.Controls;

namespace Lab_3
{
	internal class Writer<T>
	{
		//Реазізація патерна проєктування Singletine
		private static Writer<T> _sigletone; //Екземпляр

		public static Writer<T> GetInstance() //Метод, що заміняє конструктор
		{
			if (_sigletone == null) //Екземпляр відсутній
				_sigletone = new Writer<T>(); //Створення екземпляра

			return _sigletone; //Повернення екземпляра
		}

		private Writer() {}

		//Виводить всі елементи стека в графічний елемент ListBox
		public void Write(ListBox box, Stack<T> stack, TextBlock lable, string header)
		{
			lable.Text = header; //Вивід заголовка
			box.Items.Clear(); //Очищення елемента ListBox 

			while (stack.Peek(out T value)) //Перелік всіх елементів стека
				box.Items.Add(value); //Вивід вузла в ListBox
		}
	}
}