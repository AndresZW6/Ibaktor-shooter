using UnityEngine;

public class MunicionesEnMapa : MonoBehaviour
{
    private bool recolectado;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !recolectado)
        {
            PlayerControl.instancia.pistolaActiva.ObtenerMunicion();
            //Recargar municiones del jugador
            Destroy(gameObject);

            recolectado = true;
        }
    }
}
