using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    public float destroyTime=0.4f;
    void Start()
    {
        //0.4•bŒã©•ª‚ğíœ‚·‚é
        Destroy(this.gameObject, destroyTime);
    }
}
