using System;
using System.Collections;
using System.Collections.Generic;

namespace Cell {

	public class Grid {
		public readonly double scale;
		public readonly int size;
		public readonly Cell[,] cells;
		public readonly HashSet<IBody> bodies;

		public Grid(int size, double scale) {
			this.scale = scale;
			this.size = size;

			cells = new Cell[size, size];
			for (var y = 0; y < size; y++) {
				for (var x = 0; x < size; x++) {
					cells[x, y] = new Cell();
				}
			}
			bodies = new HashSet<IBody>();
		}

		public void Add(IBody body) {
			bodies.Add(body);
			body.grid = this;
		}

		public void Remove(IBody body) {
			bodies.Remove(body);
			RemoveBody(body);
			body.grid = null;
		}

		public void Update() {
			foreach (var body in bodies) {
				body.Update();
				if (body.transform.altered) {
					RemoveBody(body);
					AddBody(body);
				}
			}
		}

		public void Post() {
			foreach (var body in bodies) {
				body.Post();
			}
		}

		void AddBody(IBody body) {
			body.previousCells.Fit(body.bounds, scale);
			var bounds = body.previousCells;
			var xStart = Math.Max(bounds.min.x, 0);
			var xEnd = Math.Min(bounds.max.x + 1, size);
			var yStart = Math.Max(bounds.min.y, 0);
			var yEnd = Math.Min(bounds.max.y + 1, size);
			for (var y = yStart; y < yEnd; y++) {
				for (var x = xStart; x < xEnd; x++) {
					cells[x, y].bodies.Add(body);
				}
			}
		}

		void RemoveBody(IBody body) {
			var bounds = body.previousCells;
			var xStart = Math.Max(bounds.min.x, 0);
			var xEnd = Math.Min(bounds.max.x + 1, size);
			var yStart = Math.Max(bounds.min.y, 0);
			var yEnd = Math.Min(bounds.max.y + 1, size);
			for (var y = yStart; y < yEnd; y++) {
				for (var x = xStart; x < xEnd; x++) {
					cells[x, y].bodies.Remove(body);
				}
			}
		}

	}

}
