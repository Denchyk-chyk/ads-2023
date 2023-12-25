namespace Lab6
{
	public static class Converter
	{
		public static sbyte[,] ToAdj(this sbyte[,] inc)
		{
			var height = inc.GetLength(0);
			var width = inc.GetLength(1);
			var adj = new sbyte[height, height];

			for (sbyte x = 0; x < width; x++)
			{
				var nodes = new (sbyte pos, sbyte value)[2];
				var hasSecond = false;

				for (sbyte y = 0; y < height; y++)
				{
					if (inc[y, x] != 0)
					{
						if (inc[y, x] == 2)
						{
							adj[y, y] = 1;
							break;
						}
						else
						{
							if (hasSecond)
							{
								nodes[0] = (y, inc[y, x]);

								if (nodes[0].value > nodes[1].value)
									(nodes[0].value, nodes[1].value) = (nodes[1].value, nodes[0].value);

								adj[nodes[1].pos, nodes[0].pos] = 1;

								if (nodes[0].value == 1)
									adj[nodes[0].pos, nodes[1].pos] = 1;
							}
							else
							{
								nodes[1] = (y, inc[y, x]);
								hasSecond = true;
							}
						}
					}
				}
			}

			return adj;
		}

		public static sbyte[,] ToInc(this sbyte[,] adj)
		{
			var size = adj.GetLength(0);
			var copy = new sbyte[size, size];

            for (sbyte i = 0; i < size; i++)
            {
				for (sbyte j = 0; j < size; j++)
					copy[i, j] = adj[i, j];
			}

			var inc = new List<sbyte[]>();

			for (var x = 0; x < size; x++)
			{
				for (var y = 0; y < size; y++)
				{
					if (copy[y, x] == 1)
					{
						inc.Add(new sbyte[size]);

						if (x == y) inc[^1][x] = 2;
						else
						{
							inc[^1][x] = 1;
						
							if (copy[x, y] == 1)
							{
								inc[^1][y] = 1;
								copy[x, y] = 0;
							}
							else inc[^1][y] = -1;
						}
					}
				}
			}

			var matrix = new sbyte[size, inc.Count];
			
			for (var x = 0; x < inc.Count; x++)
			{
				for (sbyte y = 0; y < size; y++)
					matrix[y, x] = inc[x][y];
			}

			return matrix;
		}

		public static (sbyte first, sbyte second)[] ToNodes(this sbyte[,] inc)
		{
			var height = inc.GetLength(0);
			var width = inc.GetLength(1);
			var nodes = new (sbyte first, sbyte second)[width];

			for (sbyte x = 0; x < width; x++)
			{
				var hasFirst = false;

				for (sbyte y = 0; y < height; y++)
				{
					if (inc[y, x] != 0)
					{
						if (inc[y, x] == 2) nodes[x] = (y, y);
						else if (hasFirst)
						{
							nodes[x].second = y;

							if (nodes[x].first > nodes[x].second)
								nodes[x] = (nodes[x].second, nodes[x].first);
						}
						else
						{
							nodes[x] = (y, 0);
							hasFirst = true;
						}
					}
				}
			}

			return nodes;
		}

		public static Dictionary<sbyte, sbyte[]> ToAdjList(this sbyte[,] matrix)
		{
			var size = matrix.GetLength(0);
			var list = new Dictionary<sbyte, sbyte[]>();
			var nodes = new List<sbyte>();

			for (sbyte x = 0; x < size; x++)
			{
				nodes.Clear();

				for (sbyte y = 0; y < size; y++)
				{
					if (matrix[y, x] != 0) nodes.Add(y);
				}

				if (nodes.Count > 0) list[x] = [.. nodes];
			}

			return list;
		}
	}
}