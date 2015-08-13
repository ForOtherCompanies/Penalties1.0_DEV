using UnityEngine;
using System.Collections;

public class DianaEfects : MonoBehaviour {

		void OnTriggerEnter(Collider other) {
			Destroy(this.gameObject);
		}
}
