using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{

	internal class BOT_MAIN_v3
	{

		private Move bestMoveRoot;

		public Move Think(Board board)
		{

			Move[] moves = MoveGeneration.GenerateLegalMoves(board);
			bestMoveRoot = moves[0];

			Search(board, -30000, 30000, 2, 0);


			return bestMoveRoot;

			int Search(Board board, int alpha, int beta, int depth, int ply)
			{

				if (depth == 0)
				{
					return Evaluate(board);
				}

				int best = -30000;

				Move[] moves = MoveGeneration.GenerateLegalMoves(board);

				for (int i = 0; i < moves.Length; i++)
				{


					Move move = moves[i];

					GameState stateBefore = board.state;

					board.MakeMove(move);
					board.updateBoardFromUI();

					int score = -Search(board, -beta, -alpha, depth - 1, ply + 1);

					board.state = stateBefore;
					board.UndoMove(move);

					if (score > best)
					{
						best = score;

						if (ply == 0) bestMoveRoot = move;

						// Improve alpha
						alpha = Math.Max(alpha, score);

						// Fail-high
						if (alpha >= beta) break;

					}
				}

				return best;
			}

			int Evaluate(Board board)
			{

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

				return (board.active == 0) ? score : -score;
			}


		}


	}
}
