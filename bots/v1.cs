using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{

	internal class BOT_MAIN_v1
	{

		public Move Think(Board board)
		{

			Move[] moves = MoveGeneration.GenerateLegalMoves(board);
			Move bestMove = moves[0];
			int bestScore = int.MinValue;

			foreach (Move move in moves)
			{

				Board boardBefore = (Board)board.Clone();

				GameState stateBefore = board.state;

				board.MakeMove(move);
				board.updateBoardFromUI();

				int score = Evaluate(board);

				board.state = stateBefore;
				board.UndoMove(move);

				if (!board.JsonCompare(boardBefore))
				{
					//Console.WriteLine("NICE");
				}

				if (score > bestScore)
				{
					bestScore = score;
					bestMove = move;
				}
			}


			return bestMove;



			int Evaluate(Board board)
			{

				if (board.state == GameState.Draw) return 0;

				int score = 0;
				for (int i = 0; i < 24; i++)
				{

					if (board.board[i] == 'X')
					{
						score++;
					}
					else if (board.board[i] == 'O')
					{
						score--;
					}

				}

				return (board.active==0)? score : -score;
			}
		}

		
	}
}
