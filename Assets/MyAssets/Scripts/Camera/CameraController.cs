using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Camera main;
    public Camera game;
    public void ActivarGameCamera()
    {
        game.enabled = true;
        main.enabled = false;
    }

    public void DesactivarGameCamera()
    {
        game.enabled = false;
        main.enabled = true;
    }
}
