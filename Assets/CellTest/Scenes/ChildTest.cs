using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class ChildTest : MonoBehaviour {
	[SerializeField]
	public ChildTestArgs parentArgs;
	[SerializeField]
	public ChildTestArgs[] childArgs;

	Cell.Transform parent;
	Cell.Transform[] children;

	void Start() {
		parent = new Node(parentArgs.name).transform;

		children = new Cell.Transform[childArgs.Length];
		for (var c = 0; c < childArgs.Length; c++) {
			var args = childArgs[c];
			var node = new Node(args.name);
			node.transform.SetParent(parent);
			var shape = node.AddBehavior(new MeshShape(Mesh2.square));
			shape.CreateView(args.color);
			children[c] = node.transform;
		}
	}

	void FixedUpdate() {
		parent.localPosition = parentArgs.position.ToCell();
		parent.localRotation = parentArgs.rotation;
		parent.localScale = parentArgs.scale.ToCell();

		for (var c = 0; c < children.Length; c++) {
			var child = children[c];
			var args = childArgs[c];
			child.localPosition = args.position.ToCell();
			child.localRotation = args.rotation;
			child.localScale = args.scale.ToCell();
		}

		Behavior.CoreUpdate();

		// Log(parent);
		// if (children.Length > 0)
		// 	Log(children[0]);
	}

	void Log(Cell.Transform transform) {
		print(string.Format(
			"{0}( T: {1}, R: {2}, S: {3}, C: {4} )",
			transform.node.name,
			transform.position,
			transform.rotation,
			transform.scale,
			transform.children.Count
		));
	}

}

[Serializable]
public struct ChildTestArgs {
	public string name;
	public Vector2 position;
	public double rotation;
	public Vector2 scale;
	public Color color;
}
