using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body : Behavior {
		public double speed = 1;
		public double acceleration = 1;
		public Vec2 destination = Vec2.zero;

		public CircleShape shape { get; private set; }
		public State state { get; private set; }
		public Vec2 velocity { get; private set; }

		public override void Setup() {
			shape = node.GetAddBehavior(() => new CircleShape());
		}

		public override void Update() {
			if (state == State.Still)
				return;
			
			var target = Vec2.zero;

			if (state == State.Moving) {
				var trip = destination - transform.position;
				if (trip.sqrMagnitude > sqrTheshold)
					target = (destination - transform.position).Normalized() * speed;
				else
					state = State.Stopping;
			}

			velocity += (target - velocity).Clamp(acceleration * Tick.delta);
			
			transform.position += velocity * Tick.delta;

			if (velocity == Vec2.zero)
				state = State.Still;
		}

		public void MoveTo(Vec2 position) {
			destination = position;
			state = State.Moving;
		}

		// t = v / a
		// (v * t) / 2
		// double GetStopDistance() {
		// 	return velocity.Dot(velocity / acceleration) / 2;
		// }

		static double threshold = 0.25;
		static double sqrTheshold = threshold * threshold;

		public enum State {
			Still,
			Moving,
			Stopping
		}
		
	}

}
