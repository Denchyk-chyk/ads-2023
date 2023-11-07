using System.Windows;

namespace Lab_2
{
	public partial class MainWindow : Window
	{
		private Reader _reader = new Reader(); //Клас для перетворення рядка на відповідні списки
		private Writer _writer = new Writer(); //Клас для показу списку через графічний елемент ListBox 
		private FamiliesDataBase _families = new FamiliesDataBase(); //База даних з членами родин

		public MainWindow() //Коструктор за умовчуванням
		{
			InitializeComponent(); //Ініціалізація вікна (метод із батьківського класу Window)
		}

		private void IntegerButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку чисел в рядку
		{
			_writer.Write(Output, _reader.ReadIntegers(Input)); //Виводить списку чисел в рядку
		}

		private void SymbolsButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку вимволів у рядку
		{
			_writer.Write(Output, _reader.ReadSymbols(Input)); //Виводить список символів у рядку
		}

		private void AtomsButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку атомів у рядку
		{
			_writer.Write(Output, _reader.ReadAtoms(Input)); //Виводить список атомів у рядку
		}

		private void FamilyButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку сімей з двома дітьми
		{
			_writer.Write(Output, _families.FindTwoChildrenFamilies()); //Виводить список сімей з двома дітьми
		}

		private void WorkerButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку людей зі стажем менше 10 років
		{
			_writer.Write(Output, _families.FindLowExperienceEmployees()); //Виводить список прізвищ людей зі стажем меншим за 10 років
		}

		private void WorkingWifeButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку дружин, що працюють
		{
			_writer.Write(Output, _families.FindWorkingWifes()); //Виводить список дружин, що працюють
		}

		private void WifeButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку жінок з дваома дітьми
		{
			_writer.Write(Output, _families.FindTwoChildrenWomen()); //Виводить список жінок з двома дітьми
		}

		private void PersonButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для виводу списку імен всіх людей
		{
			_writer.Write(Output, _families.FindNames()); //Виводить список імен всіх людей
		}

		private void AddButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для додавання сім'ї
		{
			_families.AddFamily(Input, Output); //Додавання сім'ї до бази даних
		}

		private void SumbitButton_Click(object sender, RoutedEventArgs e) //Натискання кнопки для додавання члена родини
		{
			_families.AddPerson(Input); //Додавання члена родини
		}
	}
}