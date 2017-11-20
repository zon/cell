using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class QuatExtension {

	public static Quat ToCell(this Quaternion q) {
		return new Quat(q.x, q.y, q.z, q.w);
	}

	public static Quaternion ToUnity(this Quat q) {
		return new Quaternion((float) q.x, (float) q.y, (float) q.z, (float) q.w);
	}

}
