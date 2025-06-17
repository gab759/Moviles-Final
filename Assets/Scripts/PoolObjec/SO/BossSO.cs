using UnityEngine;

[CreateAssetMenu(fileName = "NuevoJefeSO", menuName = "DatosDeJuego/Jefe")]
public class BossSO : ScriptableObject
{
    [Tooltip("Cuántos 'golpes' o hamburguesas se necesitan para derrotarlo.")]
    public int vida = 100;

    
}