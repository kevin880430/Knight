using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shake : MonoBehaviour
{
    public Vector3 shakeRate = new Vector3(1.5f, 0, 0);
    public float shakeTime = 0.5f;
    public float shakeDertaTime = 0.1f;


    public void ShakeThis()
    {
        StartCoroutine(Shake_Coroutine());
    }

    public IEnumerator Shake_Coroutine()
    {
        var oriPosition = gameObject.transform.position;
        for (float i = 0; i < shakeTime; i += shakeDertaTime)
        {
            gameObject.transform.position = oriPosition +
                Random.Range(-shakeRate.x, shakeRate.x) * Vector3.right +
                Random.Range(-shakeRate.y, shakeRate.y) * Vector3.up +
                Random.Range(-shakeRate.z, shakeRate.z) * Vector3.forward;
            yield return new WaitForSeconds(shakeDertaTime);
        }
        gameObject.transform.position = oriPosition;
    }

}