using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Obstacle : Behavior {
		public readonly Kind kind;

		public MeshShape shape { get; private set; }

		public Obstacle(Kind kind) {
			this.kind = kind;
		}

		public Obstacle() : this(Kind.Square) {}

		public override void Setup() {
			var source = square;
			switch (kind) {
				case Kind.BottomLeft:
					source = bottomLeft;
					break;
				case Kind.BottomRight:
					source = bottomRight;
					break;
				case Kind.TopLeft:
					source = topLeft;
					break;
				case Kind.TopRight:
					source = topRight;
					break;
			}
			shape = node.GetAddBehavior(() => new MeshShape(source));
		}

		public void Snap(Coord coord) {
			transform.localPosition = coord * shape.grid.scale;
		}

		public enum Kind {
			Square,
			BottomLeft,
			BottomRight,
			TopLeft,
			TopRight
		}
		
		static Mesh2 square = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		static Mesh2 bottomLeft = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 0),
			new Vec2(0, 1)
		});
		
		static Mesh2 bottomRight = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		static Mesh2 topLeft = new Mesh2(new Vec2[] {
			new Vec2(0, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});
		
		static Mesh2 topRight = new Mesh2(new Vec2[] {
			new Vec2(1, 0),
			new Vec2(1, 1),
			new Vec2(0, 1)
		});

	}

}