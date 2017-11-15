using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

public class TRSTest : MonoBehaviour {
	public float translationDuration = 10;
	public UnityEngine.Vector2 minPosition;
	public UnityEngine.Vector2 maxPosition;
	public double rotationRate;
	public UnityEngine.Vector2 minScale = UnityEngine.Vector2.one;
	public UnityEngine.Vector2 maxScale = UnityEngine.Vector2.one;

	Body body;
	
	void Start () {
		body = new Body();
		body.source = Mesh2.square.Clone(); ;
	}
	
	void Update () {
		var t = (Time.time % translationDuration) / translationDuration;

		body.position = UnityEngine.Vector2.Lerp(minPosition, maxPosition, t).ToCell();
		body.rotation += (rotationRate / translationDuration) * Cell.Vector2.deg2rad * Time.deltaTime;
		body.scale = UnityEngine.Vector2.Lerp(minScale, maxScale, t).ToCell();

		body.Update();

		body.mesh.DebugDraw(Color.white);
	}

}
