using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class TransformExtension {

	public static Quaternion GetUnityRotation(this Cell.Transform transform) {
		return Quaternion.Euler(0, 0, (float) (transform.rotation * Vec2.rad2deg));
	}

}
