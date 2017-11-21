using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class GridTest : MonoBehaviour {
	new public Camera camera;
	public int obstacleCount;
	public double rotation;
	public Vector2 scale = Vector2.one;

	Grid grid;
	MeshBody[] obstacles;
	MeshBody follower;

	void Start () {
		grid = new Grid(20, 0.75);

		obstacles = new MeshBody[obstacleCount];
		var size = 10f;
		for (var o = 0; o < obstacleCount; o++) {
			var ob = new MeshBody();
			ob.source = Mesh2.square.Clone();
			ob.transform.position = new Vector2(
				Random.Range(0, size),
				Random.Range(0, size)
			).ToCell();
			ob.transform.scale = scale.ToCell();
			obstacles[o] = ob;
			grid.Add(ob);
		}

		follower = new MeshBody();
		follower.source = Mesh2.square.Clone();
		grid.Add(follower);
	}
	
	void Update () {
		follower.transform.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		follower.transform.rotation = rotation;
		follower.transform.scale = scale.ToCell();

		grid.Update();
		grid.Post();

		var bodies = grid.Get(follower.cells);
		for (var o = 0; o < obstacles.Length; o++) {
			var ob = obstacles[o];
			grid.DrawCells(ob, Color.blue);
			if (bodies.Contains(ob))
				ob.mesh.DebugDraw(Color.red);
			else
				ob.mesh.DebugDraw(Color.yellow);
		}

		grid.DrawCells(follower, Color.magenta);
		follower.mesh.DebugDraw(Color.green);
	}
}
