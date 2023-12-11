using Lab_5_Logics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lab_5
{
	public partial class MainWindow : Window
	{
		private Line _line = new();
		private Numbers _numbers = new();
		private Document _document = new();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Display<K, V>(Tree<K, V> tree) where K : IComparable<K>
		{
			Output.Items.Clear();
			Output.Items.Add(Create(tree));

			foreach (var item in Output.Items)
				if (item is TreeViewItem current) Open(current);
		}

		private static TreeViewItem Create<K, V>(Tree<K, V> tree) where K : IComparable<K>
		{
            TreeViewItem node = new() { Header = tree };

            if (!tree.Left.IsEmpty) node.Items.Add(Create(tree.Left));
            if (!tree.Right.IsEmpty) node.Items.Add(Create(tree.Right));

			return node;
		}

		private static void Open(TreeViewItem item)
		{
			item.IsExpanded = true;

			foreach (var childItem in item.Items)
				if (childItem is TreeViewItem current) Open(current);
		}

		private void Numbers_Click(object sender, RoutedEventArgs e)
		{
			Display(_numbers.Create(Input.Text));
		}

		private void FindNumbers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (_numbers.Find(int.Parse(Input.Text), out string info)) Info.Text = $"{info}";
				else Info.Text = "Вершина відсутня";
			} catch { Info.Text = "Некоректні дані"; }
		}

		private void FindMax_Click(object sender, RoutedEventArgs e)
		{
			if (_numbers.Max(out int max)) Info.Text = $"Найбільше число: {max}";
			else Info.Text = "Спочатку створіть дерево";
        }

		private void Letters_Click(object sender, RoutedEventArgs e)
		{
			Display(_line.Create(Input.Text));
		}

		private void Dublicate_Click(object sender, RoutedEventArgs e)
		{
			Info.Text = $"Символи, що повторюються: {_line.Delete(out Tree<int, char> tree)}";
			Display(tree);
		}

		private void Words_Click(object sender, RoutedEventArgs e)
		{
			if (File.Exists(Input.Text)) Display(_document.Read(Input.Text));
			else Info.Text = "Неправильний шлях до файла";
        }

		private void LetterWords_Click(object sender, RoutedEventArgs e)
		{
			if (Input.Text.Length == 1 && char.IsLetter(Input.Text[0]))
			{
				Info.Text = $"Видалено слів - {_document.Delete(Input.Text[0], out Tree<string, char> tree)}";
				Display(tree);
			}
			else Info.Text = "Некоректні дані";
		}

		private void Layout_Click(object sender, RoutedEventArgs e)
		{
			Display(Lab_5_Logics.Layout.Tokens);
			Info.Text = Lab_5_Logics.Layout.Switch(Input.Text);
        }
    }
}