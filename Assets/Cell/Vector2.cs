using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Vector2 {
		public readonly double x;
		public readonly double y;

		public double magnitude { get {
			return Math.Sqrt(x * x + y * y);
		} }

		public double sqrMagnitude { get {
			return x * x + y * y;
		} }

		public static Vector2 operator +(Vector2 a, Vector2 b) {
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b) {
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator *(Vector2 v, double d) {
			return new Vector2(v.x * d, v.y * d);
		}

		public static Vector2 operator /(Vector2 v, double d) {
			return new Vector2(v.x / d, v.y / d);
		}

		public Vector2(double x, double y) {
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			var other = (Vector2) obj;
			return other.x == x && other.y == y;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(x)
				.HashValue(y);
		}

		public override string ToString() {
			return string.Format("Vector2({0}, {1})", x, y);
		}

		public Vector2 Normalized() {
			var m = magnitude;
			return new Vector2(x / m, y / m);
		}

		public double Dot(Vector2 other) {
			return x * other.x + y * other.y;
		}

		public Vector2 Min(Vector2 other) {
			return new Vector2 (
				x < other.x ? x : other.x,
				y < other.y ? y : other.y
			);
		}

		public Vector2 Max(Vector2 other) {
			return new Vector2 (
				x > other.x ? x : other.x,
				y > other.y ? y : other.y
			);
		}

		public double Radians(Vector2 other) {
			return Math.Acos(Dot(other));
		}

		public Vector2 Floor() {
			return new Vector2(Math.Floor(x), Math.Floor(y));
		}

		public Vector2 Perpendicular() {
			return new Vector2 (y, -x);
		}

		public Vector2 CounterPerpendicular() {
			return new Vector2 (-y, x);
		}

		public static Vector2 right = new Vector2(1, 0);
		public static Vector2 left = new Vector2(-1, 0);
		public static Vector2 up = new Vector2(0, 1);
		public static Vector2 down = new Vector2(0, -1);
		public static Vector2 zero = new Vector2(0, 0);
		public static Vector2 one = new Vector2(1, 1);
		public static Vector2 positiveInfinity = new Vector2(double.PositiveInfinity, double.PositiveInfinity);
		public static Vector2 negativeInfinity = new Vector2(double.NegativeInfinity, double.NegativeInfinity);

		public const double deg2rad = (Math.PI * 2) / 360;

	}

}
