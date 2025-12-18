using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public CanvasGroup MenuCanvas;
    public CanvasGroup OptionsCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OptionsCanvas.gameObject.SetActive(false);
        MenuCanvas.gameObject.SetActive(true);
    }

    public void Play(){
        SceneManager.LoadScene("Game_01");
    }

    public void Options(){
        OptionsCanvas.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
    }

    public void Return(){
        OptionsCanvas.gameObject.SetActive(false);
        MenuCanvas.gameObject.SetActive(true);

    }

    public void Exit(){
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
