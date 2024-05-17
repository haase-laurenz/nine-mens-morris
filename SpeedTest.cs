using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	internal class SpeedTest
	{

		public static void MoveGenTest(int iterations)
		{
			Board TestBoard = new Board();
			TimeSpan avg = TimeSpan.Zero;

			for (int i = 0; i < 10; i++)
			{
				DateTime startTime = DateTime.Now;

				for(int j = 0; j < iterations; j++)
				{
					MoveGeneration.GenerateLegalMoves(TestBoard);
				}

				DateTime endTime = DateTime.Now;
				TimeSpan timeDifference = endTime - startTime;
				avg += timeDifference / 10;
				Console.WriteLine($"Finished test in {timeDifference.TotalMilliseconds} ms.");
			}
			Console.WriteLine();
			Console.WriteLine($"Finished test with average {avg.TotalMilliseconds} ms.");
			Console.WriteLine();
		}

		public static void PerftTest()
		{
			Board testBoard;

			for (int i = 1;i <= 5;i++)
			{
				testBoard = new Board();
				Console.WriteLine($"Starting Depth {i}.");
				DateTime startTime = DateTime.Now;
				int res = Perft(i, testBoard);
				DateTime endTime = DateTime.Now;
				TimeSpan timeDifference = endTime - startTime;

				Console.WriteLine($"{res} Nodes in Depth {i} | {timeDifference.TotalMilliseconds} ms.");
				Console.WriteLine($"Finished Depth {i}.");
				Console.WriteLine();
			}
		

		}

		private static int Perft(int depth, Board board)
		{

			if (depth == 0) return 1;

			int count = 0;

			Move[] moves = MoveGeneration.GenerateLegalMoves(board);

            foreach (Move move in moves)
            {
				board.MakeMove(move);
				board.updateBoardFromUI();
				count+=Perft(depth - 1, board);
				board.UndoMove(move);
				board.updateBoardFromUI();

			}

			return count;
        }
	}
}
