using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class CircleShape : IShape {
		public double radius = 0.5;

		static HashSet<Vec2> surfaceAxes = new HashSet<Vec2>();

		public Transform transform { get; private set; }
		public Bounds2 bounds { get; private set; }
		public Grid grid { get; set; }
		public Rect cells { get; set; }

		public double scaleRadius {
			get { return radius * Math.Max(transform.scale.x, transform.scale.y); }
		}

		public CircleShape() {
			transform = new Transform();
			cells = new Rect();
		}

		public void Update() {
			transform.Update();
		}

		public void Post() {
			transform.Post();
		}

		public Collision CheckCollision(IShape other) {
			if (other is MeshShape)
				return Collision.CheckAxes (this, other as MeshShape);
			else if (other is CircleShape)
				return Collision.CheckRadius (this, other as CircleShape);
			else
				return null;
		}

		public HashSet<Vec2> GetSurfaceAxes() {
			return surfaceAxes;
		}

		public Line Project(Vec2 axis) {
			var p = transform.position.Dot(axis);
			var r = scaleRadius;
			return new Line(p - r, p + r);
		}

	}

}