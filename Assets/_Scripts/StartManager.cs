using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class StartManager : MonoBehaviour
{
    public AudioClip aino_Voice;
    public AudioClip yukkurikka_Voice;
    public AudioClip bgm;
    public GameObject buttons;
    public GameObject quitbutton;
    SoundManager soundManager;
    void Start()
    {
        // オーディオソース取得
        GameObject gameObject = GameObject.FindGameObjectWithTag("SoundManager");
        soundManager = gameObject.GetComponent<SoundManager>();
        soundManager.StopBgm();
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.8f);
        soundManager.PlaySe(aino_Voice);
        yield return new WaitForSeconds(2.0f);
        soundManager.PlaySe(yukkurikka_Voice);
        yield return new WaitForSeconds(0.5f);
        soundManager.PlayBgm(bgm);
        yield return new WaitForSeconds(0.5f);
        buttons.SetActive(true);
        #if UNITY_WEBGL
        quitbutton.SetActive(false);
        #endif
        yield return null;
    }
}
