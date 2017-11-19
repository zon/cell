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

		public MeshBody() {
			transform = new Transform();
			mesh = new Mesh2();
		}

		public void Update() {
			matrix = transform.GetMatrix();

			if (mesh.vertices.Length != source.vertices.Length)
				mesh.vertices = new Vector2[source.vertices.Length];

			for (var i = 0; i < mesh.vertices.Length; i++)
				mesh.vertices[i] = matrix * source.vertices[i];

			mesh.Update();
		}

		public Collision CheckCollision(IBody other) {
			return Collision.CheckAxes (this, other);
		}

		public HashSet<Vector2> GetSurfaceAxes() {
			return mesh.surfaceAxes;
		}

		public Line Project(Vector2 axis) {
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
