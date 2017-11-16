using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class CircleCollisionTest : MonoBehaviour {
	new public Camera camera;
	public double aRotationRate;
	public UnityEngine.Vector2 aScale = UnityEngine.Vector2.one;
	public UnityEngine.Vector2 bScale = UnityEngine.Vector2.one;

	MeshBody a;
	CircleBody b;

	void Start () {
		a = new MeshBody();
		a.source = Mesh2.square.Clone();
		b = new CircleBody();
		b.radius = 0.5;
	}

	void Update () {
		a.rotation += aRotationRate * Cell.Vector2.deg2rad * Time.deltaTime;
		a.scale = aScale.ToCell();
		a.Update();

		b.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.scale = bScale.ToCell();
		b.Update();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.position += collision.overlap;
			b.Update();
		}

		a.mesh.DebugDraw(Color.red);
	}

	void OnDrawGizmos() {
		if (!Application.isPlaying)
			return;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (b.position.ToUnity (), (float) b.scaleRadius);
	}

}
