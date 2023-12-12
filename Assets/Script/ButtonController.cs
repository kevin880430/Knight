using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //押された状態のボタン画像素材
    public Sprite pressedSprite;
    //押されていない状態のボタン画像素材
    public Sprite unpressedSprite;
    //SpriteRendererを宣言
    private SpriteRenderer sr;
    public Vector3 pressedScale = new Vector3(0.8f, 0.8f, 0.8f); // 按下时的缩放值
    public float effectDuration = 0.1f; // 效果持续时间

    private void Start()
    {
        //SpriteRendererを取得する
        sr = GetComponent<SpriteRenderer>();
        //ボタン画像状態を初期化
        SetUnpressedState();
    }
    public void Scale()
    {
        StartCoroutine(ScaleButton());
    }    
    public void SetPressedState()
    { 
        //押された画像に変更
        sr.sprite = pressedSprite;
        Debug.Log("Preesed");
    }
    public void SetUnpressedState()
    {
        //押されていない画像に変更
        sr.sprite = unpressedSprite;
        Debug.Log("UnPressed");
    }
    IEnumerator ScaleButton()
    {
        Vector3 originalScale = this.transform.localScale; // 保存原始缩放值

        // 缩小按钮
        this.transform.localScale = pressedScale;

        // 等待一段时间
        yield return new WaitForSeconds(effectDuration);

        // 恢复按钮原始大小
        this.transform.localScale = originalScale;
    }

}
