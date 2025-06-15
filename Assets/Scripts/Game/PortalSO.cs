using UnityEngine;

[CreateAssetMenu(fileName = "PortalSO", menuName = "PortalData/Portal")]
public class PortalSO : ScriptableObject
{
    public int number;
    public enum PortalType { Suma, Multiplicacion }
    public PortalType portalType;
    public bool isRightPortal = false;

  
}
