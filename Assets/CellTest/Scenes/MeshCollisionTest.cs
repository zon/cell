﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class MeshCollisionTest : MonoBehaviour {
	new public Camera camera;
	public double aRotationRate;
	public UnityEngine.Vector2 aScale = UnityEngine.Vector2.one;
	public double bRotationRate;
	public UnityEngine.Vector2 bScale = UnityEngine.Vector2.one;

	MeshShape a;
	MeshShape b;

	void Start () {
		a = new MeshShape(Mesh2.square);
		b = new MeshShape(Mesh2.square);
	}
	
	void Update () {
		a.transform.rotation += aRotationRate * Vec2.deg2rad * Time.deltaTime;
		a.transform.scale = aScale.ToCell();
		a.Update();

		b.transform.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.transform.rotation += bRotationRate * Vec2.deg2rad * Time.deltaTime;
		b.transform.scale = bScale.ToCell();
		b.Update();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.transform.position += collision.overlap;
			b.Update();
		}

		a.mesh.DebugDraw(Color.red);
		b.mesh.DebugDraw(Color.blue);

	}
}
