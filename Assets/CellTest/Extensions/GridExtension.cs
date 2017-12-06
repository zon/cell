using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class GridExtension  {

	public static void DrawCells(this Grid grid, Shape body, Color color) {
		var rect = body.area;
		var scale = grid.scale;
		var bounds = rect.ToBounds2() * scale;

		var half = new Color(color.r, color.g, color.b, color.a / 2);
		for (var y = rect.min.y + 1; y <= rect.max.y; y++) {
			Draw.Line(new Vec2(bounds.min.x, y * scale), new Vec2(bounds.max.x, y * scale), half);
		}
		for (var x = rect.min.x + 1; x <= rect.max.x; x++) {
			Draw.Line(new Vec2(x * scale, bounds.min.y), new Vec2(x * scale, bounds.max.y), half);
		}

		Draw.Bounds(bounds, color);
	}

}
