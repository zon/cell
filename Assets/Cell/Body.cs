using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body {
		public Mesh2 source;
		public Vector2 position;
		public double rotation;
		public Vector2 scale = Vector2.one;

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

	}

}
