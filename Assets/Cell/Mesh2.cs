using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Mesh2 {
		public Vec2[] vertices;
		public Bounds2 bounds;

		HashSet<Vec2> _surfaceAxes = new HashSet<Vec2>();

		public HashSet<Vec2> surfaceAxes {
			get { return _surfaceAxes; }
		}

		public Mesh2(Vec2[] vertices) {
			this.vertices = vertices;
			Update();
		}

		public Mesh2() {
			vertices = new Vec2[0];
			bounds = new Bounds2();
		}

		public Mesh2 Clone() {
			return new Mesh2((Vec2[]) vertices.Clone());
		}

		public void Update() {
			_surfaceAxes.Clear();
			
			var min = Vec2.zero;
			var max = Vec2.zero;
			
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

		public static Mesh2 square = new Mesh2(new Vec2[] {
			new Vec2(0.5, 0.5),
			new Vec2(0.5, -0.5),
			new Vec2(-0.5, -0.5),
			new Vec2(-0.5, 0.5)
		});

	}

}