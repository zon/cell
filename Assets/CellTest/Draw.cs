using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Draw {

	public static void Line(
		Vec2 start, Vec2 end, Color color, float duration = 0, bool depthTest = true
	) {
		Debug.DrawLine(start.ToUnity(), end.ToUnity(), color, duration, depthTest);
	}

	public static void Bounds(
		Vec2 min, Vec2 max, Color color, float duration = 0, bool depthTest = true
	) {
		Line(new Vec2(min.x, min.y), new Vec2(max.x, min.y), color, duration, depthTest);
		Line(new Vec2(max.x, min.y), new Vec2(max.x, max.y), color, duration, depthTest);
		Line(new Vec2(max.x, max.y), new Vec2(min.x, max.y), color, duration, depthTest);
		Line(new Vec2(min.x, max.y), new Vec2(min.x, min.y), color, duration, depthTest);
	}

	public static void Bounds(
		Bounds2 bounds, Color color, float duration = 0, bool depthTest = true
	) {
		Bounds(bounds.min, bounds.max, color, duration, depthTest);
	}

	public static void Rect(
		Coord min, Coord max, Color color, float duration = 0, bool depthTest = true
	) {
		Bounds(min.ToVec2(), max.ToVec2() + Vec2.one, color, duration, depthTest);
	}

	public static void Rect(
		Cell.Area rect, Color color, float duration = 0, bool depthTest = true  
	) {
		Rect(rect.min, rect.max, color, duration, depthTest);
	}

}
