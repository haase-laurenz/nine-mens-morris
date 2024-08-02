using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{

	internal class BOT_MAIN_v9
	{

		private Move bestMoveRoot;
		private int[] fieldTable = new int[]
		{
			
		};


		public Move Think(Board board)
		{

			Move[] moves = MoveGeneration.GenerateLegalMoves(board);
			bestMoveRoot = moves[0];

			Search(board, -30000, 30000, 6, 0);

			return bestMoveRoot;


			int Search(Board board, int alpha, int beta, int depth, int ply)
			{

				if (board.state == GameState.Draw) return 0;
				if (board.state == GameState.Finished) return -1000;

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
						score+=10;
					}
					else if (board.board[i] == 'O')
					{
						score-=10;
					}

				}

				foreach (int[] mill in Board.MILLS)
				{
					int xCount = 0;
					int oCount = 0;
					int emptyCount = 0;

					foreach (int field in mill)
					{
						if (board.board[field] == 'X')
						{
							xCount++;
						}
						else if (board.board[field] == 'O') // Leeres Feld
						{
							oCount++;
						}
						else if (board.board[field] == ' ') // Leeres Feld
						{
							emptyCount++;
						}
					}

					if (emptyCount == 3) continue;

					if(emptyCount == 2 && xCount == 1)
					{
						score++;
					}else if (emptyCount == 2 && oCount == 1)
					{
						score--;
					}else if(emptyCount == 1 && xCount == 2)
					{
						score+=5;
					}
					else if (emptyCount == 1 && oCount == 2)
					{
						score -= 5;
					}

				}

				return (board.active == 0) ? score : -score;

			}


		}

	}
}
