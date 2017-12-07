using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Console {

	public static void Log(params object[] objects) {
		var text = "";
		for (var o = 0; o < objects.Length; o++) {
			if (o != 0)
				text += ", ";
			var obj = objects[o];
			if (obj != null)
				text += objects[o].ToString();
			else
				text += "null";
		}
		Debug.Log(text);
	}

}
