using System.Windows;

namespace Lab_4
{
	public partial class MainWindow : Window
	{
		private Solution _solution { get; } = new(); //Клас, в якому здіснюються всі операції зі списками

		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _solution.Add(Input); //Додавання в список тексту з поля для вводу тексту
		}

		private void RemoveValueButton_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _solution.Remove(Input); //Вивід списку в графічний елемент ListBox
		}

		private void RemoveThirdButton_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _solution.RemoveThird(); //Видалення елемента зі списку за значенням, введеним з текстового поля та повідомлення про успішність операції
		}

		private void DisplayButton_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = "Список:"; //Заголовок
			Output.Items.Clear(); //Очищення ListBox
			_solution.Queue.DoForEach(value => Output.Items.Add(value)); //Проходження всіма елементами списку та додавання їх у ListBox
		}

		private void CleanButton_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _solution.Clean(); //Очищення списку
		}
    }
}