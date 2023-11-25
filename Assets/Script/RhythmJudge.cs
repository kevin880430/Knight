using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmJudge : MonoBehaviour
{
    public RhythmControl rhythmControl;//代理器所代理的基类
    public int JudgeIndex = 0;//此代理器的代理序号
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        rhythmControl.OnTriggerEnterProxy(collision, this);
    }
}
