using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Matrix3x3 {
		public readonly double[,] elements;

		public Matrix3x3(double[,] elements) {
			this.elements = elements;
		}

		public double this[int row, int column] {
			get {
				return elements [row, column];
			}
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType())
				return false;
			var other = (Matrix3x3) obj;
			for (var x = 0; x < 3; x++) {
				for (var y = 0; x < 3; y++) {
					if (other [x, y] != this [x, y])
						return false;
				}
			}
			return true;
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

		public Matrix3x3 Transpose() {
			var res = new double[3, 3];
			for (var x = 0; x < 3; x++) {
				for (var y = 0; x < 3; y++) {
					res [x, y] = this [y, x];
				}
			}
			return new Matrix3x3 (res);
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

		public static Vector2 operator *(Matrix3x3 m, Vector2 v) {
			return new Vector2 (
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

		public static Matrix3x3 Translate(Vector2 offset) {
			return Translate (offset.x, offset.y);
		}

		public static Matrix3x3 Rotate(double radians) {
			return new Matrix3x3 (new double[,] {
				{ Math.Cos(radians), -Math.Sin(radians), 0 },
				{ Math.Sin(radians), Math.Cos(radians), 0 },
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

		public static Matrix3x3 Scale(Vector2 scale) {
			return Scale (scale.x, scale.y);
		}

		public static Matrix3x3 identity = new Matrix3x3(new double[,] {
			{ 1, 0, 0 },
			{ 0, 1, 0 },
			{ 0, 0, 1 }
		});

	}

}