using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body : Behavior {
		public double speed = 1;
		public double acceleration = 1;
		public Vec2 destination = Vec2.zero;

		public CircleShape shape { get; private set; }
		public bool moving { get; private set; }
		public Vec2 velocity { get; private set; }

		public override void Setup() {
			shape = node.GetAddBehavior(() => new CircleShape());
		}

		public override void Update() {
			if (!moving)
				return;
			
			var target = Vec2.zero;
			var trip = destination - transform.position;
			if (trip.sqrMagnitude > sqrTheshold)
				target = (destination - transform.position).Normalized() * speed;

			velocity += (target - velocity).Clamp(acceleration * Tick.delta);
			
			transform.position += velocity * Tick.delta;

			if (velocity == Vec2.zero)
				moving = false;
		}

		public void MoveTo(Vec2 position) {
			destination = position;
			moving = true;
		}

		static double threshold = 0.02;
		static double sqrTheshold = threshold * threshold;
		
	}

}
