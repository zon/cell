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

		public void Add(Body body) {
			body.shape.grid = this;
		}

		public void Remove(Body body) {
			RemoveBody(body);
			body.shape.grid = null;
		}

		public HashSet<Body> Get(int minX, int minY, int maxX, int maxY) {
			var result = new HashSet<Body>();
			var xStart = Math.Max(minX, 0);
			var xEnd = Math.Min(maxX, size - 1);
			var yStart = Math.Max(minY, 0);
			var yEnd = Math.Min(maxY, size - 1);
			for (var y = yStart; y <= yEnd; y++) {
				for (var x = xStart; x <= xEnd; x++) {
					result.UnionWith(cells[x, y].bodies);
				}
			}
			return result;
		}

		public HashSet<Body> Get(Area area) {
			return Get(area.min.x, area.min.y, area.max.x, area.max.y);
		}

		public List<Collision> CheckCollision(Body body) {
			var neighbors = Get(body.shape.area);
			var collisions = new List<Collision>();
			foreach (var neighbor in neighbors) {
				var collision = body.shape.CheckCollision(neighbor.shape);
				if (collision != null)
					collisions.Add(collision);
			}
			return collisions;
		}

		public void Update(Body body) {
			if (body.shape.transform.altered) {
				RemoveBody(body);
				AddBody(body);
			}
		}

		void AddObstacle(Obstacle obstacle) {
			obstacle.shape.FitArea(scale);
			Loop(obstacle.shape.area, c => c.obstacles.Add(obstacle));
		}

		void RemoveObstacle(Obstacle obstacle) {
			Loop(obstacle.shape.area, c => c.obstacles.Remove(obstacle));
		}

		void AddBody(Body body) {
			body.shape.FitArea(scale);
			Loop(body.shape.area, c => c.bodies.Add(body));
		}

		void RemoveBody(Body body) {
			Loop(body.shape.area, c => c.bodies.Remove(body));
		}

		void Loop(Area area, Action<Cell> callback) {
			var xStart = Math.Max(area.min.x, 0);
			var xEnd = Math.Min(area.max.x, size - 1);
			var yStart = Math.Max(area.min.y, 0);
			var yEnd = Math.Min(area.max.y, size - 1);
			for (var y = yStart; y <= yEnd; y++) {
				for (var x = xStart; x <= xEnd; x++) {
					callback(cells[x, y]);
				}
			}
		}

	}

}
