using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshShapeView : MonoBehaviour {

	public MeshFilter filter { get; private set; }
	public new MeshRenderer renderer { get; private set; }
	public MeshShape shape { get; private set; }

	void Awake() {
		filter = GetComponent<MeshFilter>();
		renderer = GetComponent<MeshRenderer>();
	}

	void Update() {
		transform.position = shape.transform.position.ToUnity();
		transform.rotation = Quaternion.AngleAxis((float) shape.transform.rotation * Mathf.Rad2Deg, Vector3.forward);
		transform.localScale = shape.transform.scale.ToUnity();
	}

	public void Attach(MeshShape shape) {
		this.shape = shape;
		filter.mesh = shape.source.ToUnity();
		renderer.material = material;
	}

	public void Attach(MeshShape shape, Color color) {
		Attach(shape);
		renderer.material.color = color;
	}

	static Material _material;

	public static Material material {
		get {
			if (_material == null)
				_material = new Material(Shader.Find("Sprites/Default"));
			return _material;
		}
	} 

}
