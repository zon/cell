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

	MeshShape body;
	
	void Start () {
		body = new MeshShape(Mesh2.square);
	}
	
	void Update () {
		var t = (Time.time % translationDuration) / translationDuration;

		body.transform.localPosition = Vector2.Lerp(minPosition, maxPosition, t).ToCell();
		body.transform.localRotation += (rotationRate / translationDuration) * Vec2.deg2rad * Time.deltaTime;
		body.transform.localScale = Vector2.Lerp(minScale, maxScale, t).ToCell();

		body.Update();

		body.mesh.DebugDraw(Color.white);
	}

}
