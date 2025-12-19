using UnityEngine;

public class BulletControl : MonoBehaviour
{
    //-------------Variables-------------

    //Velocidad de proyectil
    public float Vbullet;

    //Tiempo de desaparición del proyectil al no impactar con un colisionador
    public float TiempoDesaparicion;

    //Cuerpo rigido del proyectil para realizar fisicas
    public Rigidbody RBB;

    //Efectos especiales
    public GameObject EfectoImpacto;

    //Variable para asignar daño
    public int danoPistola;

    //Condicion para el daño hecho al jugador
    public bool danoEnemigo, danoJugador;

    //public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento lineas hacia adelante del proyectil(Pruebras)
        RBB.linearVelocity = transform.forward * Vbullet;

        //Condicion para eliminar proyectil despues de cierto tiempo determinado por la variable TiempoDesaparicion
        TiempoDesaparicion -= Time.deltaTime;

        if (TiempoDesaparicion <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Funcion para destruir mi proyectil al entrar en contacto con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        //Condiciones para eliminar objetivos y enemigos
        if(other.gameObject.tag == "Objetivos" && danoEnemigo)
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Enemigos" && danoEnemigo)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<VidaEnemigoControlador>().DanoEnemigo(danoPistola * 2);
        }

        if (other.gameObject.tag == "Player" && danoJugador)
        {
            //Debug.Log("Golpeo al jugador en " + transform.position);
            VidaJugadorControlador.instanciaVidaJugador.DanoJugador(danoPistola);
        }

        //Condicion para eliminar proyectil cuando impacte con otro objeto
        Destroy(gameObject);
        Instantiate(EfectoImpacto, transform.position + (transform.forward * (-Vbullet * Time.deltaTime)), transform.rotation);
    }

}
