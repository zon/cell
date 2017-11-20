using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using Cell;

public class Matrix3x3Test {

	[Test]
	public void Multiply() {
		var a = new Matrix4x4(
			new Vector4(2, 3, 5, 0),
			new Vector4(11, 13, 17, 0),
			new Vector4(23, 29, 31, 0),
			new Vector4(0, 0, 0, 1)
		);
		var b = new Matrix4x4(
			new Vector4(59, 61, 67, 0),
			new Vector4(73, 79, 83, 0),
			new Vector4(97, 101, 103, 0),
			new Vector4(0, 0, 0, 1)
		);

		var unity = a * b;
		var cell = a.To3x3() * b.To3x3();

		Assert.AreEqual(cell, unity.To3x3());
	}

	[Test]
	public void Translate() {
		var offset = new UnityEngine.Vector3 (21, 37, 0);
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Matrix3x3.Translate (offset.XY ().ToCell ());
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Translate (offset);
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void Rotate() {
		var degrees = 30f;
		var radians = degrees * Cell.Vec2.deg2rad;
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Matrix3x3.Rotate (radians);
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Rotate (UnityEngine.Quaternion.Euler (0, 0, degrees));
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void QuatRotate() {
		var degrees = 30f;
		var quat = new Quat(Vec3.up, degrees);
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Matrix3x3.Rotate (quat);
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Rotate (Quaternion.AngleAxis(degrees, Vector3.up));
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void Scale() {
		var scale = new UnityEngine.Vector3 (2, 7, 1);
		var point = new UnityEngine.Vector3 (5, 3, 0);

		var cell = Matrix3x3.Scale (scale.XY ().ToCell ());
		var cv = cell * point.XY ().ToCell ();

		var unity = Matrix4x4.Scale (scale);
		var uv = unity.MultiplyPoint (point);

		Assert.AreEqual (cv.ToUnity (), uv.XY ());
	}

	[Test]
	public void TRS() {
		var offset = new UnityEngine.Vector3(887, 431, 0);
		var degrees = 83f;
		var quat = new Quat(Vec3.up, degrees);
		var scale = new Vector3(2, 3, 1);
		var point = new Vector3(409, 701);

		var cell = Matrix3x3.TRS(offset.XY().ToCell(), quat, scale.XY().ToCell());
		var cv = cell * point.XY().ToCell();

		var unity = Matrix4x4.TRS(offset, Quaternion.Euler(0, 0, degrees), scale);
		var uv = unity.MultiplyPoint(point);

		Assert.AreEqual(cv.Floor(), uv.XY().ToCell().Floor());
	}

}
