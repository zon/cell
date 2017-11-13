using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Cell;

public class Matrix3x3Test {

	[Test]
	public void Translate() {
		var offset = new UnityEngine.Vector3 (21, 37, 0);
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Cell.Matrix3x3.Translate (offset.XY ().ToCell ());
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Translate (offset);
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void Rotate() {
		var degrees = 30f;
		var radians = degrees * Cell.Vector2.deg2rad;
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Cell.Matrix3x3.Rotate (radians);
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Rotate (Quaternion.Euler (0, 0, degrees));
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void Scale() {
		var scale = new UnityEngine.Vector3 (2, 7, 1);
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Cell.Matrix3x3.Scale (scale.XY ().ToCell ());
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Scale (scale);
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

}
