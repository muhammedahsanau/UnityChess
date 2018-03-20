﻿using System.Text;

namespace UnityChess {
	/// <summary>
	///     Representation of a move, namely a piece and its end square.
	/// </summary>
	public class Movement {

		/// <summary>
		///     Creates a new Movement.
		/// </summary>
		/// <param name="end">Square which the piece will land on.</param>
		/// <param name="piece">Piece being moved.</param>
		public Movement(Square end, Piece piece) {
			End = end;
			Piece = piece;
		}

		//Used to improve readability
		internal Movement(int file, int rank, Piece piece) {
			End = new Square(file, rank);
			Piece = piece;
		}

		/// <summary>
		///     Copy constructor.
		/// </summary>
		internal Movement(Movement move) {
			End = new Square(move.End);
			Piece = move.Piece;
		}

		public Square End { get; set; }
		public Piece Piece { get; set; }

		/// <summary>
		///     Checks whether a move is legal on a given board/turn.
		/// </summary>
		/// <param name="turn">Side of the player whose turn it currently is.</param>
		public bool IsLegal(Side turn) {
			if (Piece.Side != turn) return false;

			return Piece.ValidMoves.Contains(this);

			// TODO method may be wrong if .Contains uses ref equality. If so, need to use .Exists w/ lambda exp to check if a move with the fields of the passed move exists in the list
		}

		// override object.Equals
		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			Movement move = obj as Movement;
			// ReSharper disable once PossibleNullReferenceException
			return End.Equals(move.End) && Piece.Equals(move.Piece);
		}

		// override object.GetHashCode
		public override int GetHashCode() {
			int hash = 13;
			hash = hash * 7 + End.GetHashCode();
			hash = hash * 7 + Piece.GetHashCode();
			return hash;
		}

		public override string ToString() {
			StringBuilder s = new StringBuilder();

			s.AppendLine(Piece.ToString());
			s.AppendLine(string.Format("\tFrom: {0}", Piece.Position));
			s.AppendFormat("\tTo: {0}", End);
			return s.ToString();
		}
	}
}