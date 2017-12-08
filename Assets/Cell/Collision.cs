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

		public static Collision Check(Shape aShape, Shape bShape) {
			if (aShape == bShape)
				return null;
			if (aShape is MeshShape || bShape is MeshShape)
				return CheckAxes(aShape, bShape);
			else
				return CheckRadius((CircleShape) aShape, (CircleShape) bShape);
		}

		static Collision CheckAxes(Shape aShape, Shape bShape) {
			var minOverlap = double.PositiveInfinity;
			var minOverlapVector = Vec2.zero;

			var axes = new HashSet<Vec2> ();
			axes.UnionWith (aShape.surfaceAxes);
			axes.UnionWith (bShape.surfaceAxes);
			if (aShape is CircleShape || bShape is CircleShape)
				axes.Add((bShape.bounds.center - aShape.bounds.center).Normalized());
			foreach (var axis in axes) {

				var a = aShape.Project (axis);
				var b = bShape.Project (axis);

				// Draw.Line(axis * b.min, axis * b.max, UnityEngine.Color.cyan);
				// Draw.Line(axis * a.min, axis * a.max, UnityEngine.Color.magenta);

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

			var delta = bShape.bounds.center - aShape.bounds.center;
			if (delta.Dot (minOverlapVector) > 0)
				minOverlapVector *= -1;

			return new Collision (bShape, minOverlapVector);
		}

		static Collision CheckRadius(CircleShape aCircle, CircleShape bCircle) {
			if (aCircle == bCircle)
				return null;
			var delta = bCircle.transform.position - aCircle.transform.position;
			var min = bCircle.radius + aCircle.radius;
			if (delta.sqrMagnitude < min * min) {
				return new Collision(bCircle, delta.Normalized() * (min - delta.magnitude));
			} else {
				return null;
			}
		}

	}

}
