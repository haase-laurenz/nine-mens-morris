using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	public enum GameState
	{
		Waiting = 1,
		StartingPhase = 2,
		MidGame = 3,
		EndGame = 4,
		Finished = 5,
		Draw = 6,
		Cancelled = 7,
	}
}
