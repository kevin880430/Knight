using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{

    void Start()
    {
        //0.4秒後自分を削除する
        Destroy(this.gameObject, 0.4f);
    }
}
