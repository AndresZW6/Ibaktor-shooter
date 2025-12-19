using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    //Generamos la referencia a seguir para el enemigo
    public static PlayerControl instancia;
    
    //Declarar objetos para interacciones con disparadores/colisionadores
    public GameObject cubo, esfera, cilindro, pressF;

    //------VARIABLES------

    //variable de velocidad de movimiento del jugador
    public float Vmovimiento, Vcorrer;

    //Variable para contener el controlador del personaje (fisicas)
    public CharacterController CharCon;

    //Variable de vector para mover al player
    private Vector3 moveInput;

    //Movimiento de camara 
    public Transform CamMov;

    public float SensibilidadCam;

    public bool invertX;
    public bool invertY;

    //salto
    public float FuerzaSalto, FuerzaSaltoDoble;

    private bool PuedoSaltar, PuedoSaltarDoble;
    public Transform PisoOK;
    public LayerMask QueEsPiso;

    //Disparar
    public GameObject proyectil;
    public Transform puntoFuego;

    public Pistola pistolaActiva;

    public List<Pistola> TodasLasPistolas = new List<Pistola>();
    public int PistolaActual;

    //Modificador de gravedad

    public float gravedadM;

    //---------------------------------------------------------------------------------------------

    private void Awake()
    {
        instancia = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sesión de interaccion con triggers
        cubo.SetActive(true);
        esfera.SetActive(false);
        cilindro.SetActive(false);
        pressF.SetActive(false);

        //Municion inicial
        //UIControl.instanciaUI.TextoMunicion.text = "MUNICION: " + pistolaActiva.MunicionActual;

        //pistolaActiva = TodasLasPistolas[PistolaActual];
        //pistolaActiva.gameObject.SetActive(true);
        PistolaActual --;
        CambioArma();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del player con entrada en eje correspondiente multiplicada por la velocidad de movimiento
        //moveInput.x = Input.GetAxis("Horizontal") * Vmovimiento * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * Vmovimiento  * Time.deltaTime;

        // Almacenar velocidad en y
        // variable para almacenar el movimiento en eje vertical
        float yStore = moveInput.y;

        Vector3 MovVertical = transform.forward * Input.GetAxis("Vertical");
        Vector3 MovHorizontal = transform.right * Input.GetAxis("Horizontal");

        moveInput = MovHorizontal + MovVertical;
        moveInput = moveInput * Vmovimiento;
        moveInput.Normalize();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * Vcorrer;
        }
        else
        {
            moveInput = moveInput * Vmovimiento;
        }

        moveInput.y = yStore;

        //gravedad en mi personaje
        moveInput.y += Physics.gravity.y * gravedadM * Time.deltaTime;

        //Condicion del personaje en el suelo 

        if(CharCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravedadM;
        }
        
        PuedoSaltar = Physics.OverlapSphere(PisoOK.position, 0.1f, QueEsPiso).Length > 0;

        //Salto
        if(PuedoSaltar)
        {
            PuedoSaltarDoble = false;
        }

        //Si pulso la tecla espacio y puedo saltar es verdadero 
        if(Input.GetKeyDown(KeyCode.Space) && PuedoSaltar)
        {
            // ejecucion del salto
            moveInput.y = FuerzaSalto;
            //activando variable de salto doble
            PuedoSaltarDoble = true;
        } else if(PuedoSaltarDoble && Input.GetKeyDown(KeyCode.Space))
       { 
           moveInput.y = FuerzaSaltoDoble;

           PuedoSaltarDoble = false;
       }

        CharCon.Move(moveInput * Time.deltaTime);

        //Rotacion de camara
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * SensibilidadCam * Time.deltaTime;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        CamMov.rotation = Quaternion.Euler(CamMov.rotation.eulerAngles + new Vector3(mouseInput.y, 0f, 0f));

        //Control de disparo
        //Un unico disparo
        if(Input.GetMouseButtonDown(0) && pistolaActiva.contadorCadencia <= 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(CamMov.position, CamMov.forward, out hit, 50f))
            {
                if(Vector3.Distance(CamMov.position,hit.point) > 2f)
                {
                    puntoFuego.LookAt(hit.point);
                }
                puntoFuego.LookAt(hit.point);
            }
            else
            {
                puntoFuego.LookAt(CamMov.position + (CamMov.forward * 30f));
            }

            //Ejecucion de disparos antes de agregar sistema de armas
            //Instantiate(proyectil, puntoFuego.position, puntoFuego.rotation);

            //Ejectuamos la funcion de disparos
            Disparos();
        }

        //Disparos repetitivos
        if(Input.GetMouseButton(0) && pistolaActiva.PuedeDispararAutomatico)
        {
            if(pistolaActiva.contadorCadencia <= 0)
            {
                Disparos();
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CambioArma();
        }
    }

    //Funcion que ejecuta disparos con la pistola activa
    public void Disparos()
    {
        if(pistolaActiva.MunicionActual > 0 )
        {

            pistolaActiva.MunicionActual --;
            //Creamos el disparo a partir del objeto que tiene asignado el script de pistola
            Instantiate(pistolaActiva.Bala, puntoFuego.position, puntoFuego.rotation);

            pistolaActiva.contadorCadencia = pistolaActiva.CadenciaTiro;

            UIControl.instanciaUI.TextoMunicion.text = "MUNICION: " + pistolaActiva.MunicionActual;

        }
    }

    //------SISTEMAS DE INTERACCIONES------

    //Cuando entre en colision
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas entrando al cubo");
            cubo.SetActive(false);
            cilindro.SetActive(true);
        }

        if(other.name == "TriggerCilindro")
        {
            Debug.Log("Estas entrando al cilindro");
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas entrando a la esfera");
            pressF.SetActive(true);

        }
    }

    //Cuando salga de la colision
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas saliendo del cubo");
        }

        if(other.name == "TriggerCilindro")
        {
            Debug.Log("Estas saliendo del cilindro");
            cilindro.SetActive(false);
            esfera.SetActive(true);
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas saliendo de la esfera");
            pressF.SetActive(false);
        }
    }

    //Cuando este dentro de la colision
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas dentro del cubo");
        }

        if(other.name == "TriggerCilindro")
        {
            Debug.Log("Estas dentro del cilindro");
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas dentro de la esfera");
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("Game_02");
            }
        }
    }

    public void CambioArma()
    {
        pistolaActiva.gameObject.SetActive(false);

        PistolaActual++; 
        
        if(PistolaActual >= TodasLasPistolas.Count)
        {
            PistolaActual = 0;
        }

        pistolaActiva = TodasLasPistolas[PistolaActual];
        pistolaActiva. gameObject.SetActive(true);

        UIControl.instanciaUI.TextoMunicion.text = "MUNICION: " + pistolaActiva.MunicionActual;

        puntoFuego.position = pistolaActiva.puntofuego.position;
    }
}
