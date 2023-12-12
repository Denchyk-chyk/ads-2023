using System.Diagnostics;

namespace Tester
{
	public abstract class Unit
	{
		public virtual void Load() { }

		public void Test(string header, string file, params object[] list)
		{
			Process current = Process.GetCurrentProcess();
			current.ProcessorAffinity = new IntPtr(1);
			current.PriorityClass = ProcessPriorityClass.Idle;

			Load();

			var items = Execute(list);
			items[0] = ("avg", new decimal[list.Length]);
			items[1] = ("min", new decimal[list.Length]);
			items[2] = ("max", new decimal[list.Length]);

			for (int i = 0; i < items[1].time.Length; i++)
			{
				items[1].time[i] = decimal.MaxValue;
				items[2].time[i] = decimal.MinValue;
			}

			for (int i = 3; i < items.Length; i++)
			{
				for (int j = 0; j < items[i].time.Length; j++)
				{
					if (items[i].time[j] < items[1].time[j])
						items[1].time[j] = items[i].time[j];
					else if (items[i].time[j] > items[2].time[j])
						items[2].time[j] = items[i].time[j];

					items[0].time[j] += items[i].time[j];
				}
			}

			for (int i = 0; i < items[0].time.Length; i++)
				items[0].time[i] = (items[0].time[i] - items[1].time[i] - items[2].time[i]) / (items.Length - 5);

			using StreamWriter writer = new(file, true);

			writer.WriteLine(Header(header));

			writer.WriteLine(Header("Вхідні дані"));

			for (int i = 0; i < list.Length; i++)
				writer.WriteLine($"{i + 1}.".PadRight(5) + $" | {list[i]}");

			writer.WriteLine(Header("Час"));

			for (int i = 0; i < items.Length; i++)
			{
				writer.WriteLine($"{items[i].header,-5} | {string.Join("| ", items[i].time.Select(i => i.ToString().PadRight(6)[..6].PadRight(7)))}");
				if (i == 2) writer.WriteLine(Header("Детально"));
			}

			writer.WriteLine();
		}

		private static string Header(string header)
		{
			int total = 52;
			int content = total - 2;

			return header.PadLeft(content / 2 + header.Length / 2, '-').PadRight(content, '-');
		}

		private (string header, decimal[] time)[] Execute(object[] list)
		{
			List<decimal[]> time = [];
			bool[] used = new bool[list.Length];
			int[] current = new int[list.Length];

			int[] indexes = new int[list.Length];
			for (int i = 0; i < indexes.Length; i++) indexes[i] = i;

			Execute(list, indexes, used, current, 0, time);
			time.RemoveAt(0);

			var output = new (string header, decimal[] time)[time.Count + 3];

			for (int i = 0; i < time.Count; i++)
				output[i + 3] = ($"{i + 1}.", time[i]);
			
			return output;
		}

		private void Execute(object[] list, int[] indexes, bool[] used, int[] current, int index, List<decimal[]> result)
		{
			int length = indexes.Length;

			if (index == length)
			{
				result.Add(new decimal[indexes.Length]);

				for (int i = 0; i < indexes.Length; i++)
				{
					Stopwatch watch = new();
					watch.Start();
					Test(list[indexes[i]], indexes[i]);
					watch.Stop();
					result[^1][indexes[i]] = (decimal)watch.ElapsedTicks * 1000 / Stopwatch.Frequency;
				}

				return;
			}

			for (int i = 0; i < length; i++)
			{
				if (!used[i])
				{
					used[i] = true;
					current[index] = indexes[i];
					Execute(list, indexes, used, current, index + 1, result);
					used[i] = false;
				}
			}
		}
		public abstract void Test(object item, int index);
	}
}