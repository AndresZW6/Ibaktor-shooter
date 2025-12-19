using UnityEngine;

public class VidaJugadorControlador : MonoBehaviour
{
    public static VidaJugadorControlador instanciaVidaJugador;

    public int vidaMaxima, vidaActual;

    public float invencible = 1f;
    public float contadorInvencible;

    private void Awake()
    {
        instanciaVidaJugador = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual = vidaMaxima;
    
        UIControl.instanciaUI.SliderVida.maxValue = vidaMaxima;
        UIControl.instanciaUI.SliderVida.value = vidaActual;
        UIControl.instanciaUI.TextoVida.text = "VIDA: " + vidaActual + "/" + vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        if(contadorInvencible > 0)
        {
            contadorInvencible -= Time.deltaTime;
        }
    }

    public void DanoJugador(int cantidadDano)
    {
        if(contadorInvencible <=0)
        {

            vidaActual -= cantidadDano;

            if(vidaActual <= 0)
            {
                gameObject.SetActive(false);

                vidaActual = 0;

                GameManager.instaciaManager.JugadorMuerto();
            }

            contadorInvencible = invencible;

            UIControl.instanciaUI.SliderVida.value = vidaActual;
            UIControl.instanciaUI.TextoVida.text = "VIDA: " + vidaActual + "/" + vidaMaxima;

        }
    }
}
