using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cell;

public class GridTest : MonoBehaviour {
	new public Camera camera;
	public int obstacleCount;
	public double rotation;
	public Vector2 scale = Vector2.one;

	Grid grid;
	MeshShapeView[] obstacles;
	MeshShapeView follower;

	void Start () {
		grid = new Grid(20, 0.75);

		obstacles = new MeshShapeView[obstacleCount];
		var size = 10f;
		for (var o = 0; o < obstacleCount; o++) {
			var shape = new Node("Obstacle "+ o).AddBehavior(new MeshShape(Mesh2.square));
			shape.transform.position = new Vector2(
				Random.Range(0, size),
				Random.Range(0, size)
			).ToCell();
			shape.transform.scale = scale.ToCell();
			grid.Add(shape);

			var view = new GameObject(shape.node.name).AddComponent<MeshShapeView>();
			view.Attach(shape, Color.yellow);
			obstacles[o] = view;
		}

		var square = new Node("Follower").AddBehavior(new MeshShape(Mesh2.square));
		grid.Add(square);

		follower = new GameObject(square.node.name).AddComponent<MeshShapeView>();
		follower.Attach(square, Color.green);
	}
	
	void Update () {
		follower.shape.transform.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		follower.shape.transform.rotation = rotation;
		follower.shape.transform.scale = scale.ToCell();

		Behavior.Loop<Cell.Transform>(t => t.Update());
		Behavior.Loop<MeshShape>(s => s.Update());
		Behavior.Loop<Cell.Transform>(t => t.PostUpdate());
		Behavior.Loop<MeshShape>(s => s.PostUpdate());
		
		var collisions = grid.CheckCollision(follower.shape);
		var bodies = collisions.Select(c => c.shape);
		for (var o = 0; o < obstacles.Length; o++) {
			var ob = obstacles[o];
			grid.DrawCells(ob.shape, Color.blue);
			if (bodies.Contains(ob.shape))
				ob.renderer.material.color = Color.red;
			else
				ob.renderer.material.color = Color.yellow;
		}

		grid.DrawCells(follower.shape, Color.magenta);
		follower.shape.mesh.DebugDraw(Color.green);
	}
}
