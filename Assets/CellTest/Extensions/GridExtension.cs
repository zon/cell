using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class GridExtension  {

	public static void DrawCells(this Grid grid, IBody body, Color color) {
		var rect = body.cells;
		var scale = grid.scale;

		var half = new Color(color.r, color.g, color.b, color.a / 2);
		for (var y = rect.min.y + 1; y < rect.max.y; y++) {
			Draw.Line(new Coord(rect.min.x, y) * scale, new Coord(rect.max.x, y) * scale, half);
		}
		for (var x = rect.min.x + 1; x < rect.max.x; x++) {
			Draw.Line(new Coord(x, rect.min.y) * scale, new Coord(x, rect.max.y) * scale, half);
		}

		var ld = rect.min * scale;
		var rd = new Coord(rect.max.x, rect.min.y) * scale;
		var ru = body.cells.max * grid.scale;
		var lu = new Coord(rect.min.x, rect.max.y) * scale;
		Draw.Line(ld, lu, color);
		Draw.Line(lu, ru, color);
		Draw.Line(ru, rd, color);
		Draw.Line(rd, ld, color);
	}

}
