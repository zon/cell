using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Matrix3x3Extension {

	public static Matrix4x4 To4x4(this Matrix3x3 m) {
		return new Matrix4x4(
			new Vector4((float) m[0, 0], (float) m[0, 1], (float) m[0, 2], 0),
			new Vector4((float) m[1, 0], (float) m[1, 1], (float) m[1, 2], 0),
			new Vector4((float) m[2, 0], (float) m[2, 1], (float) m[2, 2], 0),
			new Vector4(0, 0, 0, 1)
		);
	}

}
