using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Mühle
{
	public struct Move
	{

		public static Move NullMove = new Move(-1,-1,-1);
		public int start { get; set; }
		public int target { get; set; }
		public int captured {  get; set; }

		public Move(int from, int to ,int capture)
		{
			start = from;
			target = to;
			captured = capture;
		}

		public override string ToString()
		{

			return $"{start} {target} {captured}";
			
		}

		
	}
}
