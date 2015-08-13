using UnityEngine;
using System.Collections;

public class InputEffects : MonoBehaviour
{
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	private GameObject lineGO;
	private LineRenderer lineRenderer;
	private int i = 0;
	
	void Start ()
	{
		lineGO = new GameObject ("Line");
		lineGO.AddComponent<LineRenderer> ();
		lineRenderer = lineGO.GetComponent<LineRenderer> ();
		lineRenderer.material = new Material (Shader.Find ("Mobile/Particles/Additive"));
		lineRenderer.SetColors (c1, c2);
		lineRenderer.SetWidth (0.05F, 0.05f);
		lineRenderer.SetVertexCount (0);
	}
	
	void FixedUpdate ()
	{

		if (Input.touchCount > 0 ) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Moved) {
				lineRenderer.SetVertexCount (i + 1);
				Vector3 mPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y,1f);
				lineRenderer.SetPosition (i, Camera.main.ScreenToWorldPoint (mPosition));
				i++;
			}
			
			if (touch.phase == TouchPhase.Ended) {

				
				lineRenderer.SetVertexCount (0);
				i = 0;
			}
		
	}
}
	public void Parar(){
		lineRenderer.SetVertexCount (0);
		i = 0;
		this.enabled = false;
	}
}
