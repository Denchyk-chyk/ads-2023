using System.Windows;

namespace Lab4
{
	public partial class MainWindow : Window
	{
		private DequeSolution _deque { get; } //Клас, в якому здіснюються всі операції зі списками
		private PriorityQueueSolution _priorityQueue { get; } //Клас, в якому здіснюються всі операції зі списками

		public MainWindow()
		{
			InitializeComponent();
			_deque = new(Input);
			_priorityQueue = new(Input);
		}

		//Метод для виводу списку
		private void Display<T>(string header, IEnumerable<T> queue)
		{
			Info.Text = header; //Заголовок
			Output.Items.Clear(); //Очищення ListBox
			foreach (var item in queue) Output.Items.Add(item); //Проходження всіма елементами списку та додавання їх у ListBox
		}

		//Метод для виводу частини людей
		private void DisplayPartOfPeople(string header, Status status) => Display(header, _priorityQueue.Queue.Where(person => person.Status == status));

		private void PushBack_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.PushForward();
		}

		private void PushForward_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.PushBack();
		}

		private void RemoveFirst_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.RemoveFirst();
		}

		private void RemoveLast_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.RemoveLast();
		}

		private void RemoveValue_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.Remove();
		}

		private void RemoveThird_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.RemoveThird();
		}

		private void CheckPalindrom_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.CheckPalindrom();
		}

		private void DisplayDequeu_Click(object sender, RoutedEventArgs e)
		{
			Display("Список:", _deque.Queue);
		}

		private void CleanDequee_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _deque.Clean();
		}

		private void AddPersonButton_Click_1(object sender, RoutedEventArgs e)
		{
			Info.Text = _priorityQueue.Add();
		}

		private void RemovePersonButton_Click_1(object sender, RoutedEventArgs e)
		{
			Info.Text = _priorityQueue.Remove();
		}

		private void DisplayHusbands_Click(object sender, RoutedEventArgs e)
		{
			DisplayPartOfPeople("Чоловіки:", Status.Husband);
		}

		private void DisplayWifes_Click(object sender, RoutedEventArgs e)
		{
			DisplayPartOfPeople("Жінки:", Status.Wife);
		}

		private void DisplayChildren_Click(object sender, RoutedEventArgs e)
		{
			DisplayPartOfPeople("Діти:", Status.Child);
		}

		private void DisplayPeople_Click(object sender, RoutedEventArgs e)
		{
			Display("Люди:", _priorityQueue.Queue);
		}

		private void CleanPeople_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = _priorityQueue.Clean();
		}
	}
}