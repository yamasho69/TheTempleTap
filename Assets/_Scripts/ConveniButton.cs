using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ConveniButton : MonoBehaviour {
    [Header("表示したいWebページ")] public String url;
    [Header("移動したいシーン")] public String scene;
    [Header("表示したい・消したいゲームオブジェクト")] public GameObject gObject;
    [Header("Delayで遅らせたい時間")] public float delayTime;
    [Header("ウィンドウオープン時またはシーン遷移時になるSE")]public AudioClip OpenSE;
    [Header("ウィンドウクローズ時またはゲーム終了時になるSE")]public AudioClip CloseSE;
    bool dontSeBarrage;//DelayGoT0Scene,DelayQuitSceneのSEの連射防止
    SoundManager soundManager;

    public void Start() {
        GameObject gameObject = GameObject.FindGameObjectWithTag("SoundManager");
        soundManager = gameObject.GetComponent<SoundManager>();
        dontSeBarrage = false;
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    public void GoToScene() {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// delayTime分遅れて、シーン遷移
    /// </summary>
    public void DelayGoToScene() {
        if (OpenSE&&!dontSeBarrage) {
            soundManager.PlaySe(OpenSE);
            dontSeBarrage = true;
        }
        Invoke("GoToScene", delayTime);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// delayTime分遅れて、ゲーム終了
    /// </summary>
    public void DelayQuitGame() {
        if (CloseSE&&!dontSeBarrage) {
            soundManager.PlaySe(CloseSE);
            dontSeBarrage = true;
        }
        Invoke("QuitGame", delayTime);
    }

    /// <summary>
    /// Webページを開く
    /// </summary>
    public void GoToWeb() {
        if (OpenSE) {
            soundManager.PlaySe(OpenSE);
        }
        Application.OpenURL(url);
    }

    /// <summary>
    /// ActiveSelfを切り替える
    /// </summary>
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
