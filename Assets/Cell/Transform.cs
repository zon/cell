using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Transform {
		public Vector2 position;
		public double rotation;
		public Vector2 scale = Vector2.one;

		public Matrix3x3 GetMatrix() {
			return Matrix3x3.TRS(position, rotation, scale);
		}

	}

}

