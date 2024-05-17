using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	internal class UI
	{

		private bool runningGame;
		private Board currentBoard;

		public UI() {
			runningGame = false;
		}
		public void Start()
		{
			Console.WriteLine("Hello to this nine men's morris game.");
			Console.WriteLine("Type 'help' for more instructions.");

			Console.WriteLine();

			while (true)
			{
				if (runningGame)
				{

					if (currentBoard?.state != GameState.Finished && currentBoard?.state != GameState.Draw)
					{
						currentBoard?.Display();
						Console.Write("Input Move/Command: ");
						currentBoard?.updateBoardFromUI();
						if (false)
						{
							Move[] moves = MoveGeneration.GenerateLegalMoves(currentBoard);

							Random random = new Random();
							int randomIndex = random.Next(0, moves.Length);

							Move randomMove = moves[randomIndex];

							currentBoard?.MakeMoveFromUI(randomMove);
							Console.WriteLine(randomMove);
							//Thread.Sleep(10);
							currentBoard?.updateBoardFromUI();
							continue;
						}

					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						if (currentBoard.state == GameState.Finished)
						{
							Console.WriteLine($"{currentBoard.enemyChar} has won.");
						}else
						{
							Console.WriteLine("Draw by repitition");
						}

						Console.WriteLine("");
						runningGame = false;
						currentBoard = null;

						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Closed current Game.");
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine("");

					}
				}


				
				string? input = Console.ReadLine();
				
				if (input == "help" || input == "-help")
				{

					PrintHelp();
					continue;

				}else if(input == "new"){

					if (runningGame)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Can not start another game. Type 'close' first.");
						Console.ForegroundColor = ConsoleColor.White;
						continue;
					}
					else
					{
						NewGame();
						continue;
					}

				}else if(input == "close"){

					if (!runningGame)
					{

						Console.WriteLine();
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("No current Game running.  Type 'new' for a new Game .");
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine();
						continue;
					}
					else
					{
						runningGame = false;
						currentBoard = null;
						Console.WriteLine();
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Closed current Game.");
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine();
						continue;
					}

				}
				else if (input == "cls")
				{
					Console.Clear();
					continue;
				}
				else if (input == "test")
				{
					SpeedTest.MoveGenTest(100000);
					continue;
				}
				else if (input == "perft")
				{
					SpeedTest.PerftTest();
					continue;
				}
				else if (input == "help -moves")
				{

					PrintHelpMoves();
					continue;

				}




				if (runningGame)
				{

					
					string[] parts = input.Trim().Split(' ');

					string input1;
					string input2;
					string input3;

					switch (parts.Length)
					{

						case 0:
							PrintWrongMoveInut();
							break;
						case 1:

							input1 = parts[0];

							try
							{
								Move move = new Move(-1,int.Parse(input1),-1);

								currentBoard?.MakeMoveFromUI(move);

								

							}
							catch 
							{
								PrintWrongMoveInut();
							}

							break;

						case 2:


							input1 = parts[0];
							input2 = parts[1];

							try
							{

								Move move;
								if (currentBoard?.state == GameState.StartingPhase)
								{
									move = new Move(-1, int.Parse(input1), int.Parse(input2));
								}
								else
								{
									move = new Move(int.Parse(input1), int.Parse(input2), -1);
								}
								

								currentBoard?.MakeMoveFromUI(move);

							}
							catch
							{
								PrintWrongMoveInut();
							}

							break;

						case 3:

							input1 = parts[0];
							input2 = parts[1];
							input3 = parts[2];

							try
							{

								Move move;
								if (currentBoard?.state == GameState.StartingPhase)
								{
									throw new Exception("");
								}
								else
								{
									move = new Move(int.Parse(input1), int.Parse(input2), int.Parse(input3));
								}


								currentBoard?.MakeMoveFromUI(move);

							}
							catch
							{
								PrintWrongMoveInut();
							}

							break;

						default:

							PrintWrongMoveInut();
							break;





					}
					currentBoard?.updateBoardFromUI();

				}
				
				else
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Command not found. Type 'help' for more information.");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine();
				}
				

			}

		}

		private void PrintWrongMoveInut()
		{
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine("No valid Input, type 'help -moves' for more information.");
			Console.ForegroundColor = ConsoleColor.White;
		}
		private void PrintHelp()
		{

			Console.WriteLine();

			Console.WriteLine("+===================================================================+\n" +
							  "|                     NINE MAN'S MORRIS - HELP                      |\n" +
							  "+===================================================================+\n" +
							  "| Commands:                                                         |\n" +
							  "|   help  - Displays this help message.                             |\n" +
							  "|   new   - Starts a new game.                                      |\n" +
							  "|   close - Ends the current game.                                  |\n" +
							  "|   cls   - Clears the console screen.                              |\n" +
							  "|                                                                   |\n" +
							  "| Game Instructions:                                                |\n" +
							  "|   The game board consists of 24 fields connected by lines.        |\n" +
							  "|   Each player has 9 pieces.                                       |\n" +
							  "|   The objective is to place three own pieces in a row.            |\n" +
							  "|   Once achieved, a opponent's piece can be removed.               |\n" +
							  "|                                                                   |\n" +
							  "+===================================================================+"
			);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Press any Key to continue.");
			Console.ForegroundColor = ConsoleColor.White;
			Console.ReadKey(true);

			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, currentLineCursor);
			Console.WriteLine();
		}

		private void PrintHelpMoves()
		{
			
			Console.WriteLine(
				"+=============================================================================+\n" +
				"|                           Moves - HELP                                      |\n" +
				"+=============================================================================+\n" +
				"|  Moves Format:                                                              |\n" +
				"|                                                                             |\n" +
				"|      Placement Phase:                                                       |\n" +
				"|          Single Placemeent:        [Position]                               |\n" +
				"|          Placement with Capture:   [Position] [CapturePosition]             |\n" +
				"|                                                                             |\n" +
				"|      Mid-/EndGame Phase:                                                    |\n" +
				"|          Single Move:        [FromPosition] [ToPosition]                    |\n" +
				"|          Move with Capture:  [FromPosition] [ToPosition] [CapturePosition]  |\n" +
				"|                                                                             |\n" +
				"|  Position Format:                                                           |\n" +
				"|      Format: [Number]                                                       |\n" +
				"|      Example: '10' -> Place stone on tile 10                                |\n" +
				"|                                                                             |\n" +
				"+=============================================================================+"
			);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Press any Key to continue.");
			Console.ForegroundColor = ConsoleColor.White;
			Console.ReadKey(true);

			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, currentLineCursor);
			Console.WriteLine();

		}

		private void NewGame()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Started new Game");
			Console.ForegroundColor = ConsoleColor.White;

			runningGame = true;
			currentBoard = new Board();


		}

	}
}
