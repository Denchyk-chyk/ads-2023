using Tester;
using Lab_5_Logics;

namespace TreesTester
{
	public static class Storage
	{
		public static Dictionary<int, Line> Lines { get; } = [];
		public static Dictionary<int, Numbers> Numbers { get; } = [];
		public static Dictionary<int, Document> Documents { get; } = [];
	}

	public class NumberstUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Numbers[index] = new();
			Storage.Numbers[index].Create(item.ToString());
		}
	}

	public class SearchUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Numbers[index].Find((int)item, out _);
		}
	}

	public class MaxUnit : Unit
	{
		public override void Test(object item, int index) => Storage.Numbers[(int)item].Max(out _);
	}

	public class LayoutUnit : Unit
	{
		public override void Load() => Layout.Switch(string.Empty);
		public override void Test(object item, int index) => Layout.Switch(item.ToString());
	}

	public class LineUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Lines[index] = new();
			Storage.Lines[index].Create(item.ToString());
		}
	}

	public class DublicatesUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Lines[(int)item].Delete(out _);
		}
	}

	public class DocumentUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Documents[index] = new();
			Storage.Documents[index].Read(item.ToString());
		}
	}

	public class DeletionUnit : Unit
	{
		public override void Test(object item, int index)
		{
			Storage.Documents[index].Delete((char)item, out _);
		}
	}
}