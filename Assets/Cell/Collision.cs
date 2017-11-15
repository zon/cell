﻿using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Collision {
		public readonly Body body;
		public readonly Vector2 push;

		public Collision(Body body, Vector2 push) {
			this.body = body;
			this.push = push;
		}

	}

}