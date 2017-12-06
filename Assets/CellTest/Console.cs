using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Console {

	public static void Log(params object[] objects) {
		var text = "";
		for (var o = 0; o < objects.Length; o++) {
			if (o != 0)
				text += ", ";
			text += objects[o].ToString();
		}
		Debug.Log(text);
	}

}
