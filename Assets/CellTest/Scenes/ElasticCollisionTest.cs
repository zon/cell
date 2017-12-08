using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class ElasticCollisionTest : MonoBehaviour {
	public Color aColor = Color.red;
	public double aRadius = 0.5;
	public Vector2 aPosition = new Vector2(2, 5);
	public Vector2 aVelocity = new Vector2(2, 0);

	public Color bColor = Color.blue;
	public double bRadius = 0.5;
	public Vector2 bPosition = new Vector2(8, 5);
	public Vector2 bVelocity = new Vector2(-2, 0);

	Grid grid;
	Body a;
	Body b;

	void Start() {
		Tick.Setup(Time.fixedDeltaTime);

		a = Create("A", aColor, aRadius, aPosition.ToCell(), aVelocity.ToCell());
		b = Create("B", bColor, bRadius, bPosition.ToCell(), bVelocity.ToCell());

		grid = new Grid(10, 1);
		grid.Add(a.shape);
		grid.Add(b.shape);
	}

	void FixedUpdate() {
		Behavior.Loop<Body>(b => b.Update());
		Behavior.Loop<Body>(b => b.PhysicsUpdate());
		Behavior.Loop<Cell.Transform>(t => t.Update());
		Behavior.Loop<MeshShape>(s => s.Update());
		Behavior.Loop<CircleShape>(s => s.Update());
		Behavior.Loop<Cell.Transform>(t => t.PostUpdate());
	}

	void Update() {
		grid.DrawCells(a.shape, aColor);
		grid.DrawCells(b.shape, bColor);
	}

	Body Create(string name, Color color, double radius, Vec2 position, Vec2 velocity) {
		var body = new Node("Body "+ name).AddBehavior(new Body());
		body.shape.radius = radius;
		body.transform.position = position;
		body.velocity = velocity;
		body.shape.CreateView(color);
		return body;
	}

	
}
