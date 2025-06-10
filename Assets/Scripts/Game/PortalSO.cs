using UnityEngine;

[CreateAssetMenu(fileName = "PortalSO", menuName = "PortalData/Portal")]
public class PortalSO : ScriptableObject
{
    public int number;
    public enum PortalType { Suma, Multiplicacion }
    public PortalType portalType;
    public bool isRightPortal = false;

    public void GeneratePortal()
    {
        if (portalType == PortalType.Suma)
        {
            number = Random.Range(1, 11) * 5;
        }
        else if (portalType == PortalType.Multiplicacion)
        {
            number = Random.Range(1, 6);
        }
    }
}
