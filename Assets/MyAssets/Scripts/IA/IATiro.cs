using UnityEngine;
using System.Collections;

public class IATiro : MonoBehaviour {

	//keep private
	private static float MinX=-7.5f;
	private static float MaxX=1.35f;
	private static float MinY=0.8f;
	private static float MaxY=2.7f;
	private static float Z = 10.89f;
	private static float MaxForce = 179f;
	private static float MinForce = 145f;
	//turn private 
	private float x;
	private float y;
	private Vector3 direccion;
	private Vector3 impacto;
	private float fuerza;
	//keep public
	public PelotaFisicas pelota;
	public GameObject pelotaPosicionLanzamiento;
	public int level;
	// Use this for initialization
	void Start () {
		level = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			x = Random.Range(MinX,MaxX);
			y = Random.Range(MinY,MaxY);
			impacto = new Vector3(x,y,Z);
			direccion =impacto - pelotaPosicionLanzamiento.transform.position ;
			direccion= direccion.normalized;
			direccion.y*=2.68f;
			fuerza = Random.Range(MinForce,MaxForce);
			pelota.LanzamientoIA(direccion,fuerza,level);
			level = Mathf.Clamp(level,0,8)+1;
		}
	}
}
