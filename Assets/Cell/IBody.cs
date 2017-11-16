using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public interface IBody {
		Vector2 position { get; set; }
		double rotation { get; set; }
		Vector2 scale { get; set; }

		void Update();

		Collision CheckCollision(IBody other);

		Line Project(Vector2 axis);

	}

}
