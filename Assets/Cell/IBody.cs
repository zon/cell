using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public interface IBody {
		Transform transform { get; }

		void Update();

		Collision CheckCollision(IBody other);

		HashSet<Vector2> GetSurfaceAxes ();
		Line Project(Vector2 axis);

	}

}
