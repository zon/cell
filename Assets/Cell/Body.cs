using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body : Behavior {

		public CircleShape shape { get; private set; }

		public override void Setup() {
			shape = node.GetAddBehavior(() => new CircleShape());
		}

	}

}
