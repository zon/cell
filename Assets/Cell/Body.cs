using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cell {

	public class Body {
		public Mesh2 source;
		public Vector2 position;
		public double rotation;
		public Vector2 scale = Vector2.one;

		Matrix3x3 _matrix;
		Mesh2 _mesh = new Mesh2();

		public Matrix3x3 matrix {
			get { return _matrix; }
		}

		public Mesh2 mesh {
			get { return _mesh; }
		}

		public void Update() {
			_matrix = Matrix3x3.TRS(position, rotation, scale);

			if (mesh.vertices.Length != source.vertices.Length)
				mesh.vertices = new Vector2[source.vertices.Length];

			for (var i = 0; i < mesh.vertices.Length; i++)
				mesh.vertices[i] = _matrix * source.vertices[i];



			mesh.Update();
		}

		public Collision CheckCollision(Body other) {
			var axes = new Vector2[_mesh.surfaceAxes.Count + other.mesh.surfaceAxes.Count];
			var pushes = new Vector2[axes.Length];
			
			var i = 0;
			foreach (var axis in _mesh.surfaceAxes)
				axes[i++] = axis;
			foreach (var axis in other.mesh.surfaceAxes)
				axes[i++] = axis;

			for (var a = 0; a < axes.Length; a++) {
				var test = IsSeparate(axes[a], other);
				if (test.isSeparate)
					return null;
				else
					pushes[a] = test.push;
			}

			var min = Vector2.zero;
			var minLength = double.PositiveInfinity;
			for (var p = 0; p < pushes.Length; p++) {
				var push = pushes[p];
				var length = push.Dot(push);
				if (length < minLength) {
					minLength = length;
					min = push;
				}
			}

			var delta = other.position - position;
			if (delta.Dot(min) > 0)
				min = min * -1;

			return new Collision(other, min);
		}

		IsSeperateResult IsSeparate(Vector2 axis, Body other) {
			var res = new IsSeperateResult();
			var a = Project(_mesh, axis);
			var b = Project(other.mesh, axis);

			Draw.Line(axis * b.min, axis * b.max, UnityEngine.Color.cyan);
			Draw.Line(axis * a.min, axis * a.max, UnityEngine.Color.magenta);

			if (a.max >= b.min && b.max >= a.min) {
				res.push = axis * Math.Min(b.max - a.min, a.max - b.min);
			} else {
				res.isSeparate = true;
			}
			return res;
		}

		Line Project(Mesh2 mesh, Vector2 axis) {
			var min = double.PositiveInfinity;
			var max = double.NegativeInfinity;
			for (var v = 0; v < mesh.vertices.Length; v++) {
				var projection = mesh.vertices[v].Dot(axis);
				min = Math.Min(min, projection);
				max = Math.Max(max, projection);
			}
			var line = new Line();
			line.min = min;
			line.max = max;
			return line;
		}

		struct Line {
			public double min;
			public double max;
		}

		struct IsSeperateResult {
			public bool isSeparate;
			public Vector2 push;
		}

	}

}
