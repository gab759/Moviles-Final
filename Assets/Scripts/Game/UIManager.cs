using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header("Elementos de UI")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private GameObject panelPausa;
    void Update()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + GameManager.Instance.puntaje;
        }
    }
    public void OnClick_Pausar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PausarJuego();
            panelPausa.SetActive(true); // Mostramos el panel de pausa
        }
    }
    public void OnClick_Reanudar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReanudarJuego();
            panelPausa.SetActive(false); // Ocultamos el panel de pausa
        }
    }
    public void OnClick_Reiniciar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarJuego();
        }
    }
}