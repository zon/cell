using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshExtension {

	public static void Fill2D(this Mesh mesh) {
		var normals = new Vector3[mesh.vertices.Length];
		var uv = new Vector2[mesh.vertices.Length];
		for (var v = 0; v < mesh.vertices.Length; v++) {
			normals[v] = Vector3.back;
			uv[v] = mesh.vertices[v];
		}

		var triangles = new int[Mathf.Max(mesh.vertices.Length - 2, 0) * 3];
		for (var t = 0; t < triangles.Length / 3; t++) {
			var i = t * 3;
			triangles[i] = 0;
			triangles[i + 1] = t + 1;
			triangles[i + 2] = t + 2;
		}

		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
	}

}
