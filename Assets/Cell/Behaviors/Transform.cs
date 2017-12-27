using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Transform : Behavior {
		Vec2 _localPosition;
		double _localRotation;
		Vec2 _localScale = Vec2.one;
		Vec2 _position;
		Vec2 _scale = Vec2.one;
		Transform _parent;

		public readonly HashSet<Transform> children = new HashSet<Transform>();
		
		public Transform parent { get; private set; }
		public bool isRoot { get; private set; }
		public Matrix3x3 matrix { get; private set; }
		public bool altered { get; private set; }

		public Vec2 localPosition {
			get {
				return _localPosition;
			}
			set {
				_localPosition = value;
				if (_parent != null)
					_position = _parent.position + (_localPosition * _parent.scale);
				else
					_position = _localPosition;
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
				_scale = (_parent != null ? _parent.scale : Vec2.one) * _localScale;
				altered = true;
			}
		}

		public Vec2 position {
			get {
				return _position;
			}
			set {
				_position = value;
				if (_parent != null)
					_localPosition = (_position - _parent.position) / _parent.scale;
				else
					_localPosition = _position;
				altered = true;
			}
		}

		public Vec2 scale {
			get {
				return _scale;
			}
			set {
				_scale = value;
				_localScale = _scale / (_parent != null ? _parent.scale : Vec2.one);
				altered = true;
			}
		}

		public Transform() {
			matrix = Matrix3x3.identity;
			altered = true;
		}

		public void SetParent(Transform parent) {
			if (this.parent != null)
				this.parent.children.Remove(this);
			this.parent = parent;
			isRoot = parent == null;
			if (!isRoot)
				parent.children.Add(this);
			localPosition = _localPosition;
			localScale = _localScale;
		}

		public override void Update() {
			if (altered)
				ForceUpdate();
		}

		public void ForceUpdate() {
			matrix = Matrix3x3.TRS(_position, _localRotation, _scale);
			foreach (var child in children)
				child.ForceUpdate();
		}

		public override void PostUpdate() {
			altered = false;
		}

	}

}

