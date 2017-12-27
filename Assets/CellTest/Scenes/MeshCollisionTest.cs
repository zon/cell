using System.Collections;
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
		a.transform.localRotation += aRotationRate * Vec2.deg2rad * Time.deltaTime;
		a.transform.localScale = aScale.ToCell();
		a.Update();

		b.transform.localPosition = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.transform.localRotation += bRotationRate * Vec2.deg2rad * Time.deltaTime;
		b.transform.localScale = bScale.ToCell();
		b.Update();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.transform.localPosition += collision.overlap;
			b.Update();
		}

		a.mesh.DebugDraw(Color.red);
		b.mesh.DebugDraw(Color.blue);

	}
}
