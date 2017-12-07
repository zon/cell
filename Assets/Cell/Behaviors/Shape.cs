using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public abstract class Shape : Behavior {
		public readonly Area area;
		public Grid grid;

		public Bounds2 bounds { get; protected set; }
		public HashSet<Vec2> surfaceAxes { get; protected set; }

		public Shape() {
			area = new Area();
		}

		public override void Update() {
			if (grid != null)
				grid.Update(this);
		}

		public void FitArea(double scale) {
			area.Fit(bounds, scale);
		}

		public Collision CheckCollision(Shape other) {
			return Collision.Check(this, other);
		}

		public abstract Line Project(Vec2 axis);

	}

}
