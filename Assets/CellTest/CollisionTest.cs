﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class CollisionTest : MonoBehaviour {
	new public Camera camera;
	public double aRotationRate;
	public UnityEngine.Vector2 aScale = UnityEngine.Vector2.one;
	public double bRotationRate;
	public UnityEngine.Vector2 bScale = UnityEngine.Vector2.one;

	Body a;
	Body b;

	void Start () {
		a = new Body();
		a.source = Mesh2.square.Clone();
		b = new Body();
		b.source = Mesh2.square.Clone();
	}
	
	void Update () {
		a.rotation += aRotationRate * Cell.Vector2.deg2rad * Time.deltaTime;
		a.scale = aScale.ToCell();
		a.Update();

		b.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.rotation += bRotationRate * Cell.Vector2.deg2rad * Time.deltaTime;
		b.scale = bScale.ToCell();
		b.Update();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.position += collision.push;
			b.Update();
		}

		a.mesh.DebugDraw(Color.red);
		b.mesh.DebugDraw(Color.blue);

	}
}
