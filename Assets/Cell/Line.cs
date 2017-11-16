using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public struct Line {
		public double min;
		public double max;

		public Line(double min, double max) {
			this.min = min;
			this.max = max;
		}

		public double Overlap(Line other) {
			if (max >= other.min && other.max >= min) {
				return Math.Min(other.max - min, max - other.min);
			} else {
				return 0;
			}
		}

	}		
	
		// IsSeperateResult IsSeparate(Vector2 axis, IBody other) {
		// 	var res = new IsSeperateResult();
		// 	var a = other.Project(axis); Project(_mesh, axis);
		// 	var b = Project(other.mesh, axis);

		// 	Draw.Line(axis * b.min, axis * b.max, UnityEngine.Color.cyan);
		// 	Draw.Line(axis * a.min, axis * a.max, UnityEngine.Color.magenta);

			
		// 	return res;
		// }

}