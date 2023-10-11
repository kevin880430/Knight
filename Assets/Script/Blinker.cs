using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    //点滅の速さ 
    public float speed = 1.0f;
    //テキストオブジェクトを宣言
    private Text text;
    //時間周期
    private float time;
    void Start()
    {
        //テキストオブジェクトを取得
        text = this.gameObject.GetComponent<Text>();
    }


    void Update()
    {
        //テキストの色を変更メソッドを呼ぶ
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        //時間流れのスピード
        time += Time.deltaTime * 5.0f * speed;
        //Sin関数による周期で色を変更する
        color.a = Mathf.Sin(time);
        //色情報を返却
        return color;
    }
}