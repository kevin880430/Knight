using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{

    void Start()
    {
        //0.4�b�㎩�����폜����
        Destroy(this.gameObject, 0.4f);
    }
}
