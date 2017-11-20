using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public static class Mesh2Extension {

	public static Mesh ToUnity(this Mesh2 mesh) {
		var vertices = new UnityEngine.Vector3[mesh.vertices.Length];
		for (var v = 0; v < vertices.Length; v++) {
			vertices[v] = mesh.vertices[v].ToUnity();
		}
		var res = new Mesh();
		res.vertices = vertices;
		res.triangles = new int[] { 0, 1, 2 };
		return res;
	}

	public static void DebugDraw(this Mesh2 mesh, Color color, float duration = 0, bool depthTest = true) {
		for (var v = 1; v < mesh.vertices.Length; v++) {
			Debug.DrawLine(
				mesh.vertices[v - 1].ToUnity(),
				mesh.vertices[v].ToUnity(),
				color, duration, depthTest
			);
		}
		if (mesh.vertices.Length > 2)
			Debug.DrawLine(
				mesh.vertices[0].ToUnity(),
				mesh.vertices[mesh.vertices.Length - 1].ToUnity(),
				color, duration, depthTest
			);
	}

}
