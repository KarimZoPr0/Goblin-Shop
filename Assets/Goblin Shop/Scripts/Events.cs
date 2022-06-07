using System.Collections;

namespace something {


	public class Events {

		public delegate IEnumerator ShoppingDelegate();

		public event ShoppingDelegate ShoppingEvent;

	}
}