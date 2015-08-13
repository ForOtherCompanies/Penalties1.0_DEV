using UnityEngine;
using System.Collections;

public class IAManager : MonoBehaviour {

	protected static float MinX=-7.5f;
	protected static float MaxX=1.35f;
	protected static float MinY=0.8f;
	protected static float MaxY=2.7f;
	protected static float Z = 11.13f;
	protected static float MaxForce;
	protected static float MinForce;
	
	protected float x;
	protected float y;
	protected Vector3 direccion;
	protected Vector3 impacto;
	protected float fuerza;
	
	protected int level;
	public PhysicManager fisicasIA;
	
	public GameObject Posicion;


	virtual public void  RealizarAccion(){}
}
