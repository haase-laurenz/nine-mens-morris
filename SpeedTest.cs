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

			DateTime startTime = DateTime.Now;

			for(int i = 0; i < iterations; i++)
			{
				MoveGeneration.GenerateLegalMoves(TestBoard);
			}

			DateTime endTime = DateTime.Now;
			TimeSpan timeDifference = endTime - startTime;

			Console.WriteLine($"Finished test in {timeDifference.TotalMilliseconds} ms.");
			Console.WriteLine();
		}

	}
}
