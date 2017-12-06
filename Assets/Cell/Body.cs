using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Body {
		public readonly int id;
		public readonly CircleShape shape;

		public Transform transform {
			get {
				return shape.transform;
			}
		}

		public Body() {
			id = ++autoId;
			shape = new CircleShape();
			all.Add(this);
		}

		public void Update() {
			shape.Update();
			if (shape.grid != null)
				shape.grid.Update(this);
		}

		public void PostUpdate() {
			shape.PostUpdate();
		}

		public void Destroy() {
			all.Remove(this);
		}

		static int autoId;
		static HashSet<Body> all = new HashSet<Body>();

		public static void UpdateAll() {
			foreach (var body in all) {
				body.Update();
			}
		}

		public static void PostUpdateAll() {
			foreach (var body in all) {
				body.PostUpdate();
			}
		}

	}

}
