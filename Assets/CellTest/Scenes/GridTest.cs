using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class GridTest : MonoBehaviour {
	new public Camera camera;
	public double rotation;
	public Vector2 scale = Vector2.one;

	Grid grid;
	MeshBody body;

	// Use this for initialization
	void Start () {
		grid = new Grid(10, 0.75);
		body = new MeshBody();
		body.source = Mesh2.square.Clone();
		grid.Add(body);
	}
	
	void Update () {
		body.transform.position = camera.ScreenToWorldPoint(Input.mousePosition).XY().ToCell();
		body.transform.rotation = rotation;
		body.transform.scale = scale.ToCell();

		grid.Update();
		grid.Post();

		grid.DrawCells(body, Color.blue);
		body.mesh.DebugDraw(Color.green);
	}
}
