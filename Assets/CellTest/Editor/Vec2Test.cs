using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using Cell;

public class Vec2Test {

	[Test]
	public void Dot() {
		var a = new Vec2(21, 67);
		var b = new Vec2(7, 31);
		Assert.AreEqual(
			Vector2.Dot(a.ToUnity(), b.ToUnity()),
			(float) a.Dot(b)
		);
	}

	[Test]
	public void FromRadians() {
		var d = 35f;
		var r = d * Vec2.deg2rad;

		var cell = Vec2.FromRadians(r);
		var unity = (Quaternion.Euler(0, 0, d) * Vector3.right).XY().ToCell();

		var tollerance = 0.0001;
		Assert.That(unity.x, Is.EqualTo(cell.x).Within(tollerance));
		Assert.That(unity.y, Is.EqualTo(cell.y).Within(tollerance));
	}

	[Test]
	public void RadiansTo() {
		var a = new Vector2(2, 5);
		var b = new Vector2(3, 7);

		var unity = (double) (Vector2.Angle(a, b) * Mathf.Deg2Rad);
		var	cell = a.ToCell().RadiansTo(b.ToCell());

		Assert.AreEqual(unity, cell);
	}

	[Test]
	public void ToRadians() {
		var r = 3.33;
		var v = Vec2.FromRadians(r);
		Assert.That(r, Is.EqualTo(Math.PI * 2 + v.Radians()).Within(0.0001));
	}

}