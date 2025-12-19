using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    //Variable de velocidad para el enemigo
    public float VMEnemigo;

    //Fisicas enemigo cuerpo rigido
    //public Rigidbody RBEnemigo;

    //Seguimiento del jugador
    private bool siguiendo;
    public float distanciaS = 10f;
    public float distanciaNS = 15f;
    public float distanciaDetenerse = 2f;

    //Variable para guardar el jugador a seguir

    private Vector3 PuntoObjetivo, PuntoInicio;

    //Agente de navegacion determina en que areas va a poder desplazarse el enemigo
    public NavMeshAgent agent;

    //Tiempo de espera en buscar al jugador y reset de posicion
    public float TiempoParaBuscar = 5f;
    private float ReiniciarBusqueda;

    //Disparos del enemigo
    public GameObject proyectilE;
    public Transform PuntoFuegoE;

    public float CadenciaE;
    private float ContadorFE;

    public float EsperaDisparo = 2f, tiempoDisparo = 1f;
    private  float EsperaDisparoContador, tiempoDisparoContador;

    public Animator animEnemigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PuntoInicio = transform.position;

        tiempoDisparoContador = tiempoDisparo;
        EsperaDisparoContador = EsperaDisparo;
    }

    // Update is called once per frame
    void Update()
    {
        PuntoObjetivo = PlayerControl.instancia.transform.position;
        PuntoObjetivo.y = transform.position.y;

        if (!siguiendo)
        {
            if(Vector3.Distance(transform.position, PuntoObjetivo) < distanciaS)
            {
                siguiendo = true;

                tiempoDisparoContador = tiempoDisparo;
                EsperaDisparoContador = EsperaDisparo;
            }

            if (ReiniciarBusqueda > 0)
            {
                ReiniciarBusqueda -= Time.deltaTime;

                if(ReiniciarBusqueda <= 0)
                {
                    agent.destination = PuntoInicio;
                }
            }

            if(agent.remainingDistance < 0.25f)
            {
                animEnemigo.SetBool("SeEstaMoviendo", false);
            }
            else
            {
                animEnemigo.SetBool("SeEstaMoviendo", true);
            }
        } 
        else
        {
        //Seguimiento del enemigo al jugador cancelado antes dle nav mesh
        //transform.LookAt(PuntoObjetivo);
        //RBEnemigo.linearVelocity = transform.forward * VMEnemigo;
            if(Vector3.Distance(transform.position, PuntoObjetivo) > distanciaDetenerse)
            {
                agent.destination = PuntoObjetivo;
            }
            else
            {
                agent.destination = transform.position; 
            }

            if(Vector3.Distance(transform.position, PuntoObjetivo) > distanciaNS)
            {
                siguiendo = false;

                ReiniciarBusqueda = TiempoParaBuscar;
            }
       
            if (EsperaDisparoContador > 0)
            { 
                EsperaDisparoContador -= Time.deltaTime;

                if(EsperaDisparoContador <= 0)
                {
                    tiempoDisparoContador = tiempoDisparo;              
                }
                animEnemigo.SetBool("SeEstaMoviendo", true);
            } else {

            if(PlayerControl.instancia.gameObject.activeInHierarchy)
            {

            tiempoDisparoContador -= Time.deltaTime;

                if (tiempoDisparoContador > 0)
                {
                    ContadorFE -= Time.deltaTime;

                    if(ContadorFE <= 0)
                    {
                        ContadorFE = CadenciaE;

                        PuntoFuegoE.LookAt(PlayerControl.instancia.transform.position + new Vector3(0f, 1f,0f));

                        //revision del algulo al jugador

                        Vector3 DireccionObjetivo = PlayerControl.instancia.transform.position - transform.position;
                        float angulo = Vector3.SignedAngle(DireccionObjetivo, transform.forward, Vector3.up);

                        if(Mathf.Abs(angulo) < 30f)
                        {
                            Instantiate(proyectilE, PuntoFuegoE.position, PuntoFuegoE.rotation);

                            animEnemigo.SetTrigger("EstaDisparando");
                        }
                        else
                        {
                            tiempoDisparoContador = EsperaDisparo;
                        }
                        
                    }

                    agent.destination = transform.position;

                } else {
                    tiempoDisparoContador = EsperaDisparo;
                }
            }
                
                animEnemigo.SetBool("SeEstaMoviendo", false);
        }           
        }
    }
}

