using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    public AnimationCurve curve;
    public float endScale = 1f;
    public float startScale = 0f;
    public float tweenTime = 1f;
    public float holdTime = 1f;
    private IEnumerator Start()
    {
        gameObject.transform.localScale = startScale * Vector3.one;
        LeanTween.scale(gameObject, endScale * Vector3.one, tweenTime).setEase(curve);
        yield return new WaitForSeconds(holdTime);
        LeanTween.scale(gameObject, startScale * Vector3.one, tweenTime).setEase(curve).setOnComplete(DestroyMe);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
