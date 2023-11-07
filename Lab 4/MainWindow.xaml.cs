using System.Windows;

namespace Lab_4
{
	public partial class MainWindow : Window
	{
		private Writer _writer = new Writer(); //Клас для виводу списків в графічний елемент ListBox
		private Solution _solution = new Solution(); //Клас, в якому здіснюються всі операції зі списками

		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			_solution.Add(Input); //Додавання в список тексту з поля для вводу тексту
		}

		private void RemoveValueButton_Click(object sender, RoutedEventArgs e)
		{
			_writer.Write(Output, _solution.Queue, _solution.Queue.IsntEmpty ? "Список:" : "Список порожній"); //Вивід списку в графічний елемент ListBox
		}

		private void RemoveThirdButton_Click(object sender, RoutedEventArgs e)
		{
			_writer.Write(Output, new Queue(), _solution.Remove(Input)); //Видалення елемента зі списку за значенням, введеним з текстового поля та повідомлення про успішність операції
		}

		private void DisplayButton_Click(object sender, RoutedEventArgs e)
		{
			_solution.RemoveThird(); //Видалення кожного третього елемента зі списку
		}
	}
}
