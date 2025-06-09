using System.Collections;
using UnityEngine;

public class EnemyObstacle : EnemyBase
{
    [SerializeField] private float bounceDuration = 1f;
    [SerializeField] private Vector3 smallScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 normalScale = Vector3.one;

    private Coroutine bounceRoutine;

    public override void OnActivate()
    {
        base.OnActivate();

        transform.localScale = smallScale;

        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);

        bounceRoutine = StartCoroutine(BounceScale());
    }

    private IEnumerator BounceScale()
    {
        float t = 0f;
        float half = bounceDuration / 2f;

        while (t < bounceDuration)
        {
            if (t < half)
                transform.localScale = Vector3.Lerp(smallScale, normalScale, t / half);
            else
                transform.localScale = Vector3.Lerp(normalScale, smallScale, (t - half) / half);

            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = smallScale;
    }
}
