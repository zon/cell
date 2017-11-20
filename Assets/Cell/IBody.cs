using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public interface IBody {
		Transform transform { get; }
		Bounds2 bounds { get; }
		Grid grid { get; set; }
		Rect previousCells { get; set; }

		void Update();
		void Post();

		Collision CheckCollision(IBody other);

		HashSet<Vec2> GetSurfaceAxes ();
		Line Project(Vec2 axis);

	}

}
