using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Cell {
		public readonly HashSet<Obstacle> obstacles = new HashSet<Obstacle>();
		public readonly HashSet<Body> bodies = new HashSet<Body>();
	}

}
