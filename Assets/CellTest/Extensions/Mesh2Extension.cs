using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cell;

public static class Mesh2Extension {

	public static Mesh ToUnity(this Mesh2 mesh) {
		
		var vertices = new Vector3[mesh.vertices.Length];
		var normals = new Vector3[vertices.Length];
		var uv = new Vector2[vertices.Length];
		for (var v = 0; v < vertices.Length; v++) {
			var vertex = mesh.vertices[v].ToUnity();
			vertices[v] = vertex;
			normals[v] = Vector3.back;
			uv[v] = vertex;
		}

		var triangles = new int[Mathf.Max(vertices.Length - 2, 0) * 3];
		for (var t = 0; t < triangles.Length / 3; t++) {
			var i = t * 3;
			triangles[i] = 0;
			triangles[i + 1] = t + 1;
			triangles[i + 2] = t + 2;
		}

		var result = new Mesh();
		result.vertices = vertices;
		result.triangles = triangles;
		result.normals = normals;
		result.uv = uv;
		return result;
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
