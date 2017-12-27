using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class PedestrianTest : MonoBehaviour {
	public new Camera camera;
	public double radius = 0.5;
	public double speed = 2;
	public double acceleration = 2;

	Pedestrian follower;
	CircleShapeView followerView;

	void Start () {
		Tick.Setup(Time.fixedDeltaTime);

		follower = new Node("Follower").AddBehavior(new Pedestrian());
		follower.shape.radius = radius;
		followerView = follower.shape.CreateView();
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

		Behavior.Loop<Cell.Transform>(t => t.Update());
		Behavior.Loop<MeshShape>(s => s.Update());
		Behavior.Loop<CircleShape>(s => s.Update());
		Behavior.Loop<Pedestrian>(b => b.Update());
		Behavior.Loop<Pedestrian>(b => b.PhysicsUpdate());
		Behavior.Loop<Cell.Transform>(t => t.PostUpdate());
	}

}
