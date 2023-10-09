using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Sprite pressedSprite; // 按下狀態的圖片
    public Sprite unpressedSprite; // 未按下狀態的圖片
    private SpriteRenderer sr;
    
    private void Start()
    { 
        sr = GetComponent<SpriteRenderer>();
        SetUnpressedState();
    }
    
    // 切換按鈕圖片為按下狀態
    public void SetPressedState()
    { 
        sr.sprite = pressedSprite;
        Debug.Log("已更改圖片");
    }
    public void SetUnpressedState()
    {
        sr.sprite = unpressedSprite;
        Debug.Log("已重製圖片");
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
