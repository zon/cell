using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Behavior {
		public Node node;

		public Transform transform {
			get {
				return node.transform;
			}
		}

		public virtual void Setup() {}
		public virtual void Update() {}
		public virtual void PostUpdate() {}
		public virtual void OnDestroy() {}

		public B GetBehavior<B>() where B : Behavior {
			if (node != null)
				return node.GetBehavior<B>();
			else
				return default(B);
		}

		static Dictionary<Type, HashSet<Behavior>> all = new Dictionary<Type, HashSet<Behavior>>();

		public static void CoreUpdate() {
			Loop<Pedestrian>(b => b.Update());
			Loop<Pedestrian>(b => b.PhysicsUpdate());
			Loop<Transform>(t => t.Update());
			Loop<Transform>(t => t.ChildUpdate());
			Loop<MeshShape>(s => s.Update());
			Loop<CircleShape>(s => s.Update());
			Loop<Transform>(t => t.PostUpdate());
		}

		public static void Loop<B>(Action<B> callback) where B : Behavior {
			HashSet<Behavior> behaviors;
			if (!all.TryGetValue(typeof(B), out behaviors))
				return;
			foreach (var behavior in behaviors)
				callback((B) behavior);
		}

		public static void Add(Behavior behavior) {
			var type = behavior.GetType();
			HashSet<Behavior> behaviors;
			if (!all.TryGetValue(type, out behaviors)) {
				behaviors = new HashSet<Behavior>();
				all.Add(type, behaviors);
			}
			behaviors.Add(behavior);
		}

		public static bool Remove(Behavior behavior) {
			var type = behavior.GetType();
			HashSet<Behavior> behaviors;
			if (all.TryGetValue(type, out behaviors))
				return behaviors.Remove(behavior);
			else
				return false;
		}
		
	}

}
