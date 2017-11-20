using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public interface IBody {
		Transform transform { get; }

		void Update();

		Collision CheckCollision(IBody other);

		HashSet<Vec2> GetSurfaceAxes ();
		Line Project(Vec2 axis);

	}

}
