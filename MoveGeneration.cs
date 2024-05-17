using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	internal class MoveGeneration
	{

		public static Move[] GenerateLegalMoves(Board board) {

			List<Move> moves = new List<Move>();

			List<int> enemyPieces = new List<int>();
			List<int> activePieces = new List<int>();
			List<int> emptyFields = new List<int>();
			
			for (int i = 0; i < 24; i++)
			{
				if (board.board[i] == board.enemyChar)
				{
					enemyPieces.Add(i);
				}
				else if (board.board[i] == board.activeChar)
				{
					activePieces.Add(i);
				}
				else
				{
					emptyFields.Add(i);
				}
			}

			if (board.state == GameState.StartingPhase)
			{

				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						if (board.board[i*3+j] == ' ')
						{

							board.board[i * 3 + j] = board.activeChar;

							board.UpdateCanCapture(i * 3 + j);

							if (board.activeCanCapture)
							{
								foreach(int enemyPiece in enemyPieces)
								{
									moves.Add(new Move(-1, i * 3 + j, enemyPiece));
								}
							}
							else
							{
								moves.Add(new Move(-1, i * 3 + j, -1));
							}
							// Hinzufügen eines Zugs zum Platzieren eines Steins an dieser Position
							

							board.board[i * 3 + j] = ' ';
							board.activeCanCapture = false;


						}
					}
				}

			}
			else
			{

				if (board.state == GameState.MidGame)
				{

					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							int field = i * 3 + j;
							if (board.board[field] == board.activeChar)
							{

								int[] neighbours = Board.EDGES[field];

								for (int k = 0; k < neighbours.Length; k++)
								{
									int neighbour = neighbours[k];
									if (board.board[neighbour] == ' ')
									{

										board.board[neighbour] = board.activeChar;
										board.board[field] = ' ';

										board.UpdateCanCapture(i * 3 + j);

										if (board.activeCanCapture)
										{
											foreach (int enemyPiece in enemyPieces)
											{
												moves.Add(new Move(field, neighbour, enemyPiece));
											}
										}
										else
										{
											moves.Add(new Move(field, neighbour, -1));
										}

										board.activeCanCapture = false;
										board.board[field] = board.activeChar;
										board.board[neighbour] = ' ';

									}

								}

							}

						}
					}
				}

				else if(board.state == GameState.EndGame)

				// LATE GAME
				{

					foreach(int field in activePieces)
					{

						foreach(int empty in emptyFields)
						{

							board.board[empty] = board.activeChar;
							board.board[field] = ' ';

							board.UpdateCanCapture(empty);

							if (board.activeCanCapture)
							{
								foreach (int enemyPiece in enemyPieces)
								{
									moves.Add(new Move(field, empty, enemyPiece));
								}
							}
							else
							{
								moves.Add(new Move(field, empty, -1));
							}

							board.activeCanCapture = false;
							board.board[field] = board.activeChar;
							board.board[empty] = ' ';

						}

					}


				}
				else
				{
					throw new NotImplementedException("ERROR");
				}



			}


			return moves.ToArray();

		}
	}
}
