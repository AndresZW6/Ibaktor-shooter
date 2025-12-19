using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    public static UIControl instanciaUI;

    public Slider SliderVida;
    public TextMeshProUGUI TextoVida;

    public TextMeshProUGUI TextoMunicion;

    private void Awake()
    {
        instanciaUI = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
