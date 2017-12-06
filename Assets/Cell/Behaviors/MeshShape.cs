using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cell {

	public class MeshShape : Shape {
		public readonly Mesh2 source;

		public Matrix3x3 matrix { get; private set; }
		public Mesh2 mesh { get; private set; }

		public MeshShape(Mesh2 source) : base() {
			this.source = source.Clone();
			mesh = new Mesh2();
		}

		public override void Update() {
			if (!transform.altered)
				return;

			if (mesh.vertices.Length != source.vertices.Length)
				mesh.vertices = new Vec2[source.vertices.Length];

			for (var i = 0; i < mesh.vertices.Length; i++)
				mesh.vertices[i] = transform.matrix * source.vertices[i];

			mesh.Update();

			bounds = mesh.bounds;
			surfaceAxes = mesh.surfaceAxes;

			base.Update();
		}

		public override Collision CheckCollision(Shape other) {
			return Collision.CheckAxes (this, other);
		}

		public override Line Project(Vec2 axis) {
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
