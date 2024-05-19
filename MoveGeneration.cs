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

			foreach (int[] mill in Board.MILLS)
			{
				bool millFound = true;

				for (int i = 0; i < 3; i++)
				{
					int field = mill[i];

					if (board.board[field] != board.enemyChar)
					{
						millFound = false;
						break; // Keine Mühle, zum nächsten Mill gehen
					}


				}
				// Wenn eine Mühle gefunden wurde, gib true zurück
				if (millFound)
				{
					enemyPieces.Remove(mill[0]);
					enemyPieces.Remove(mill[1]);
					enemyPieces.Remove(mill[2]);
				}

			}





			if (board.state == GameState.StartingPhase)
			{

				foreach(int field in emptyFields) { 

					board.board[field] = board.activeChar;

					board.UpdateCanCapture(field);

					if (board.activeCanCapture)
					{
						foreach(int enemyPiece in enemyPieces)
						{
							moves.Add(new Move(-1, field, enemyPiece));
						}
					}
					else
					{
						moves.Add(new Move(-1, field, -1));
					}
					// Hinzufügen eines Zugs zum Platzieren eines Steins an dieser Position
							

					board.board[field] = ' ';
					board.activeCanCapture = false;

				}


			}
			else
			{

				if (board.state == GameState.MidGame)
				{

						
					foreach (int field in activePieces) { 

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

										board.UpdateCanCapture(neighbour);

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




			}


			return moves.ToArray();

		}
	}
}
