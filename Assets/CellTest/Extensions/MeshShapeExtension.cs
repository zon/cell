using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class MeshShapeExtension {

	public static MeshShapeView CreateView(this MeshShape shape) {
		var view = new GameObject(shape.node.name).AddComponent<MeshShapeView>();
		view.Attach(shape);
		return view;
	}

}
