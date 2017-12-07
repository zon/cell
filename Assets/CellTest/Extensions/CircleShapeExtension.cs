using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class CircleShapeExtension {

	public static CircleShapeView CreateView(this CircleShape shape) {
		var view = new GameObject(shape.node.name).AddComponent<CircleShapeView>();
		view.Attach(shape);
		return view;
	}

}