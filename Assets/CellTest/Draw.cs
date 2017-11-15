using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Draw {

	public static void Line(
		Cell.Vector2 start, Cell.Vector2 end, Color color, float duration = 0, bool depthTest = true
	) {
		Debug.DrawLine(start.ToUnity(), end.ToUnity(), color, duration, depthTest);
	}

}
