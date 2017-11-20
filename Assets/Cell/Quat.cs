using System;

namespace Cell {

    public struct Quat : IEquatable<Quat> {
        static readonly Quat _identity = new Quat(0, 0, 0, 1);

        public double x;
        public double y;
        public double z;
        public double w;

        public static Quat identity {
            get { return _identity; }
        }

        public double this[int index] {
            get {
                switch (index) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (index) {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Quat cubic_slerp(Quat b, Quat preA, Quat postB, float t)
        {
            float t2 = (1.0f - t) * t * 2f;
            Quat sp = Slerp(b, t);
            Quat sq = preA.Slerpni(postB, t);
            return sp.Slerpni(sq, t2);
        }

        public double Dot(Quat b) {
            return x * b.x + y * b.y + z * b.z + w * b.w;
        }

        public Quat Inverse() {
            return new Quat(-x, -y, -z, w);
        }

        public double Length() {
            return Math.Sqrt(sqrLength());
        }

        public double sqrLength() {
            return Dot(this);
        }

        public Quat Normalized() {
            return this / Length();
        }

        public void set(double x, double y, double z, double w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quat Slerp(Quat b, double t) {
            // Calculate cosine
            var cosom = x * b.x + y * b.y + z * b.z + w * b.w;

            var to1 = new double[4];

            // Adjust signs if necessary
            if (cosom < 0.0) {
                cosom = -cosom; to1[0] = -b.x;
                to1[1] = -b.y;
                to1[2] = -b.z;
                to1[3] = -b.w;
            } else {
                to1[0] = b.x;
                to1[1] = b.y;
                to1[2] = b.z;
                to1[3] = b.w;
            }

            double sinom, scale0, scale1;

            // Calculate coefficients
            if ((1.0 - cosom) > double.Epsilon) {

                // Standard case (Slerp)
                var omega = Math.Acos(cosom);
                sinom = Math.Sin(omega);
                scale0 = Math.Sin((1.0f - t) * omega) / sinom;
                scale1 = Math.Sin(t * omega) / sinom;

            } else {

                // Quaternions are very close so we can do a linear interpolation
                scale0 = 1.0f - t;
                scale1 = t;
            }

            // Calculate final values
            return new Quat(
                scale0 * x + scale1 * to1[0],
                scale0 * y + scale1 * to1[1],
                scale0 * z + scale1 * to1[2],
                scale0 * w + scale1 * to1[3]
            );
        }

        public Quat Slerpni(Quat b, double t) {
            var dot = this.Dot(b);

            if (Math.Abs(dot) > 0.9999f) {
                return this;
            }

            var theta = Math.Acos(dot);
            var sinT = 1.0f / Math.Sin(theta);
            var newFactor = Math.Sin(t * theta) * sinT;
            var invFactor = Math.Sin((1.0f - t) * theta) * sinT;

            return new Quat(
                invFactor * this.x + newFactor * b.x,
                invFactor * this.y + newFactor * b.y,
                invFactor * this.z + newFactor * b.z,
                invFactor * this.w + newFactor * b.w
            );
        }

        public Vec3 Xform(Vec3 v) {
            Quat q = this * v;
            q *= this.Inverse();
            return new Vec3(q.x, q.y, q.z);
        }

        public Quat(double x, double y, double z, double w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quat(Vec3 axis, double degrees) {
            var radians = Vec2.deg2rad * degrees;
            var d = axis.magnitude;
			if (d == 0) {
                x = 0;
                y = 0;
                z = 0;
                w = 0;
            } else {
                var s = Math.Sin(radians * 0.5) / d;
                x = axis.x * s;
                y = axis.y * s;
                z = axis.z * s;
                w = Math.Cos(radians * 0.5);
            }
        }

        public static Quat operator *(Quat a, Quat b) {
            return new Quat(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y + a.y * b.w + a.z * b.x - a.x * b.z,
                a.w * b.z + a.z * b.w + a.x * b.y - a.y * b.x,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
            );
        }

        public static Quat operator +(Quat a, Quat b) {
            return new Quat(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Quat operator -(Quat a, Quat b) {
            return new Quat(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Quat operator -(Quat q) {
            return new Quat(-q.x, -q.y, -q.z, -q.w);
        }

        public static Quat operator *(Quat q, Vec3 v) {
            return new Quat(
                q.w * v.x + q.y * v.z - q.z * v.y,
                q.w * v.y + q.z * v.x - q.x * v.z,
                q.w * v.z + q.x * v.y - q.y * v.x,
                -q.x * v.x - q.y * v.y - q.z * v.z
            );
        }

        public static Quat operator *(Vec3 v, Quat q) {
            return q * v;
        }

        public static Quat operator *(Quat q, double d) {
            return new Quat(q.x * d, q.y * d, q.z * d, q.w * d);
        }

        public static Quat operator *(double d, Quat q) {
            return q * d;
        }

        public static Quat operator /(Quat q, double d) {
            return q * (1.0 / d);
        }

        public static bool operator ==(Quat a, Quat b) {
            return a.Equals(b);
        }

        public static bool operator !=(Quat a, Quat b) {
            return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
        }

        public override bool Equals(object obj) {
            if (obj is Quat)
                return Equals((Quat) obj);
            else
				return false;
        }

        public bool Equals(Quat other) {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

		public override int GetHashCode() {
			return Hash.Base
				.HashValue(x)
				.HashValue(y)
				.HashValue(z)
				.HashValue(w);
		}

		public override string ToString() {
			return string.Format("Quat({0}, {1}, {2}, {3})", x, y, z, w);
		}

    }
}
