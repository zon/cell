using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body : Behavior {
		public double speed = 1;
		public double acceleration = 1;
		public Vec2 destination;

		Vec2 _velocity;

		public CircleShape shape { get; private set; }
		public State state { get; private set; }

		public Vec2 velocity {
			get {
				return _velocity;
			}
			set {
				_velocity = value;
				if (state == State.Still)
					state = State.Coasting;
			}
		}

		public override void Setup() {
			shape = node.GetAddBehavior(() => new CircleShape());
		}

		public override void Update() {
			if (state == State.Still)
				return;
			
			var target = Vec2.zero;

			if (state == State.Moving) {
				var trip = destination - transform.position;
				var stop = velocity.Dot(velocity / acceleration) / 2;
				if (trip.sqrMagnitude > stop * stop)
					target = (destination - transform.position).Normalized() * speed;
				else
					state = State.Stopping;
			}

			if (state != State.Coasting)
				velocity += (target - velocity).Clamp(acceleration * Tick.delta);
		}

		public void PhysicsUpdate() {
			if (state == State.Still)
				return;

			var collisions = shape.grid.GetCollisions(shape);
			for (var c = 0; c < collisions.Count; c++) {
				var collision = collisions[c];

				var other = collision.shape.GetBehavior<Body>();
				if (other != null) {

					var normal = (transform.position - other.transform.position).Normalized();
					var a = velocity.Dot(normal);
					var b = other.velocity.Dot(normal);

					var p = ((a - b) * 2) / (shape.mass + other.shape.mass);

					velocity -= normal * p * other.shape.mass;
					other.velocity += normal * p * shape.mass;

					shape.Update();
					other.shape.Update();
				
				} else {
					transform.position += collision.overlap;
					shape.Update();
				}
			}
			
			transform.position += velocity * Tick.delta;

			if (velocity == Vec2.zero)
				state = State.Still;
		}

		public void MoveTo(Vec2 position) {
			destination = position;
			state = State.Moving;
		}

		public void Stop() {
			state = State.Stopping;
		}

		public enum State {
			Still,
			Coasting,
			Moving,
			Stopping
		}
		
	}

}
