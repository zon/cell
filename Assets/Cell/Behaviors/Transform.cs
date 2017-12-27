using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Transform : Behavior {
		public bool altered;

		Vec2 _localPosition;
		double _localRotation;
		Vec2 _localScale = Vec2.one;

		public readonly HashSet<Transform> children = new HashSet<Transform>();
		
		public Transform parent { get; private set; }
		public bool isRoot { get; private set; }
		public Matrix3x3 localMatrix { get; private set; }
		public Matrix3x3 matrix { get; private set; }
		public Vec2 position { get; private set; }
		public double rotation { get; private set; }
		public Vec2 scale { get; private set; }

		public Vec2 localPosition {
			get {
				return _localPosition;
			}
			set {
				_localPosition = value;
				altered = true;
			}
		}

		public double localRotation {
			get {
				return _localRotation;
			}
			set {
				_localRotation = value;
				altered = true;
			}
		}

		public Vec2 localScale {
			get {
				return _localScale;
			}
			set {
				_localScale = value;
				altered = true;
			}
		}

		public Transform() {
			localMatrix = Matrix3x3.identity;
			matrix = Matrix3x3.identity;
			scale = Vec2.one;
			altered = true;
		}

		public void SetParent(Transform parent) {
			if (this.parent != null)
				this.parent.children.Remove(this);
			this.parent = parent;
			isRoot = parent == null;
			if (!isRoot)
				parent.children.Add(this);
			altered = true;
		}

		public override void Update() {
			if (!altered) return;

			localMatrix = Matrix3x3.TRS(_localPosition, _localRotation, _localScale);

			foreach (var child in children)
				child.altered = true;
		}

		public void ChildUpdate() {
			if (!altered) return;

			if (parent != null) {
				matrix = parent.matrix * localMatrix;
				position = parent.matrix * _localPosition;
				rotation = parent.rotation + _localRotation;
				scale = parent.scale * _localScale;

			} else {
				matrix = localMatrix;
				position = _localPosition;
				rotation = _localRotation;
				scale = _localScale;
			}
		}

		public override void PostUpdate() {
			altered = false;
		}

	}

}

