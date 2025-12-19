using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instaciaManager;

    public float EsperaDeMuerte = 2f;

    private void Awake()
    {
        instaciaManager = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JugadorMuerto()
    {
        StartCoroutine(JugadorMuertoCO());       
    }

    public IEnumerator JugadorMuertoCO()
    {
        yield return new WaitForSeconds(EsperaDeMuerte);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
