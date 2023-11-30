using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{

    void Start()
    {
        //0.4•bŒã©•ª‚ğíœ‚·‚é
        Destroy(this.gameObject, 0.4f);
    }
}
