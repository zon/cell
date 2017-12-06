using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public static class Tick {
		public static double delta { get; private set; }

		public static void Setup(double delta) {
			Tick.delta = delta;
		}

	}

}
