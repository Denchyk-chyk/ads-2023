using System.Windows.Controls;

namespace Lab_3
{
	internal class Writer
    {
		//Виводить всі елементи стека в графічний елемент ListBox
		public void Write(ListBox box, Stack stack, string header)
		{
			box.Items.Clear();
			box.Items.Add(header);

			while (stack.Peek(out object? value))
				box.Items.Add(value);
		}

		//Виводить всі елементи стека та всі елементи стеків у ньому в графічний елемент ListBox
		public void WriteMulti(ListBox box, Stack stack, Stack headers)
		{
			box.Items.Clear();
			Write(box, stack, headers);
		}

		//Допоміжний метод для реалізації рекурсії
		private void Write(ListBox box, Stack stack, Stack headers)
		{
			headers.Peek(out object? header);
			if (header != null)
				box.Items.Add(header);

			while (stack.Peek(out object? value))
			{
				if (value is Stack)
					Write(box, value as Stack, headers);
				else
					box.Items.Add(value);
			}
		}
	}
}