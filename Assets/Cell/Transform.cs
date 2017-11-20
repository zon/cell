using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Transform {
		public Vec2 position;
		public Quat rotation;
		public Vec2 scale = Vec2.one;

		public Matrix3x3 GetMatrix() {
			return Matrix3x3.TRS(position, rotation, scale);
		}

	}

}

