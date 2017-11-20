using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cell {

	public class MeshBody : IBody {
		public Mesh2 source;

		public Transform transform { get; private set; }
		public Matrix3x3 matrix { get; private set; }
		public Mesh2 mesh { get; private set; }
		public Grid grid { get; set; }
		public Rect previousCells { get; set; }

		public Bounds2 bounds {
			get {
				return mesh.bounds;
			}
		}

		public MeshBody() {
			transform = new Transform();
			mesh = new Mesh2();
			previousCells = new Rect();
		}

		public void Update() {
			transform.Update();

			if (!transform.altered)
				return;
			
			matrix = transform.GetMatrix();

			if (mesh.vertices.Length != source.vertices.Length)
				mesh.vertices = new Vec2[source.vertices.Length];

			for (var i = 0; i < mesh.vertices.Length; i++)
				mesh.vertices[i] = matrix * source.vertices[i];

			mesh.Update();
		}

		public void Post() {
			transform.Post();
		}

		public Collision CheckCollision(IBody other) {
			return Collision.CheckAxes (this, other);
		}

		public HashSet<Vec2> GetSurfaceAxes() {
			return mesh.surfaceAxes;
		}

		public Line Project(Vec2 axis) {
			var min = double.PositiveInfinity;
			var max = double.NegativeInfinity;
			for (var v = 0; v < mesh.vertices.Length; v++) {
				var projection = mesh.vertices[v].Dot(axis);
				min = Math.Min(min, projection);
				max = Math.Max(max, projection);
			}
			var line = new Line();
			line.min = min;
			line.max = max;
			return line;
		}

	}

}
