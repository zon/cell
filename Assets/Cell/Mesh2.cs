using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Mesh2 {
		public Vector2[] vertices;
		public Bounds2 bounds;

		HashSet<Vector2> _surfaceAxes = new HashSet<Vector2>();

		public HashSet<Vector2> surfaceAxes {
			get { return _surfaceAxes; }
		}

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
			_surfaceAxes.Clear();
			
			var min = Vector2.zero;
			var max = Vector2.zero;
			
			for (var a = 0; a < vertices.Length; a++) {
				var b = (a + 1) % vertices.Length;
				var vert = vertices[a];

				var axis = (vert - vertices[b]).CounterPerpendicular().Normalized();
				if (axis.x < 0)
					axis *= -1;

				_surfaceAxes.Add(axis);
				
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