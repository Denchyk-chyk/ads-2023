using System.Windows.Controls;

namespace Lab2
{
	internal class Writer
	{
		//Виводить всі елементи списку в графічний елемент ListBox
		public void Write(ListBox box, LinkedList list, bool clean = true)
		{
			if (clean) //Очищувати поле для виводу
				box.Items.Clear(); //Очищення
			Display(box, list); //Рекурсивний вивід
		}

		//Підметод, що потрібен для реалізації рекурсії
		private void Display(ListBox box, LinkedList list)
		{
			list.MoveToHead(); //Переведення вказівника на попточний елеемкнт у списку до його голови

			do 
			{
				object value = list.Peek(); //Збереження поточного вузла списку

				if (value is LinkedList) //Перевірка на те, чи містить цей вущол інший список
					Display(box, (LinkedList)value); //Вивід цього списку
				else
					box.Items.Add(value); //Вивід значення вузла
			}
			while (list.MoveToNext()); //Перехід до наступного вузла(метод повертає false, якщо такого вузла не існує)
		}
	}
}