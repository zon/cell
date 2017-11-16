using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class CircleBody : IBody {
		public double radius = 0.5;
		public Vector2 position { get; set; }
		public double rotation { get; set; }
		public Vector2 scale { get; set; }

		static HashSet<Vector2> surfaceAxes = new HashSet<Vector2>();

		public double scaleRadius {
			get { return radius * Math.Max(scale.x, scale.y); }
		}

		public void Update() {}

		public Collision CheckCollision(IBody other) {
			if (other is MeshBody)
				return Collision.CheckAxes (this, other as MeshBody);
			else if (other is CircleBody)
				return Collision.CheckRadius (this, other as CircleBody);
			else
				return null;
		}

		public HashSet<Vector2> GetSurfaceAxes() {
			return surfaceAxes;
		}

		public Line Project(Vector2 axis) {
			var p = position.Dot(axis);
			var r = scaleRadius;
			return new Line(p - r, p + r);
		}

	}

}