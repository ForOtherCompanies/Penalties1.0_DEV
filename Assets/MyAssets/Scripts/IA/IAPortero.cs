using UnityEngine;
using System.Collections;

public class IAPortero : MonoBehaviour {

	private static float MinX=-7.5f;
	private static float MaxX=1.35f;
	private static float MinY=0.8f;
	private static float MaxY=2.7f;
	private static float Z = 10.89f;
	private static float MaxForce = 150f;
	private static float MinForce = 140f;

	private float x;
	private float y;
	private Vector3 direccion;
	private Vector3 impacto;
	private float fuerza;

	private int level;

	public GameObject posicionPortero;
	public PorteroFisicas portero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{if(Input.GetKeyDown("space")){
			x = Random.Range(MinX,MaxX);
			y = Random.Range(MinY,MaxY);
			impacto = new Vector3(x,y,Z);
			direccion =impacto - posicionPortero.transform.position ;
			direccion.z=0;
			direccion.Normalize();
			direccion.y = Mathf.Clamp(direccion.y,0.5f,0.7f);
			fuerza = Random.Range(MinForce,MaxForce);
			portero.Saltar(direccion,fuerza);
			level = Mathf.Clamp(level,0,8)+1;
		}

	
	}
}
