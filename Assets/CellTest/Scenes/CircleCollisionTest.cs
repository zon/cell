using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class CircleCollisionTest : MonoBehaviour {
	new public Camera camera;
	public double aRotationRate;
	public UnityEngine.Vector2 aScale = UnityEngine.Vector2.one;
	public UnityEngine.Vector2 bScale = UnityEngine.Vector2.one;

	MeshShape a;
	CircleShape b;

	void Start () {
		a = new Node("Square").AddBehavior(new MeshShape(Mesh2.square));
		b = new Node("Circle").AddBehavior(new CircleShape());
		b.radius = 0.5;
	}

	void Update () {
		a.transform.localRotation += aRotationRate * Vec2.deg2rad * Time.deltaTime;
		a.transform.localScale = aScale.ToCell();

		b.transform.localPosition = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		b.transform.localScale = bScale.ToCell();

		Behavior.Loop<Cell.Transform>(t => t.Update());
		Behavior.Loop<MeshShape>(s => s.Update());
		Behavior.Loop<CircleShape>(s => s.Update());
		Behavior.Loop<Cell.Transform>(t => t.PostUpdate());

		var collision = b.CheckCollision(a);
		if (collision != null) {
			b.transform.localPosition += collision.overlap;
			a.mesh.DebugDraw(Color.red);
		} else {
			a.mesh.DebugDraw(Color.yellow);
		}
	}

	void OnDrawGizmos() {
		if (!Application.isPlaying)
			return;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (b.transform.localPosition.ToUnity (), (float) b.scaleRadius);
	}

}
