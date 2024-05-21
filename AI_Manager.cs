using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	internal class AI_Manager
	{

		public static void playRandomMoveGenGames(int count, string bot1, string bot2)
		{

			Assembly assembly = Assembly.GetExecutingAssembly();  //LoadFrom("C:/Users/Laurenz Haase/Meine_Projekte/VisualStudio/Mühle/bin/Debug/net7.0/Mühle.dll");

			Type type1 = assembly.GetType("Mühle.BOT_MAIN_"+bot1);
			Type type2 = assembly.GetType("Mühle.BOT_MAIN_"+bot2);

			if (type1 == null)
			{
				Console.WriteLine($"{bot1} not found");
				return;
			}
			if (type2 == null)
			{
				Console.WriteLine($"{bot2} not found");
				return;
			}

			object instance1 = Activator.CreateInstance(type1);
			object instance2 = Activator.CreateInstance(type2);


			MethodInfo method1 = type1.GetMethod("Think");
			MethodInfo method2 = type2.GetMethod("Think");


			int draws = 0;
			int wins_X = 0;
			int wins_O = 0;




			for (int i = 0; i < count; i++)
			{

				Console.WriteLine($"Starting game {i + 1}");
				Board board = new Board();

				bool display = false;

				while (board.state != GameState.Finished && board.state != GameState.Draw)
				{

					board.updateBoardFromUI();

					if (MoveGeneration.GenerateLegalMoves(board).Length == 0)
					{
						throw new Exception("");
					}

					Board boardBefore = (Board)board.Clone();

					if(display) board.Display();

					object[] parameters = new object[] { board };
					Move move = Move.NullMove;

					if (board.active == 0)
					{

						object returnValue = method1.Invoke(instance1, parameters);

						if (returnValue != null)
						{
							move = (Move)returnValue;
							if (display)  Console.WriteLine(move);
						}
						else
						{
							Console.WriteLine("Error exec THINK method");
						}

					}
					else
					{

						object returnValue = method2.Invoke(instance2, parameters);

						if (returnValue != null)
						{
							move = (Move)returnValue;
							if (display)  Console.WriteLine(move);
						}
						else
						{
							Console.WriteLine("Error exec THINK method");
						}

						

					}

					board.MakeMoveFromUI(move);
					board.updateBoardFromUI();

					if (board.JsonCompare(boardBefore))
					{
						Console.WriteLine("BOARD SHOULD CHANGED");

					}

					if (display) Thread.Sleep(1000);

				}

				if(board.state == GameState.Finished)
				{
					if (board.active == 1){
						wins_X++;
					}
					else
					{
						wins_O++;
					}
				}
				else
				{
						draws++;
				}

				Console.WriteLine($"X Wins: {wins_X} | O Wins: {wins_O} | Draws: {draws}");

			}


			double percX = (double)wins_X / count;
			double percO = (double)wins_O / count;
			double percDraw = (double)draws / count;

			Console.WriteLine();
			Console.WriteLine($"X Wins: {wins_X} | O Wins: {wins_O} | Draws: {draws}");
			Console.WriteLine($"{percX} | {percO} | {percDraw}");
			Console.WriteLine();


		}

	}
}
