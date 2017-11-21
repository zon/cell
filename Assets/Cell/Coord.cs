using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Coord : IEquatable<Coord> {
		public readonly int x;
		public readonly int y;

		public static Coord zero = new Coord(0, 0);
		public static Coord one = new Coord(1, 1);

		public static Coord operator +(Coord a, Coord b) {
			return new Coord(a.x + b.x, a.y + b.y);
		}

		public static Coord operator -(Coord a, Coord b) {
			return new Coord(a.x - b.x, a.y - b.y);
		}

		public static Coord operator *(Coord c, int i) {
			return new Coord(c.x * i, c.y * i);
		}

		public static Coord operator /(Coord c, int i) {
			return new Coord(c.x / i, c.y / i);
		}

		public static Vec2 operator *(Coord c, double d) {
			return new Vec2(c.x * d, c.y * d);
		}

		public static bool operator ==(Coord a, Coord b) {
			return a.Equals(b);
		}

		public static bool operator !=(Coord a, Coord b) {
			return a.x != b.x || a.y != b.y;
		}

		public Coord(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			else
				return Equals((Coord) obj);
		}

		public bool Equals(Coord other) {
			return other.x == x && other.y == y;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(x)
				.HashValue(y);
		}

		public Vec2 ToVec2() {
			return new Vec2(x, y);
		}

		public override string ToString() {
			return string.Format("Coord({0}, {1})", x, y);
		}

	}

}
