using UnityEngine;
using System.Collections;

public class IAPortero : IAManager
{


    void Start()
    {
        level = 0;
        MaxForce = 190f;
        MinForce = 170f;
    }

    public override void RealizarAccion()
    {
        float minX = MinX;
        float maxX = MaxX;
        switch (Random.Range(0, 3))
        {
            case 0:
                minX = 0;
                break;
            case 1:
                maxX = 0;
                break;

        }
        x = Random.Range(minX, maxX);
        y = Random.Range(MinY, MaxY);
        impacto = new Vector3(x, y, 0);
        //direccion =impacto - Posicion.transform.position ;
        //direccion.z=0;
        direccion.Normalize();
        direccion.y = Mathf.Clamp(direccion.y, 0.5f, 0.7f);
        fuerza = Random.Range(MinForce, MaxForce);
        fisicasIA.AccionIA(direccion, fuerza, level);
        //level = Mathf.Clamp(level,0,8)+1;
    }
}
