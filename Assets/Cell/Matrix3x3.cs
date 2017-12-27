using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Matrix3x3 {
		public readonly double[,] elements;

		public Matrix3x3(double[,] elements) {
			if (elements.GetLength(0) != 3 || elements.GetLength(1) != 3)
				throw new ArgumentException("Elements array must be 3x3.", "elements");
			this.elements = elements;
		}

		public Matrix3x3(
			double xx, double xy, double xz,
			double yx, double yy, double yz,
			double zx, double zy, double zz
		) {
            elements = new double[,] {
				{ xx, xy, xz },
				{ yx, yy, yz },
				{ zx, zy, zz }
			};
        }
		
		public double this[int row, int column] {
			get {
				return elements [row, column];
			}
		}

		public override string ToString() {
			var s = "Matrix3v3 {\n\t";
			var width = elements.GetLength(0);
			var height = elements.GetLength(1);
			for (var y = 0; y < height; y++) {
				for (var x = 0; x < width; x++) {
					s += elements[x, y];
					if (x + 1 < width)
						s += ",\t";
					else if (y + 1 < height)
						s += ",\n\t";
				}
			}
			return s + "\n}";
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			var other = (Matrix3x3) obj;
			return this == other;
		}

		public override int GetHashCode() {
			var hash = Hash.Base;
			for (var x = 0; x < 3; x++) {
				for (var y = 0; y < 3; y++) {
					hash = hash.HashValue (this [x, y]);
				}
			}
			return hash;
		}

		public double[] ToArray() {
			var width = elements.GetLength(0);
			var height = elements.GetLength(1);
			var arr = new double[width * height];
			for (var y = 0; y < height; y++) {
				for (var x = 0; x < width; x++) {
					arr[x * width + y] = elements[x, y];
				}
			}
			return arr;
		}

		public Matrix3x3 Transpose() {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; x < 3; y++) {
					res [x, y] = this [y, x];
				}
			}
			return new Matrix3x3 (res);
		}

		public static bool operator ==(Matrix3x3 a, Matrix3x3 b) {
			var width = a.elements.GetLength(0);
			var height = a.elements.GetLength(1);
			if (b.elements.GetLength(0) != width || b.elements.GetLength(1) != height)
				return false;
			for (var x = 0; x < width; x++) {
				for (var y = 0; y < height; y++) {
					if (b.elements [x, y] != a.elements [x, y])
						return false;
				}
			}
			return true;
		}

		public static bool operator !=(Matrix3x3 a, Matrix3x3 b) {
			return !(a == b);
		}

		public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b) {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; y < 3; y++) {
					res [x, y] = a [x, y] + b [x, y];
				}
			}
			return new Matrix3x3 (res);
		}

		public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b) {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; y < 3; y++) {
					res [x, y] = a [x, y] - b [x, y];
				}
			}
			return new Matrix3x3 (res);
		}

		public static Matrix3x3 operator *(Matrix3x3 m, double v) {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; y < 3; y++) {
					res [x, y] = m [x, y] * v;
				}
			}
			return new Matrix3x3 (res);
		}

		public static Vec2 operator *(Matrix3x3 m, Vec2 v) {
			return new Vec2 (
				m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2],
				m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2]
			);
		}

		public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b) {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; y < 3; y++) {
					var sum = 0.0;
					for (var z = 0; z < 3; z++)
						sum += a [x, z] * b [z, y];
					res [x, y] = sum;
				}
			}
			return new Matrix3x3 (res);
		}

		public static Matrix3x3 Translate(double x, double y) {
			return new Matrix3x3 (new double[,] {
				{ 1, 0, x },
				{ 0, 1, y },
				{ 0, 0, 1 }
			});
		}

		public static Matrix3x3 Translate(Vec2 offset) {
			return Translate (offset.x, offset.y);
		}

		public static Matrix3x3 Rotate(double radians) {
			var x = Vec2.FromRadians(radians);
			var y = x.CounterPerpendicular();
			return new Matrix3x3 (new double[,] {
				{ x.x, y.x, 0 },
				{ x.y, y.y, 0 },
				{ 0, 0, 1 }
			});
		}

		public static Matrix3x3 Scale(double x, double y) {
			return new Matrix3x3 (new double[,] {
				{ x, 0, 0 },
				{ 0, y, 0 },
				{ 0, 0, 1 }
			});
		}

		public static Matrix3x3 Scale(Vec2 scale) {
			return Scale (scale.x, scale.y);
		}

		public static Matrix3x3 TRS(Vec2 offset, double radians, Vec2 scale) {
			var s = Vec2.FromRadians(radians);
			var x = s * scale.x;
			var y = s.CounterPerpendicular() * scale.y;
			return new Matrix3x3 (new double[,] {
				{ x.x, y.x, offset.x },
				{ x.y, y.y, offset.y },
				{ 0, 0, 1}
			});
		}

		public static readonly Matrix3x3 identity = new Matrix3x3(new double[,] {
			{ 1, 0, 0 },
			{ 0, 1, 0 },
			{ 0, 0, 1 }
		});

	}

}