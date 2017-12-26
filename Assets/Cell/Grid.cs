using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Grid {
		public readonly double scale;
		public readonly int size;
		public readonly Cell[,] cells;

		public Grid(int size, double scale) {
			this.scale = scale;
			this.size = size;

			cells = new Cell[size, size];
			for (var y = 0; y < size; y++) {
				for (var x = 0; x < size; x++) {
					cells[x, y] = new Cell();
				}
			}
		}

		public void Add(Shape shape) {
			shape.grid = this;
		}

		public void Remove(Shape shape) {
			RemoveShape(shape);
			shape.grid = null;
		}

		public HashSet<Shape> Get(int minX, int minY, int maxX, int maxY) {
			var result = new HashSet<Shape>();
			Loop(minX, minY, maxX, maxX, c => result.UnionWith(c.shapes));
			return result;
		}

		public HashSet<Shape> Get(Area area) {
			return Get(area.min.x, area.min.y, area.max.x, area.max.y);
		}

		public List<Collision> GetCollisions(Shape shape) {
			var neighbors = Get(shape.area);
			var collisions = new List<Collision>();
			foreach (var neighbor in neighbors) {
				if (neighbor.isTrigger)
					continue;
				var collision = shape.CheckCollision(neighbor);
				if (collision != null)
					collisions.Add(collision);
			}
			return collisions;
		}

		public void Update(Shape shape) {
			if (shape.transform.altered) {
				RemoveShape(shape);
				AddShape(shape);
			}
		}

		void AddShape(Shape shape) {
			shape.FitArea(scale);
			Loop(shape.area, c => c.shapes.Add(shape));
		}

		void RemoveShape(Shape shape) {
			Loop(shape.area, c => c.shapes.Remove(shape));
		}

		void Loop(int minX, int minY, int maxX, int maxY, Action<Cell> callback) {
			var xStart = Math.Max(minX, 0);
			var xEnd = Math.Min(maxX, size - 1);
			var yStart = Math.Max(minY, 0);
			var yEnd = Math.Min(maxY, size - 1);
			for (var y = yStart; y <= yEnd; y++) {
				for (var x = xStart; x <= xEnd; x++) {
					callback(cells[x, y]);
				}
			}
		}

		void Loop(Area area, Action<Cell> callback) {
			Loop(area.min.x, area.min.y, area.max.x, area.max.y, callback);
		}

	}

}
