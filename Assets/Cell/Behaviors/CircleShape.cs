using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class CircleShape : Shape {
		public double radius = 0.5;

		double lastRadius;

		public double scaleRadius { get; private set; }
		public double mass { get; private set; }

		public CircleShape() : base() {
			surfaceAxes = new HashSet<Vec2>();
		}

		public override void Update() {
			if (transform.altered || radius != lastRadius) {
				scaleRadius = radius * Math.Max(transform.scale.x, transform.scale.y);
				
				mass = Math.PI * scaleRadius * scaleRadius;

				bounds = new Bounds2(transform.position, Vec2.one * scaleRadius * 2);
				
				lastRadius = radius;
			}
			base.Update();
		}

		public override Line Project(Vec2 axis) {
			var p = transform.position.Dot(axis);
			return new Line(p - scaleRadius, p + scaleRadius);
		}

	}

}