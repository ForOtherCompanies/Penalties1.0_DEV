using UnityEngine;
using System.Collections;

public class FanController : MonoBehaviour {



	Animator animator;

	void Start () {

		animator = GetComponent<Animator>();
		float startPoint = Random.Range(0f, 1f);

		animator.Play ("FanSTDAnim",-1,startPoint);


	}


	

}
