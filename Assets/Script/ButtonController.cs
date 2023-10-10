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
    
    private void Start()
    {
        //SpriteRendererを取得する
        sr = GetComponent<SpriteRenderer>();
        //ボタン画像状態を初期化
        SetUnpressedState();
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
    
}
