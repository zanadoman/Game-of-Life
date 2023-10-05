using System;
using System.Threading;

namespace sejtautomata
{
	internal unsafe class Program
	{
		static Random rnd = new Random();
		static void Main(string[] args)
		{
			byte[,] livingspace = new byte[20, 100];
			generateLivingspace(livingspace);

			//kezdő állapot mutatása 1,5mp-ig
			Console.Clear();
			for (int x = 0; x < 20; x++)
			{
				for (int y = 0; y < 100; y++)
				{
					if (livingspace[x, y] == 1)
					{
						Console.BackgroundColor = ConsoleColor.Red;
						Console.Write("X");
					}
					else
					{
						Console.BackgroundColor = ConsoleColor.White;
						Console.Write(" ");
					}
				}
				Console.WriteLine();
			}
			Thread.Sleep(1500);

			simulateLivingspace(livingspace);
		}
		static void generateLivingspace(byte[,] livingspace)
		{
			for (int x = 0; x < 20; x++)
			{
				for (int y = 0; y < 100; y++)
				{
					if (rnd.Next(0, 11) == 0) //itt lehet beállítani a kezdeti sejt sűrűséget (jelenleg 1:10-hez esély)
					{
						livingspace[x, y] = 1;
					}
					else
					{
						livingspace[x, y] = 0;
					}
				}
			}
		}
		static void simulateLivingspace(byte[,] livingspace)
		{
			bool life = true;

			while(life) //addig fut amíg van élő sejt
			{
				life = false;
				Console.SetCursorPosition(0, 0); //kurzor alaphelyzetbe állítása az új képkocka rajzolásához

				for (int x = 0; x < 20; x++)
				{
					for (int y = 0; y < 100; y++)
					{
						simulateCellFate(livingspace, x, y);

						//cella kitöltése az állapotától függően
						if (livingspace[x, y] == 1)
						{
							life = true;
							Console.BackgroundColor = ConsoleColor.Red;
							Console.Write("X");
						}
						else
						{
							Console.BackgroundColor = ConsoleColor.White;
							Console.Write(" ");
						}
					}
					Console.WriteLine();
				}

				Thread.Sleep(500);
			}

			Console.ResetColor();
			Console.WriteLine("Nem maradt élő sejt.");
			Console.ReadKey();
		}
		static void simulateCellFate(byte[,] livingspace, int x, int y)
		{
			byte neighborsCount = getNeighborsCount(livingspace, x, y);

			//sors számítása, a szomszédok számának megfelelően
			if (neighborsCount == 3)
			{
				livingspace[x, y] = 1;
			}
			else if (neighborsCount > 3)
			{
				livingspace[x, y] = 0;
			}
			else if (neighborsCount < 2)
			{
				livingspace[x, y] = 0;
			}
		}
		static byte getNeighborsCount(byte[,] livingspace, int x, int y)
		{
			//szomszédok számának feldolgozása
			byte[] neighbors = getNeighbors(livingspace, x, y);
			byte neighborsCount = 0;

			foreach (byte i in neighbors)
			{
				if (i == 1)
				{
					neighborsCount++;
				}
			}

			return neighborsCount;
		}
		static byte[] getNeighbors(byte[,] livingspace, int x, int y)
		{
			byte[] neighbors = new byte[8];

			//szomszédok feltérképezése, index túlcsordulások kezelésével
			try
			{
				neighbors[0] = livingspace[x - 1, y - 1];
			}
			catch
			{
				neighbors[0] = 0;
			}
			try
			{
				neighbors[1] = livingspace[x - 1, y];
			}
			catch
			{
				neighbors[1] = 0;
			}
			try
			{
				neighbors[2] = livingspace[x - 1, y + 1];
			}
			catch
			{
				neighbors[2] = 0;
			}
			try
			{
				neighbors[3] = livingspace[x, y - 1];
			}
			catch
			{
				neighbors[3] = 0;
			}
			try
			{
				neighbors[4] = livingspace[x, y + 1];
			}
			catch
			{
				neighbors[4] = 0;
			}
			try
			{
				neighbors[5] = livingspace[x + 1, y - 1];
			}
			catch
			{
				neighbors[5] = 0;
			}
			try
			{
				neighbors[6] = livingspace[x + 1, y];
			}
			catch
			{
				neighbors[6] = 0;
			}
			try
			{
				neighbors[7] = livingspace[x + 1, y + 1];
			}
			catch
			{
				neighbors[7] = 0;
			}

			return neighbors;
		}
	}
}
