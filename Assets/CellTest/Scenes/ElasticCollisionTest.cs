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
	Pedestrian a;
	Pedestrian b;

	void Start() {
		Tick.Setup(Time.fixedDeltaTime);

		a = Create("A", aColor, aRadius, aPosition.ToCell(), aVelocity.ToCell());
		b = Create("B", bColor, bRadius, bPosition.ToCell(), bVelocity.ToCell());

		grid = new Grid(10, 1);
		grid.Add(a.shape);
		grid.Add(b.shape);
	}

	void FixedUpdate() {
		Behavior.CoreUpdate();
	}

	void Update() {
		grid.DrawCells(a.shape, aColor);
		grid.DrawCells(b.shape, bColor);
	}

	Pedestrian Create(string name, Color color, double radius, Vec2 position, Vec2 velocity) {
		var body = new Node("Body "+ name).AddBehavior(new Pedestrian());
		body.shape.radius = radius;
		body.transform.localPosition = position;
		body.velocity = velocity;
		body.shape.CreateView(color);
		return body;
	}

	
}
