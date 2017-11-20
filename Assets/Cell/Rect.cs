using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Rect : IEquatable<Rect> {
		public Coord min;
		public Coord max;

		public Rect() {
			min = Coord.zero;
			max = Coord.zero;
		}

		public Rect(int minX, int minY, int maxX, int maxY) {
			min = new Coord(minX, minY);
			max = new Coord(maxX, maxY);
		}

		public void Fit(Bounds2 bounds, double scale) {
			min = new Coord(
				(int) Math.Floor(bounds.min.x / scale),
				(int) Math.Floor(bounds.min.y / scale)
			);
			max = new Coord(
				(int) Math.Ceiling(bounds.max.x / scale),
				(int) Math.Ceiling(bounds.max.y / scale)
			);
		}

		public static bool operator ==(Rect a, Rect b) {
			return a.Equals(b);
		}

		public static bool operator !=(Rect a, Rect b) {
			return a.min != b.min || a.max != b.max;
		}

		public Coord GetSize() {
			return new Coord(max.x + 1 - min.x, max.y + 1 - min.y);
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			else
				return Equals((Rect) obj);
		}

		public bool Equals(Rect other) {
			return min == other.min && max == other.max;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(min)
				.HashValue(max);
		}

		public override string ToString() {
			return string.Format("Rect({0}, {1}, {2}, {3})", min.x, min.y, max.x, max.y);
		}

	}
	
}
