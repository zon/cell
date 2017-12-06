using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Collision {
		public readonly Shape shape;
		public readonly Vec2 overlap;

		public Collision(Shape shape, Vec2 overlap) {
			this.shape = shape;
			this.overlap = overlap;
		}

		public static Collision CheckAxes(Shape aBody, Shape bBody) {
			if (aBody == bBody)
				return null;

			var minOverlap = double.PositiveInfinity;
			var minOverlapVector = Vec2.zero;

			var axes = new HashSet<Vec2> ();
			axes.UnionWith (aBody.surfaceAxes);
			axes.UnionWith (bBody.surfaceAxes);
			foreach (var axis in axes) {

				var a = aBody.Project (axis);
				var b = bBody.Project (axis);

				Draw.Line(axis * b.min, axis * b.max, UnityEngine.Color.cyan);
				Draw.Line(axis * a.min, axis * a.max, UnityEngine.Color.magenta);

				var overlap = a.Overlap (b);
				if (overlap > 0) {
					if (overlap < minOverlap) {
						minOverlap = overlap;
						minOverlapVector = axis * overlap;
					}

				} else {
					return null;
				}
			}

			var delta = bBody.transform.position - aBody.transform.position;
			if (delta.Dot (minOverlapVector) > 0)
				minOverlapVector *= -1;

			return new Collision (bBody, minOverlapVector);
		}

		public static Collision CheckRadius(CircleShape aBody, CircleShape bBody) {
			if (aBody == bBody)
				return null;
			var delta = bBody.transform.position - aBody.transform.position;
			var min = bBody.radius + aBody.radius;
			if (delta.sqrMagnitude < min * min) {
				return new Collision(bBody, delta.Normalized() * (min - delta.magnitude));
			} else {
				return null;
			}
		}

	}

}
