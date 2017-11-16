using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class CircleBody : IBody {
		public double radius;
		public Vector2 position;
		public double rotation;
		public Vector2 scale = Vector2.one;

		public double scaleRadius {
			get { return radius * Math.Max(scale.x, scale.y); }
		}

		

	}

}