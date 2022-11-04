using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ConveniButton : MonoBehaviour {
    [Header("�\��������Web�y�[�W")] public String url;
    [Header("�ړ��������V�[��")] public String scene;
    [Header("�\���������E���������Q�[���I�u�W�F�N�g")] public GameObject gObject;
    [Header("DelayGoToScene�Œx�点��������")] public float delayTime;
    [Header("�E�B���h�E�I�[�v�����܂��̓V�[���J�ڎ��ɂȂ�SE")]public AudioClip OpenSE;
    [Header("�E�B���h�E�N���[�Y���܂��̓Q�[���I�����ɂȂ�SE")]public AudioClip CloseSE;
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
