using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleShapeView : MonoBehaviour {

	public MeshFilter filter { get; private set; }
	public new MeshRenderer renderer { get; private set; }
	public CircleShape shape { get; private set; }

	void Awake() {
		filter = GetComponent<MeshFilter>();
		renderer = GetComponent<MeshRenderer>();
	}

	void Update() {
		transform.position = shape.transform.localPosition.ToUnity();
		transform.rotation = Quaternion.AngleAxis((float) shape.transform.localRotation * Mathf.Rad2Deg, Vector3.forward);
		transform.localScale = Vector3.one * (float) shape.scaleRadius * 2;
	}

	public void Attach(CircleShape shape) {
		this.shape = shape;

		var vertices = new Vector3[vertexCount];
		for (var v = 0; v < vertexCount; v++) {
			vertices[v] = Quaternion.Euler(0, 0, (360f / vertexCount) * v) * Vector3.right * 0.5f;
		}

		var mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.Fill2D();
		filter.mesh = mesh;

		renderer.material = MeshShapeView.material;
	}

	public void Attach(CircleShape shape, Color color) {
		Attach(shape);
		renderer.material.color = color;
	}

	const int vertexCount = 32;

}
