using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ConveniButton : MonoBehaviour
{
    [Header("表示したいWebページ")]public String url;
    [Header("移動したいシーン")]public String scene;
    [Header("表示したい・消したいゲームオブジェクト")]public GameObject gObject;

    
    public void GoToScene() {
        SceneManager.LoadScene(scene);
    }

    public void GoToWeb() {
        Application.OpenURL(url);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenOrClose() {
        if (gObject.activeSelf) {
            gObject.SetActive(false);           
        } else { gObject.SetActive(true);
        }
    }
}
