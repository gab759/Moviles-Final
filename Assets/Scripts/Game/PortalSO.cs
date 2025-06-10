using UnityEngine;

[CreateAssetMenu(fileName = "PortalSO", menuName = "PortalData/Portal")]
public class PortalSO : ScriptableObject
{
    public int number;  // El n�mero generado para el portal
    public enum PortalType { Suma, Multiplicacion }
    public PortalType portalType;

    // Esta propiedad nos ayuda a saber si este es el portal derecho o no
    public bool isRightPortal = false;  // Identificador de si el portal es el derecho

    // Funci�n que actualiza el n�mero cuando se genera aleatoriamente
    public void GeneratePortal()
    {
        if (portalType == PortalType.Suma)
        {
            // Generar un m�ltiplo de 5 hasta 50 para la suma
            number = Random.Range(1, 11) * 5;  // M�ltiplos de 5 (5, 10, 15, ..., 50)
        }
        else if (portalType == PortalType.Multiplicacion)
        {
            // Generar un n�mero entre 1 y 5 para multiplicaci�n
            number = Random.Range(1, 6);  // N�meros entre 1 y 5
        }
    }
}
