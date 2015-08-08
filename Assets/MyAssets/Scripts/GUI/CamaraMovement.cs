using UnityEngine;
using System.Collections;

public class CamaraMovement : MonoBehaviour {
	
	public GameObject objetivo;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;
	private bool mover = false;
	
	// Update is called once per frame
	void Update () {
		if(mover){
			Vector3 targetPosition = objetivo.transform.TransformPoint(new Vector3(0, 5, -10));
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
			Quaternion target = Quaternion.Euler(25, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
		}
	}
	
	public void Mover(){
		mover = true;
	}
}
