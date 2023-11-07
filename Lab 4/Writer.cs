using System.Windows.Controls;

namespace Lab_4
{
	internal class Writer
	{
		public void Write(ListBox output, Queue queue, string header)
		{
			output.Items.Clear();

			while (queue.Peek(out string element))
				output.Items.Add(element);

			output.Items.Add(header);
		}
	}
}