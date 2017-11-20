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
		a.transform.rotation *= new Quat(Vec3.up, aRotationRate) * Time.deltaTime;
		a.transform.scale = aScale.ToCell();
		a.Update();

		b.transform.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.transform.scale = bScale.ToCell();
		b.Update();

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.transform.position += collision.overlap;
			b.Update();
		}

		a.mesh.DebugDraw(Color.red);
	}

	void OnDrawGizmos() {
		if (!Application.isPlaying)
			return;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (b.transform.position.ToUnity (), (float) b.scaleRadius);
	}

}
