using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using Cell;

public class RectTest {

	[Test]
	public void Fit() {
		var bounds = Bounds2.MinMax(3.2, 4.1, 10.6, 12);
		var rect = new Area();
		var scale = 0.5;
		rect.Fit(bounds, scale);
		Assert.AreEqual(new Area(6, 8, 21, 24), rect);
	}

}