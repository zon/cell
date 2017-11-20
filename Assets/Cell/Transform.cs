using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Cell {

	public class Transform {
		public Vector2 position;
		public Quaternion rotation;
		public Vector2 scale = Vector2.One;

		public Matrix3x3 GetMatrix() {
			return Matrix3x3.TRS(position, rotation, scale);
		}

	}

}

