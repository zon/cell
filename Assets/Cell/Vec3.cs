using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Vec3 : IEquatable<Vec3> {
		public readonly double x;
		public readonly double y;
		public readonly double z;

		public double magnitude { get {
			return Math.Sqrt(x * x + y * y + z * z);
		} }

		public double sqrMagnitude { get {
			return x * x + y * y + z * z;
		} }

		public static Vec3 operator +(Vec3 a, Vec3 b) {
			return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vec3 operator -(Vec3 a, Vec3 b) {
			return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vec3 operator *(Vec3 v, double d) {
			return new Vec3(v.x * d, v.y * d, v.z * d);
		}

		public static Vec3 operator /(Vec3 v, double d) {
			return new Vec3(v.x / d, v.y / d, v.z / d);
		}

		public static bool operator ==(Vec3 a, Vec3 b) {
			return a.Equals(b);
		}

		public static bool operator !=(Vec3 a, Vec3 b) {
			return a.x != b.x || a.y != b.y || a.z != b.z;
		}

		public Vec3(double x, double y, double z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			else
				return Equals((Vec3) obj);
		}

		public bool Equals(Vec3 other) {
			return other.x == x && other.y == y && other.z == z;
		}

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(x)
				.HashValue(y)
				.HashValue(z);
		}

		public override string ToString() {
			return string.Format("Vec3({0}, {1}, {2})", x, y, z);
		}

		public Vec3 Normalized() {
			var m = magnitude;
			return new Vec3(x / m, y / m, z / m);
		}

		public double Dot(Vec3 other) {
			return x * other.x + y * other.y + z * other.z;
		}

		public Vec3 Min(Vec3 other) {
			return new Vec3 (
				x < other.x ? x : other.x,
				y < other.y ? y : other.y,
				z < other.z ? z : other.z
			);
		}

		public Vec3 Max(Vec3 other) {
			return new Vec3 (
				x > other.x ? x : other.x,
				y > other.y ? y : other.y,
				z > other.z ? z : other.z
			);
		}

		public double Radians(Vec3 other) {
			return Math.Acos(Dot(other));
		}

		public Vec3 Floor() {
			return new Vec3(Math.Floor(x), Math.Floor(y), Math.Floor(z));
		}

		public static Vec3 right = new Vec3(1, 0, 0);
		public static Vec3 left = new Vec3(-1, 0, 0);
		public static Vec3 up = new Vec3(0, 1, 0);
		public static Vec3 down = new Vec3(0, -1, 0);
		public static Vec3 forward = new Vec3(0, 0, 1);
		public static Vec3 back = new Vec3(0, 0, -1);
		public static Vec3 zero = new Vec3(0, 0, 0);
		public static Vec3 one = new Vec3(1, 1, 1);
		public static Vec3 positiveInfinity = Vec3.one * double.PositiveInfinity;
		public static Vec3 negativeInfinity = Vec3.one * double.NegativeInfinity;

	}

}
