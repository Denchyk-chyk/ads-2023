using System.Windows;

namespace Lab_3
{
	public partial class MainWindow : Window
	{
		private Writer _writer = new Writer(); //Клас для показу стека через графічний елемент ListBox 
		private Analyzer _analyzer = new Analyzer(); //Клас для перетворення рядка на відповідні стеки

		public MainWindow()
		{
			InitializeComponent();
		}

		private void NumberskButton_Click(object sender, RoutedEventArgs e)
		{
			if (_analyzer.ReadNumbers(Input, out Stack numbers)) //Зчитування чисел з графічного елемента TextBox
				_writer.Write(Output, numbers, "Числа"); //Вивід чисел
			else
				_writer.Write(Output, new Stack(), "Рядок не містить чисел"); //Вивід повідомлення про відсутність чисел у рядку
		}

		private void EvenAndOddButton_Click(object sender, RoutedEventArgs e)
		{
			
			if (_analyzer.SortNumbers(Input, out Stack sortedNumbers)) //Зчитування та сортування за парністю чисел з графічного елемента TextBox
			{
				//Створення стека із заголовками та додавання елементів до нього
				Stack headers = new Stack();
				headers.Push("Непарні числа");
				headers.Push("Парні числа");
				headers.Push(null);
				
				_writer.WriteMulti(Output, sortedNumbers, headers); //Вивід двох стеків з парними та непарними числами зчитаних з графічного елемента TextBox
			}
			else
			{
				_writer.Write(Output, new Stack(), "Рядок не містить чисел"); //Вивід повідомлення про відсутність чисел у рядку
			}

		}

		private void BracketsButton_Click(object sender, RoutedEventArgs e)
		{
			string header = _analyzer.CheckBrackets(Input, out Stack brackets) ? "Правильні:" : "Не правильні:"; //Перевірка дужок зчитаних з графічного елемента TextBox
			_writer.Write(Output, brackets, header); //Вивід дужок та результату перевірки
        }
    }
}