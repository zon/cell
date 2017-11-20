using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Cell {

	public struct Bounds2 {
		public readonly Vector2 center;
		public readonly Vector2 size;
		public readonly Vector2 extents;
		public readonly Vector2 min;
		public readonly Vector2 max;

		public Bounds2(Vector2 center, Vector2 size) {
			this.center = center;
			this.size = size;
			extents = size / 2;
			min = center - extents;
			max = center + extents;
		}

		public bool Contains(Vector2 point) {
			if (point.x < min.x)
				return false;
			if (point.y < min.y)
				return false;
			if (point.x > max.x)
				return false;
			if (point.y > max.y)
				return false;
			return true;
		}

		public static Bounds2 MinMax(Vector2 min, Vector2 max) {
			var size = max - min;
			var center = min + size / 2;
			return new Bounds2 (center, size);
		}

	}
	
}
