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
    // 押された瞬間のサイズ調整値
    public Vector3 pressedScale = new Vector3(0.8f, 0.8f, 0.8f); 
    //サイズ変更の間時間
    public float effectDuration = 0.1f; 

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
        // ボタンの画像サイズを保存
        Vector3 originalScale = this.transform.localScale; 

        //ボタンの画像を縮小
        this.transform.localScale = pressedScale;

        // しばらく待つ
        yield return new WaitForSeconds(effectDuration);

        // ボタンの画像サイズを戻す
        this.transform.localScale = originalScale;
    }

}
