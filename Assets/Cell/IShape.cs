using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public interface IShape {
		Transform transform { get; }
		Bounds2 bounds { get; }
		Grid grid { get; set; }
		Rect cells { get; set; }

		void Update();
		void Post();

		Collision CheckCollision(IShape other);

		HashSet<Vec2> GetSurfaceAxes ();
		Line Project(Vec2 axis);

	}

}
