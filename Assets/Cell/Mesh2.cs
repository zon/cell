using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Mesh2 {
		public Vector2[] vertices;
		public Bounds2 bounds;

		public Mesh2(Vector2[] vertices) {
			this.vertices = vertices;
			Update();
		}

		public Mesh2() {
			vertices = new Vector2[0];
			bounds = new Bounds2();
		}

		public Mesh2 Clone() {
			return new Mesh2((Vector2[]) vertices.Clone());
		}

		public void Update() {
			var min = Vector2.zero;
			var max = Vector2.zero;
			for (var v = 0; v < vertices.Length; v++) {
				var vert = vertices[v];
				min = min.Min(vert);
				max = max.Max(vert);
			}
			bounds = Bounds2.MinMax(min, max);
		}

		public static Mesh2 square = new Mesh2(new Vector2[] {
			new Vector2(0.5, 0.5),
			new Vector2(0.5, -0.5),
			new Vector2(-0.5, -0.5),
			new Vector2(-0.5, 0.5)
		});

	}

}