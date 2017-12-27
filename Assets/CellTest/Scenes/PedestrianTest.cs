using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class PedestrianTest : MonoBehaviour {
	public new Camera camera;
	public double radius = 0.5;
	public double speed = 2;
	public double acceleration = 2;
	
	Grid grid;
	Pedestrian follower;

	void Start () {
		Tick.Setup(Time.fixedDeltaTime);
		
		grid = new Grid(20, 0.75);

		follower = new Node("Follower").AddBehavior(new Pedestrian());
		follower.shape.radius = radius;
		grid.Add(follower.shape);
		follower.shape.CreateView();
	}
	
	void Update () {
		if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
			follower.MoveTo(camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell());
			follower.shape.radius = radius;
		}
	}

	void FixedUpdate() {
		follower.speed = speed;
		follower.acceleration = acceleration;

		Behavior.CoreUpdate();
	}

}
