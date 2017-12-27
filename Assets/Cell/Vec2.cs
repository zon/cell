using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Vec2 : IEquatable<Vec2> {
		public readonly double x;
		public readonly double y;

		public double magnitude { get {
			return Math.Sqrt(x * x + y * y);
		} }

		public double sqrMagnitude { get {
			return x * x + y * y;
		} }

		public double this[int index] {
			get {
				switch (index) {
					case 0:
						return x;
					case 1:
						return y;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}

		public static Vec2 operator +(Vec2 a, Vec2 b) {
			return new Vec2(a.x + b.x, a.y + b.y);
		}

		public static Vec2 operator -(Vec2 a, Vec2 b) {
			return new Vec2(a.x - b.x, a.y - b.y);
		}

		public static Vec2 operator *(Vec2 v, double d) {
			return new Vec2(v.x * d, v.y * d);
		}

		public static Vec2 operator *(double d, Vec2 v) {
			return new Vec2(d * v.x, d * v.y);
		}

		public static Vec2 operator *(Vec2 a, Vec2 b) {
			return new Vec2(a.x * b.x, a.y * b.y);
		}

		public static Vec2 operator /(Vec2 v, double d) {
			return new Vec2(v.x / d, v.y / d);
		}

		public static Vec2 operator /(double d, Vec2 v) {
			return new Vec2(d / v.x, d / v.y);
		}

		public static Vec2 operator /(Vec2 a, Vec2 b) {
			return new Vec2(a.x / b.x, a.y / b.y);
		}

		public static bool operator ==(Vec2 a, Vec2 b) {
			return a.Equals(b);
		}

		public static bool operator !=(Vec2 a, Vec2 b) {
			return a.x != b.x || a.y != b.y;
		}

		public Vec2(double x, double y) {
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			else
				return Equals((Vec2) obj);
		}

		public bool Equals(Vec2 other) {
			return other.x == x && other.y == y;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(x)
				.HashValue(y);
		}

		public override string ToString() {
			return string.Format("Vec2({0}, {1})", x, y);
		}

		public Vec2 Normalized() {
			var m = magnitude;
			return new Vec2(x / m, y / m);
		}

		public double Dot(Vec2 other) {
			return x * other.x + y * other.y;
		}

		public Vec2 Min(Vec2 other) {
			return new Vec2 (
				x < other.x ? x : other.x,
				y < other.y ? y : other.y
			);
		}

		public Vec2 Max(Vec2 other) {
			return new Vec2 (
				x > other.x ? x : other.x,
				y > other.y ? y : other.y
			);
		}

		public Vec2 Clamp(double magnitude) {
			if (sqrMagnitude > magnitude * magnitude)
				return Normalized() * magnitude;
			else
				return new Vec2(x, y);
		}

		public double RadiansTo(Vec2 other) {
			return Math.Acos(Dot(other));
		}

		public double Radians() {
			return Math.Atan2(y, x);
		}

		public Vec2 Floor() {
			return new Vec2(Math.Floor(x), Math.Floor(y));
		}

		public Vec2 Perpendicular() {
			return new Vec2 (y, -x);
		}

		public Vec2 CounterPerpendicular() {
			return new Vec2 (-y, x);
		}

		public static Vec2 right = new Vec2(1, 0);
		public static Vec2 left = new Vec2(-1, 0);
		public static Vec2 up = new Vec2(0, 1);
		public static Vec2 down = new Vec2(0, -1);
		public static Vec2 zero = new Vec2(0, 0);
		public static Vec2 one = new Vec2(1, 1);
		public static Vec2 positiveInfinity = Vec2.one * double.PositiveInfinity;
		public static Vec2 negativeInfinity = Vec2.one * double.NegativeInfinity;

		public const double deg2rad = (Math.PI * 2) / 360;
		public const double rad2deg = 360 / (Math.PI * 2);

		public static Vec2 FromRadians(double radians) {
			return new Vec2(Math.Cos(radians), Math.Sin(radians));
		}

	}

}
