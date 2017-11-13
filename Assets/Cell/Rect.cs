using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Bounds {
		public readonly Vector2 min;
		public readonly Vector2 max;

		public Vector2 size { get { return max - min; } }

		public Bounds(Vector2 min, Vector2 max) {
			this.min = min;
			this.max = max;
		}

	}
	
}
