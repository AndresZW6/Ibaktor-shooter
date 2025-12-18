using UnityEngine;

public class ObjetivoMovimiento : MonoBehaviour
{
    public bool PuedeMoverse, puedeRotar;

    public float VMObjetivo, Vrotacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PuedeMoverse)
        {
            transform.position += new Vector3(VMObjetivo, 0f, 0f) * Time.deltaTime;
        }
        if(puedeRotar)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, Vrotacion * Time.deltaTime, 0f));
        }
    }
}
