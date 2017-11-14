﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Vector2Extension {

	public static Cell.Vector2 ToCell(this UnityEngine.Vector2 u) {
		return new Cell.Vector2 (u.x, u.y);
	}

	public static UnityEngine.Vector2 ToUnity(this Cell.Vector2 c) {
		return new UnityEngine.Vector2 ((float) c.x, (float) c.y);
	}

}