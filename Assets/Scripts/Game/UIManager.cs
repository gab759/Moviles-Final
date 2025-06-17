using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header("Elementos de UI")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;

    void Update()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + GameManager.Instance.puntaje;
        }
    }
}