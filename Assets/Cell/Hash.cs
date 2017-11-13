namespace Cell {

	public static class Hash {
		public const int Base = 104729;

		public static int HashObject(this int hash, object obj) {
			unchecked { return hash * 98251 + (obj == null ? 0 : obj.GetHashCode()); }
		}

		public static int HashValue<T>(this int hash, T value) where T : struct {
			unchecked { return hash * 98251 + value.GetHashCode(); }
		}
	}

}