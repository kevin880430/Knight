using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RemainingTime : MonoBehaviour
{
    //残り時間を表示するテキストオブジェクト
    public Text remainingTimeText; 

    void Start()
    {
        // 保存された時間情報を読み込む
        float remainingTime = PlayerPrefs.GetFloat("RemainingTime");

        // データをテキストオブジェクトに代入
        remainingTimeText.text = "Remaining Time: " + Mathf.RoundToInt(remainingTime).ToString() + "s";
    }
}
