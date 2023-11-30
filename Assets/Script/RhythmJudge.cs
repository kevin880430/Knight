using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmJudge : MonoBehaviour
{
    public RhythmControl rhythmControl;
    //Triggerの種類
    public int JudgeIndex = 0;
    
    //Trigger管理者を呼ぶ
    private void OnTriggerStay2D(Collider2D collision)
    {
        rhythmControl.OnTriggerEnterProxy(collision, this);
    }
}
