using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Node {
		public readonly int id;
		public string name;
		public readonly Transform transform;

		Dictionary<Type, Behavior> behaviors = new Dictionary<Type, Behavior>();

		public Node(string name) {
			id = ++autoId;
			this.name = name;
			transform = new Transform();
			AddBehavior(transform);
			all.Add(this);
		}

		public B GetBehavior<B>() where B : Behavior {
			Behavior res;
			if (behaviors.TryGetValue(typeof(B), out res))
				return (B) res;
			else
				return default(B);
		}

		public B GetAddBehavior<B>(Func<B> create) where B : Behavior {
			var type = typeof(B);
			Behavior behavior;
			if (!behaviors.TryGetValue(type, out behavior))
				behavior = create();
			return (B) behavior;
		}

		public B AddBehavior<B>(B behavior) where B : Behavior {
			var type = behavior.GetType();
			RemoveBehavior(behavior, false);
			behaviors.Add(type, behavior);
			behavior.node = this;
			Behavior.Add(behavior);
			behavior.Setup();
			return behavior;
		}

		public bool RemoveBehavior(Behavior behavior) {
			return RemoveBehavior(behavior, true);
		}

		bool RemoveBehavior(Behavior behavior, bool global) {
			var removed = behaviors.Remove(behavior.GetType());
			behavior.node = null;
			if (global)
				Behavior.Remove(behavior);
			return removed;
		}

		public void Destroy() {
			foreach (var pair in behaviors)
				pair.Value.OnDestroy();
			transform.OnDestroy();
			foreach (var pair in behaviors)
				RemoveBehavior(pair.Value);
			RemoveBehavior(transform);
			all.Remove(this);
		}

		public override string ToString() {
			return string.Format("Node({0})", name);
		}

		static int autoId;
		static HashSet<Node> all = new HashSet<Node>();

	}

}