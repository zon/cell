using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using Cell;

public class QuatTest {

	[Test]
	public void AngleAxis() {
		var angle = 41f;
		var cell = new Quat(Vec3.up, angle);
		var unity = Quaternion.AngleAxis(angle, Vector3.up);
		Assert.AreEqual(cell.ToUnity(), unity);
	}
	
}
