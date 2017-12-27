using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class MeshCollisionTest : MonoBehaviour {
	new public Camera camera;
	public double aRotationRate;
	public Vector2 aScale = Vector2.one;
	public double bRotationRate;
	public Vector2 bScale = Vector2.one;

	MeshShape a;
	MeshShape b;

	void Start () {
		a = new Node("A").AddBehavior(new MeshShape(Mesh2.square));
		b = new Node("B").AddBehavior(new MeshShape(Mesh2.square));
	}
	
	void Update () {
		a.transform.localRotation += aRotationRate * Vec2.deg2rad * Time.deltaTime;
		a.transform.localScale = aScale.ToCell();

		b.transform.localPosition = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.transform.localRotation += bRotationRate * Vec2.deg2rad * Time.deltaTime;
		b.transform.localScale = bScale.ToCell();

		Behavior.CoreUpdate();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.transform.localPosition += collision.overlap;
			b.transform.Update();
			b.Update();
			
			a.mesh.DebugDraw(Color.red);
		} else {
			a.mesh.DebugDraw(Color.yellow);
		}

		b.mesh.DebugDraw(Color.blue);

	}
}
