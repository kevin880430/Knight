using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMain : MonoBehaviour
{
   
    void Update()
    {
        //R‚ğ‰Ÿ‚µ‚½‚çƒƒCƒ“‰æ–Ê‚É–ß‚é
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
