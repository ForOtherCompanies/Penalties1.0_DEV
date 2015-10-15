using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{

    private bool IA;
    private bool MP;
    private Animator PlayerAnimator;
    public MacthController mController;
    // Use this for initialization
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        Reiniciar();
        IA = false;
        MP = false;
    }

    private void Reiniciar()
    {
        PlayerAnimator.SetBool("Idle_Tiro", false);
        PlayerAnimator.SetBool("Tiro", false);
        PlayerAnimator.SetBool("Idle_Salto", false);
        PlayerAnimator.SetBool("Salto", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Tirar()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Tiro", true);
    }

    internal void Saltar()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Salto", true);
    }

    internal void TirarIA()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Tiro", true);
        IA = true;
    }
    internal void SaltarIA()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Salto", true);
        IA = true;
    }

    internal void TirarMP()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Tiro", true);
        MP = true;
    }
    internal void SaltarMP()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Salto", true);
        MP = true;
    }
    internal void IdleSalto()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Idle_Salto", true);
    }

    internal void IdleTiro()
    {
        Reiniciar();
        PlayerAnimator.SetBool("Idle_Tiro", true);
    }

    internal void realizarAccion()
    {
        mController.DoAccion(MP, IA);
        if (MP)
        {
            MP = !MP;
        }
        if (IA)
        {
            IA = !IA;
        }
    }



}
