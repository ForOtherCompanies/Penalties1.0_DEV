using UnityEngine;
using System.Collections;

public class IAPortero : IAManager {

	
	void Start () {
		level = 0;
		MaxForce = 150f;
		MinForce = 140f;
	}

	public override void  RealizarAccion()
	{
		x = Random.Range(MinX,MaxX);
		y = Random.Range(MinY,MaxY);
		impacto = new Vector3(x,y,Z);
		direccion =impacto - Posicion.transform.position ;
		direccion.z=0;
		direccion.Normalize();
		direccion.y = Mathf.Clamp(direccion.y,0.5f,0.7f);
		fuerza = Random.Range(MinForce,MaxForce);
		fisicasIA.AccionIA(direccion,fuerza,level);
		level = Mathf.Clamp(level,0,8)+1;
	}
}
