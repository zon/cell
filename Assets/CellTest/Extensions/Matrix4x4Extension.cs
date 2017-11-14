using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Matrix4x4Extension {

	public static Matrix3x3 To3x3(this Matrix4x4 m) {
		return new Matrix3x3(new double[,] {
			{ m[0, 0], m[0, 1], m[0, 2] },
			{ m[1, 0], m[1, 1], m[1, 2] },
			{ m[2, 0], m[2, 1], m[2, 2] }
		});
	}

}
