using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Animator EnemeyAnimator;

    void Start()
    {
       EnemeyAnimator= GetComponent<Animator>();
    }
    public void Gethurt()
    {
        Invoke("GetHit", 0.15f);
    }

    // Update is called once per frame
    public void GetHit()
    {
        EnemeyAnimator.SetTrigger("GetHit");
    }
}
