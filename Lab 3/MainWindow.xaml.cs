using System.Windows;

namespace Lab_3
{
	public partial class MainWindow : Window
	{
		private Analyzer _analyzer = new(); //Клас для аналізу тексту за допомогою стеків

		public MainWindow()
		{
			InitializeComponent(); //Конструктор за умовчуванням
		}

		private void NumberskButton_Click(object sender, RoutedEventArgs e)
		{
			string header = _analyzer.ReadNumbers(Input, out Stack<int> numbers) ? "Числа:" : "Рядок не містить чисел"; //Зчитування чисел з графічного елемента TextBox
			Writer<int>.GetInstance().Write(Output, numbers, Info, header); //Вивід чисел та фнормації про їх наявність чи відсутність
		}

		//Метод для виводу висел певної парності
		private void SelectNumbers(Number type, string header)
		{
			header = _analyzer.SelectNumbers(Input, out Stack<int> numbers, type) ? $"{header} числа:" : "Рядок не містить відповідних чисел"; //Зчитування чисел з графічного елемента TextBox
			Writer<int>.GetInstance().Write(Output, numbers, Info, header); //Вивід чисел та фнормації про їх наявність чи відсутність
		}

		private void EvenButton_Click(object sender, RoutedEventArgs e)
		{
			SelectNumbers(Number.Even, "Парні"); //Вивід парних чисел
		}

		private void OddButton_Click(object sender, RoutedEventArgs e)
		{
			SelectNumbers(Number.Odd, "Непарні"); //Вивід непарних чисел
		}

		private void BracketsButton_Click(object sender, RoutedEventArgs e)
		{
			string header = _analyzer.CheckBrackets(Input, out Stack<char> brackets, out int analyzedCount) ? "Правильні" : $"Неправильні (проаналізовано дужок - {analyzedCount})"; //Перевірка дужок зчитаних з графічного елемента TextBox
			Writer<char>.GetInstance().Write(Output, brackets, Info, header); //Вивід дужок та результату перевірки
        }
	}
}