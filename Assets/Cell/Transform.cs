using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Transform : Behavior {
		Vec2 _position;
		double _rotation;
		Vec2 _scale = Vec2.one;
		
		public Matrix3x3 matrix { get; private set; }
		public bool altered { get; private set; }

		public Vec2 position {
			get {
				return _position;
			}
			set {
				_position = value;
				altered = true;
			}
		}

		public double rotation {
			get {
				return _rotation;
			}
			set {
				_rotation = value;
				altered = true;
			}
		}

		public Vec2 scale {
			get {
				return _scale;
			}
			set {
				_scale = value;
				altered = true;
			}
		}

		public Transform() {
			matrix = Matrix3x3.identity;
			altered = true;
		}

		public override void Update() {
			if (altered)
				matrix = Matrix3x3.TRS(position, rotation, scale);
		}

		public override void PostUpdate() {
			altered = false;
		}

	}

}

