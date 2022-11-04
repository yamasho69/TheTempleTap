using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ConveniButton : MonoBehaviour {
    [Header("表示したいWebページ")] public String url;
    [Header("移動したいシーン")] public String scene;
    [Header("表示したい・消したいゲームオブジェクト")] public GameObject gObject;
    [Header("DelayGoToSceneで遅らせたい時間")] public float delayTime;
    [Header("ウィンドウオープン時またはシーン遷移時になるSE")]public AudioClip OpenSE;
    [Header("ウィンドウクローズ時またはゲーム終了時になるSE")]public AudioClip CloseSE;
    SoundManager soundManager;

    public void Start() {
        GameObject gameObject = GameObject.FindGameObjectWithTag("SoundManager");
        soundManager = gameObject.GetComponent<SoundManager>();
    }

    public void GoToScene() {
        SceneManager.LoadScene(scene);
    }

    public void DelayGoToScene() {
        if (OpenSE) {
            soundManager.PlaySe(OpenSE);
        }
        Invoke("GoToScene", delayTime);
    }

    public void DelayQuitGame() {
        if (CloseSE) {
            soundManager.PlaySe(CloseSE);
        }
        Invoke("QuitGame", delayTime);
    }

    public void GoToWeb() {
        if (OpenSE) {
            soundManager.PlaySe(OpenSE);
        }
        Application.OpenURL(url);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenOrClose() {
        if (gObject.activeSelf) {
            if (CloseSE) {
                soundManager.PlaySe(CloseSE);
            }
            gObject.SetActive(false);           
        } else {
            if (OpenSE) {
                soundManager.PlaySe(OpenSE);
            }
            gObject.SetActive(true);
        }
    }
}
