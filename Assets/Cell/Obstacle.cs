using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cell {

	public class Obstacle {
		public readonly Kind kind;
		public readonly MeshShape shape;

		public Transform transform {
			get {
				return shape.transform;
			}
		}

		public Obstacle(Kind kind) {
			this.kind = kind;
			shape = new MeshShape();
			switch (kind) {
				case Kind.Square:
					shape.source = square;
					break;
				case Kind.BottomLeft:
					shape.source = bottomLeft;
					break;
				case Kind.BottomRight:
					shape.source = bottomRight;
					break;
				case Kind.TopLeft:
					shape.source = topLeft;
					break;
				case Kind.TopRight:
					shape.source = topRight;
					break;
			}
		}

		public Obstacle() : this(Kind.Square) {}

		public enum Kind {
			Square,
			BottomLeft,
			BottomRight,
			TopLeft,
			TopRight
		}
		
		public static Mesh2 square = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		public static Mesh2 bottomLeft = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 0),
			new Vec2(0, 1)
		});
		
		public static Mesh2 bottomRight = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		public static Mesh2 topLeft = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		public static Mesh2 topRight = new Mesh2(new Vec2[] {
			new Vec2(1, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});

	}

}