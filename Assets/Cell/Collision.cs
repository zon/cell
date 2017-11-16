using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Collision {
		public readonly IBody body;
		public readonly Vector2 overlap;

		public Collision(IBody body, Vector2 overlap) {
			this.body = body;
			this.overlap = overlap;
		}

		public static Collision CheckAxes(IBody aBody, IBody bBody) {
			var minOverlap = double.PositiveInfinity;
			var minOverlapVector = Vector2.zero;

			var axes = new HashSet<Vector2> ();
			axes.UnionWith (aBody.GetSurfaceAxes ());
			axes.UnionWith (bBody.GetSurfaceAxes ());
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

			var delta = bBody.position - aBody.position;
			if (delta.Dot (minOverlapVector) > 0)
				minOverlapVector *= -1;

			return new Collision (bBody, minOverlapVector);
		}

		public static Collision CheckRadius(CircleBody aBody, CircleBody bBody) {
			var delta = bBody.position - aBody.position;
			var min = bBody.radius + aBody.radius;
			if (delta.sqrMagnitude < min * min) {
				return new Collision(bBody, delta.Normalized() * (min - delta.magnitude));
			} else {
				return null;
			}
		}

	}

}
