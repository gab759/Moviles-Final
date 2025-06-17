using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    [Header("Elementos UI")]
    [SerializeField] private RectTransform titulo;
    [SerializeField] private RectTransform subtitulo;
    [SerializeField] private RectTransform[] botones;

    [Header("Animación")]
    [SerializeField] private float duracionTexto = 0.5f;
    [SerializeField] private float duracionBoton = 0.4f;
    [SerializeField] private float retrasoEntreBotones = 0.15f;
    [SerializeField] private float fueraPantallaY = 800f;
    [SerializeField] private float fueraPantallaX = 1000f;

    private Vector2 posTituloOriginal;
    private Vector2 posSubtituloOriginal;
    private Vector2[] posBotonesOriginal;

    private void Awake()
    {
        posTituloOriginal = titulo.anchoredPosition;
        posSubtituloOriginal = subtitulo.anchoredPosition;

        posBotonesOriginal = new Vector2[botones.Length];
        for (int i = 0; i < botones.Length; i++)
        {
            posBotonesOriginal[i] = botones[i].anchoredPosition;
        }
    }

    private void OnEnable()
    {
        AnimarEntrada();
    }

    private void AnimarEntrada()
    {
        titulo.anchoredPosition = posTituloOriginal + Vector2.up * fueraPantallaY;
        subtitulo.anchoredPosition = posSubtituloOriginal + Vector2.up * fueraPantallaY;

        titulo.DOAnchorPos(posTituloOriginal, duracionTexto).SetEase(Ease.OutBack);
        subtitulo.DOAnchorPos(posSubtituloOriginal, duracionTexto).SetEase(Ease.OutBack).SetDelay(0.1f);

        for (int i = 0; i < botones.Length; i++)
        {
            float direccion = (i % 2 == 0) ? -1f : 1f;
            botones[i].anchoredPosition = posBotonesOriginal[i] + Vector2.right * direccion * fueraPantallaX;

            botones[i].DOAnchorPos(posBotonesOriginal[i], duracionBoton)
                .SetEase(Ease.OutBack)
                .SetDelay(0.3f + i * retrasoEntreBotones);
        }
    }
}
