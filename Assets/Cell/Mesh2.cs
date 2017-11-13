using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Mesh2 {
		public readonly Vector2[] vertices;
		public readonly Bounds2 bounds;

		public Mesh2(Vector2[] vertices) {
			this.vertices = vertices;

			var min = Vector2.zero;
			var max = Vector2.zero;
			for (var v = 0; v < vertices.Length; v++) {
				var vert = vertices [v];
				min = min.Min (vert);
				max = max.Max (vert);
			}
			bounds = Bounds2.MinMax (min, max);
		}

	}

}