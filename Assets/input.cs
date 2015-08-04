using UnityEngine;
using System.Collections;

public class input : MonoBehaviour
{

	public GameObject ball;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	private GameObject lineGO;
	private LineRenderer lineRenderer;
	private int i = 0;
	public float time = 1.0f;
	private Vector2 inicio;
	private Vector2 fin;
	private float timer;
	private bool first = true;
	private bool lanzado = false;
	
	void Start ()
	{
		lineGO = new GameObject ("Line");
		lineGO.AddComponent<LineRenderer> ();
		lineRenderer = lineGO.GetComponent<LineRenderer> ();
		lineRenderer.material = new Material (Shader.Find ("Mobile/Particles/Additive"));
		lineRenderer.SetColors (c1, c2);
		lineRenderer.SetWidth (0.3F, 0);
		lineRenderer.SetVertexCount (0);
		timer = time;
	}
	
	void FixedUpdate ()
	{
		if (Input.touchCount > 0 && !lanzado) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Moved) {
				if (first) {
					inicio = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					first = false;
				}
				lineRenderer.SetVertexCount (i + 1);
				Vector3 mPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
				lineRenderer.SetPosition (i, Camera.main.ScreenToWorldPoint (mPosition));
				i++;
				timer -= Time.deltaTime;
			}
			
			if (touch.phase == TouchPhase.Ended || timer <= 0) {
				/* Remove Line */
				
				lineRenderer.SetVertexCount (0);
				i = 0;
				timer = time;
				first = true;
				fin = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				lanzado = true;
				lanzar ();
			}
		}
	}

	void lanzar ()
	{
		Rigidbody rb = ball.GetComponent<Rigidbody> ();
		float x, y, z;
		x = fin.x - inicio.x;
		y = fin.y - inicio.y;
		z = 15;
		rb.AddForce (new Vector3 (x, y, z));
	}
}
