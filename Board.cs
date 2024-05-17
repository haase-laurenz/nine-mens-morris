using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	internal class Board
{

		public static int[][] MILLS = new int[][]
		{
			new int[] {0, 1, 2},
			new int[] {3, 4, 5},
			new int[] {6, 7, 8},
			new int[] {9, 10, 11},
			new int[] {12, 13, 14},
			new int[] {15, 16, 17},
			new int[] {18, 19, 20},
			new int[] {21, 22, 23},
			new int[] {0, 9, 21},
			new int[] {3, 10, 18},
			new int[] {6, 11, 15},
			new int[] {1, 4, 7},
			new int[] {16, 19, 22},
			new int[] {8, 12, 17},
			new int[] {5, 13, 20},
			new int[] {2, 14, 23}
		};


		// Maps Indizes of the Arrays to the corresponding Nodes
		public static int[][] EDGES = new int[][]
		{
			new int[] {1, 9},      
			new int[] {0, 2, 4},   
			new int[] {1, 14},      
			new int[] {4, 10},    
			new int[] {1, 3, 5, 7},
			new int[] {4, 13},      
			new int[] {7, 11},     
			new int[] {4, 6, 8},    
			new int[] {7, 12},      
			new int[] {0, 21, 10},  
			new int[] {3, 9, 11, 18},
			new int[] {6, 10, 15},   
			new int[] {8, 13, 17},   
			new int[] {5, 12, 14, 20},
			new int[] {2, 13, 23},  
			new int[] {11, 16},     
			new int[] {15, 17, 19},  
			new int[] {12, 16},      
			new int[] {10, 19},     
			new int[] {16, 18, 20, 22},
			new int[] {13, 19},      
			new int[] {9, 22},       
			new int[] {19, 21, 23},  
			new int[] {14, 22}

		};

		public char[] board;
		public int active = 0; // 0 - WHITE, 1 --BLACK
		public char activeChar = 'X';
		public char enemyChar = 'O';
		public GameState state;
		public bool startingPhase = true;
		public List<Move> playedMoves = new List<Move>();
		public int xStonesLeft = 9;
		public int oStonesLeft = 9;
		public bool activeCanCapture = false;
		
		public Board()
		{
			// Initialisiere das Spielbrett
			board = new char[24] {
				' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' '
			};

			state = GameState.StartingPhase;

			

		}

		public void updateBoardFromUI()
		{

			int ownPieces = 0;
			int enemyPieces = 0;

			for(int i = 0; i < board.Length; i++)
			{
				if (board[i]== activeChar)
				{
					ownPieces++;
				}else if (board[i]== enemyChar)
				{
					enemyPieces++;
				}
			}
			
			if (xStonesLeft>0 || oStonesLeft>0)
			{
				state  = GameState.StartingPhase;
			}
			else if (ownPieces > 3)
			{
				state = GameState.MidGame;
			}
			else if (enemyPieces < 3 || ownPieces<3) 
			{
				state = GameState.Finished;
			}
			else
			{
				state = GameState.EndGame;
			}
		}
		public void MakeMove(Move move)
		{

			if (state == GameState.StartingPhase) 
			{

				if (move.target == -1) int.Parse("ThrowException");
				if (move.start != -1 ) int.Parse("ThrowException");
				if (board[move.target] != ' ') int.Parse("ThrowException");

				board[move.target] = activeChar;

				UpdateCanCapture(move.target);

				board[move.target] = ' ';

				if (move.captured != -1)
				{

					if (!activeCanCapture) int.Parse("ThrowException");
					if (board[move.captured] != enemyChar) int.Parse("ThrowException");

					board[move.captured] = ' ';

				}

					if (active == 0)
				{
					xStonesLeft--;
				}
				else
				{
					oStonesLeft--;
				}

				board[move.target] = activeChar;



			}

			active ^= 1;
			(activeChar, enemyChar) = (enemyChar, activeChar);
			activeCanCapture = false;





		}

		public void UndoMove(Move move)
		{

		}

		public bool IsWon()
		{

			return false;
		}

		public void UpdateCanCapture(int target)
		{
			foreach (int[] mill in MILLS)
			{
				bool millFound = true;
				// Überprüfe, ob alle Felder der Mühle vom Spieler besetzt sind
				for (int i = 0;i<3;i++)
				{
					int field = mill[i];

					if (board[field] != activeChar)
					{
						millFound = false;
						break; // Keine Mühle, zum nächsten Mill gehen
					}
				}
				// Wenn eine Mühle gefunden wurde, gib true zurück
				if (millFound)
					activeCanCapture = true;
					return;
			}
			// Keine Mühle gefunden
			activeCanCapture = false;
			return;

		}




		public void Display()
		{
			
			Console.WriteLine();
			Console.WriteLine("BOARD:\n");
			Console.WriteLine(

				"{"+ board[0] +"}--------------{"+ board[1] + "}--------------{" + board[2] + "}                     {0}--------------{1}--------------{2}\n" +
				" |                |                |                       |                |                |\n" +
				" |    {"+ board[3] + "}--------{" + board[4] + "}--------{" + board[5] + "}    |                       |    {3}--------{4}--------{5}    |\n" +
				" |     |          |          |     |                       |     |          |          |     |\n" +
				" |     |    {"+ board[6] + "}--{" + board[7] + "}--{" + board[8] + "}    |     |                       |     |    {6}--{7}--{8}    |     |\n" +
				" |     |     |         |     |     |                       |     |     |         |     |     |\n" +
				"{"+ board[9] + "}---{" + board[10] + "}---{" + board[11] + "}       {" + board[12] + "}---{" + board[13] + "}---{" + board[14] + "}                     {9}--{10}--{11}      {12}---{13}--{14}\n" +
				" |     |     |         |     |     |                       |     |     |         |     |     |\n" +
				" |     |    {"+ board[15] + "}--{" + board[16] + "}--{" + board[17] + "}    |     |                       |     |   {15}-{16}-{17}    |     |\n" +
				" |     |          |          |     |                       |     |          |          |     |\n" +
				" |    {"+ board[18] + "}--------{" + board[19] + "}--------{" + board[20] + "}    |                       |   {18}-------{19}--------{20}   |\n" +
				" |                |                |                       |                |                |\n" +
				"{"+ board[21] + "}--------------{" + board[22] + "}--------------{" + board[23] + "}                     {21}------------{22}--------------{23}\n"

			);

			Console.WriteLine($"X to place: {xStonesLeft}");
			Console.WriteLine($"O to place: {oStonesLeft}");

			if (active == 0)
			{
				Console.WriteLine("X TO PLAY");
			}
			else
			{
				Console.WriteLine("O TO PLAY");
			}


			Console.WriteLine();
			Move[] moves = MoveGeneration.GenerateLegalMoves(this);
			foreach(Move move in moves)
			{
				Console.WriteLine(move.ToString() );
			}

			Console.WriteLine(moves.Length+" Moves");

			Console.WriteLine(state);
		}


	}
}
