using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleShapeView : MonoBehaviour {
	public MeshFilter filter { get; private set; }
	public new MeshRenderer renderer { get; private set; }

	void Awake() {
		filter = GetComponent<MeshFilter>();
		renderer = GetComponent<MeshRenderer>();
	}
	

}
