using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject Bala;

    public bool PuedeDispararAutomatico;

    public float CadenciaTiro;

    [HideInInspector]

    public float contadorCadencia;

    public int MunicionActual, municionObtenida;

    public Transform puntofuego;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorCadencia > 0)
        {
            contadorCadencia -= Time.deltaTime;
        }
    }

    public void ObtenerMunicion()
    {
        MunicionActual += municionObtenida;

        UIControl.instanciaUI.TextoMunicion.text = "MUNICION: " + MunicionActual;
    }
}
