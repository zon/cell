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

}