using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cell {

	public class MeshBody : IBody {
		public Mesh2 source;
		public Vector2 position { get; set; }
		public double rotation { get; set; }
		public Vector2 scale { get; set; }

		Matrix3x3 _matrix;
		Mesh2 _mesh = new Mesh2();

		public Matrix3x3 matrix {
			get { return _matrix; }
		}

		public Mesh2 mesh {
			get { return _mesh; }
		}

		public void Update() {
			_matrix = Matrix3x3.TRS(position, rotation, scale);

			if (mesh.vertices.Length != source.vertices.Length)
				mesh.vertices = new Vector2[source.vertices.Length];

			for (var i = 0; i < mesh.vertices.Length; i++)
				mesh.vertices[i] = _matrix * source.vertices[i];

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
			for (var v = 0; v < _mesh.vertices.Length; v++) {
				var projection = _mesh.vertices[v].Dot(axis);
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
