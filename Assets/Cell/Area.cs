using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Area : IEquatable<Area> {
		public Coord min;
		public Coord max;

		public Area() {
			min = Coord.zero;
			max = Coord.zero;
		}

		public Area(int minX, int minY, int maxX, int maxY) {
			min = new Coord(minX, minY);
			max = new Coord(maxX, maxY);
		}

		public void Fit(Bounds2 bounds, double scale) {
			min = new Coord(
				(int) Math.Floor(bounds.min.x / scale),
				(int) Math.Floor(bounds.min.y / scale)
			);
			max = new Coord(
				(int) Math.Floor(bounds.max.x / scale),
				(int) Math.Floor(bounds.max.y / scale)
			);
		}

		public static bool operator ==(Area a, Area b) {
			return a.Equals(b);
		}

		public static bool operator !=(Area a, Area b) {
			return a.min != b.min || a.max != b.max;
		}

		public Coord GetSize() {
			return new Coord(max.x + 1 - min.x, max.y + 1 - min.y);
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			else
				return Equals((Area) obj);
		}

		public bool Equals(Area other) {
			return min == other.min && max == other.max;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(min)
				.HashValue(max);
		}

		public Bounds2 ToBounds2() {
			return Bounds2.MinMax(min.ToVec2(), max.ToVec2() + Vec2.one);
		}

		public override string ToString() {
			return string.Format("Rect({0}, {1}, {2}, {3})", min.x, min.y, max.x, max.y);
		}

	}
	
}
