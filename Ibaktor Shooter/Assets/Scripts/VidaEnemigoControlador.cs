using UnityEngine;

public class VidaEnemigoControlador : MonoBehaviour
{
    public int VidaActualE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DanoEnemigo(int CantidadDano)
    {
        VidaActualE -= CantidadDano;

        if (VidaActualE <= 0)
        {
            Destroy(gameObject);
        }
    }
}
