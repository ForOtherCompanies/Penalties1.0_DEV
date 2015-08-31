using UnityEngine;
using System.Collections;

public class DianaEfects : MonoBehaviour {
		GameObject particleEffects;

		public void Start(){
			particleEffects = GameObject.FindGameObjectWithTag ("Particles");	
			particleEffects.GetComponent<ParticleSystem>().Stop();
			particleEffects.transform.position = transform.position;

		}

		void OnTriggerEnter(Collider other) {
			particleEffects.GetComponent<ParticleSystem>().Play();
			Destroy(this.gameObject);
		}
}
