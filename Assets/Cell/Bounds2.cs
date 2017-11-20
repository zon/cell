using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Bounds2 {
		public readonly Vec2 center;
		public readonly Vec2 size;
		public readonly Vec2 extents;
		public readonly Vec2 min;
		public readonly Vec2 max;

		public Bounds2(Vec2 center, Vec2 size) {
			this.center = center;
			this.size = size;
			extents = size / 2;
			min = center - extents;
			max = center + extents;
		}

		public bool Contains(Vec2 point) {
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

		public static Bounds2 MinMax(Vec2 min, Vec2 max) {
			var size = max - min;
			var center = min + size / 2;
			return new Bounds2 (center, size);
		}

		public static Bounds2 MinMax(double minX, double minY, double maxX, double maxY) {
			return MinMax(new Vec2(minX, minY), new Vec2(maxX, maxY));
		}

		public override string ToString() {
			return string.Format("Bounds2({0}, {1}, {2}, {3})", center.x, center.y, size.x, size.y);
		}

	}
	
}
